using LAB.DB;
using LAB.MODEL.DoTempClass;
using LAB.REPOSITORY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LAB.SERVERS
{
    public class SysUserServiceImp : ISysUserService
    {
        private LAB.REPOSITORY.db_sysuser db_Sysuser;
        public SysUserServiceImp(db_sysuser db_Sysuser)
        {
            this.db_Sysuser = db_Sysuser;
        }

        #region 用户添加
        public bool AddUser(LAB.MODEL.DoTempClass.AddUser addUser)
        {
            try
            {
                return db_Sysuser.AddUser(addUser);
            }
            catch
            {
                return true;
            }
        }


        #endregion

        #region 获取所有用户信息列表
        public IEnumerable<object> GetAllList()
        {
            try
            {
                return db_Sysuser.GetAllList();
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

        #region 获取所有角色列表
        public IEnumerable<object> GetRoleList()
        {
            try
            {
                return db_Sysuser.GetRoleList();
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

        #region 修改用户信息
        public bool EditUser(AddUser doUser)
        {
            try
            {
                return db_Sysuser.EditUser(doUser);

            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 根据学院查找人员
        public IEnumerable<object> GetByAcademy(int CID)
        {
            try
            {
                return db_Sysuser.GetByAcademy(CID);
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
                return db_Sysuser.GetRepairs();
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

    }
}
