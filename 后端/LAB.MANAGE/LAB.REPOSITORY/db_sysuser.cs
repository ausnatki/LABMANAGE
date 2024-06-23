using LAB.DB;
using LAB.MODEL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace LAB.REPOSITORY
{
    public class db_sysuser
    {
        private readonly DB.LabContext _ctx;

        public db_sysuser(LabContext ctx)
        {
            _ctx = ctx;
        }

        #region 获取所有用户信息
        public IEnumerable<object> GetAllList()
        {
            try
            {
                var list = _ctx.SysUsers.Select(c => new
                {
                    Id = c.Id,
                    UserName = c.UserName,
                    LoginName = c.LoginName,
                    Email = c.Email,
                    Phone = c.Phone,
                    Sex = c.Sex,
                    AcademyId = c.CID,
                    Password = c.Password,
                    Academy = _ctx.Academys.Where(x=>x.Id == c.CID).Select(c=>c.Name).FirstOrDefault(),
                    Role = _ctx.UserRoles.Where(x => x.UID == c.Id).Select(c => c.Role.RoleName).FirstOrDefault(),
                    RoleId = _ctx.UserRoles.Where(x => x.UID == c.Id).Select(c => c.Role.Id).FirstOrDefault(),
                }).ToList();
                return list;
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

        #region 添加用户信息
        public bool AddUser(LAB.MODEL.DoTempClass.AddUser addUser)
        {
            using (var transaction = _ctx.Database.BeginTransaction())
            {
                try
                {
                    // 判断角色真实性
                    var CID = _ctx.Academys.Where(c => c.Id == addUser.CID).FirstOrDefault();
                    var RID = _ctx.Roles.Where(c => c.Id == addUser.RID).FirstOrDefault();
                    var user = _ctx.SysUsers.Where(c=>c.LoginName == addUser.LoginName).FirstOrDefault();
                    if (RID == null || CID==null) throw new Exception("服务器错误");// 如果角色不存在则判断该角色是个假数据
                    if (user != null) throw new Exception("此用户名已存在请重新创建");

                    var sysuser = new LAB.MODEL.SysUser();
                    sysuser.UserName = addUser.UserName;
                    sysuser.LoginName = addUser.LoginName;
                    sysuser.Email = addUser.Email;
                    sysuser.Password = addUser.Password;
                    sysuser.Phone = addUser.Phone;
                    sysuser.Sex = addUser.Sex;
                    sysuser.CID = addUser.CID;

                    _ctx.SysUsers.Add(sysuser);
                    _ctx.SaveChanges();
                    // 用户信息添加完毕 
                    // 处理角色用户关联

                    var userRole = new LAB.MODEL.UserRoles();
                    userRole.UID = sysuser.Id;
                    userRole.RID = addUser.RID;
                    _ctx.UserRoles.Add(userRole);
                    _ctx.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
        #endregion

        #region 获取角色列表
        public IEnumerable<object> GetRoleList()
        {
            try
            {
                var lsit = _ctx.Roles.ToList();
                return lsit;
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

        #region 修改用户信息
        public bool EditUser(LAB.MODEL.DoTempClass.AddUser addUser)
        {
            using (var transaction = _ctx.Database.BeginTransaction())
            {
                try
                {
                    // 判断是否是假数据
                    var taddUser = _ctx.SysUsers.Where(c => c.Id == addUser.Id).FirstOrDefault();
                    var CID = _ctx.Academys.Where(c => c.Id == addUser.CID).FirstOrDefault();
                    var RID = _ctx.Roles.Where(c => c.Id == addUser.RID).FirstOrDefault();
                    if (RID == null || CID == null || taddUser == null) throw new Exception();// 如果角色不存在则判断该角色是个假数据

                    taddUser.UserName = addUser.UserName;
                    taddUser.Password = addUser.Password;
                    taddUser.CID = addUser.CID;
                    taddUser.Email = addUser.Email;
                    taddUser.Phone = addUser.Phone;
                    taddUser.Sex = addUser.Sex;

                    _ctx.SaveChanges();

                    var userRole = _ctx.UserRoles.Where(c => c.RID == addUser.RID).FirstOrDefault();

                    if (userRole == null) throw new Exception();

                    userRole.RID = addUser.RID;

                    _ctx.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback(); return false;
                }
            }
        }
        #endregion

        #region 根据学院获取用户列表
        public IEnumerable<object> GetByAcademy(int CID)
        {
            try 
            {

                var list = (from user in _ctx.SysUsers
                            join role in _ctx.UserRoles on user.Id equals role.UID
                            where user.CID == CID && role.RID == 2
                            select user).ToList();
                return list;
            }
            catch 
            {
                throw new Exception();
            }
        }
        #endregion

        #region 获取所有维修人员
        public IEnumerable<object> GetRepairs()
        {
            try
            {
                var list = (from user in _ctx.SysUsers
                            join role in _ctx.UserRoles on user.Id equals role.UID
                            where role.RID == 3
                            select user).ToList();
                return list;
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

        #region 获取显示屏数据
        public object GetDash()
        {
            try 
            {
                var list =new{ bcount =  _ctx.SingleBuilding.Count(),
                    checkcoutn = _ctx.DailySafetyChecks.Count(),
                    usercount = _ctx.SysUsers.Count(), 
                    academycount = _ctx.Academys.Count()};
                return list;
            }
            catch 
            {
                throw new Exception();
            }
        }
        #endregion
    }
}
