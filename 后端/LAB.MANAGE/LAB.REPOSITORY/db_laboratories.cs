using LAB.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LAB.REPOSITORY
{
    public class db_laboratories
    {
        private readonly LAB.DB.LabContext _ctx;

        public db_laboratories(LabContext ctx)
        {
            _ctx = ctx;
        }

        #region 获取所有实验室信息
        public IEnumerable<object> GetAllList()
        {
            try
            {
                var list = _ctx.Laboratories.Select(c => new
                {
                    Id = c.Id,
                    Name = c.LabNumber, //实验室号码
                    Builder = _ctx.Floor
                                        .Where(x => x.Id == c.FloorId)
                                        .Select(x => _ctx.SingleBuilding
                                        .Where(d => d.Id == x.BuildingId)
                                        .Select(b => b.Name)
                                        .FirstOrDefault()), // 所属楼房
                    Academy = _ctx.Academys.Where(o => o.Id == c.AcademyId).Select(c => c.Name).FirstOrDefault(),// 所属院校
                    AcademyId = c.AcademyId, //所属院校id
                    IsDel = c.IsDel, // 状态
                    ManageId = c.UID, // 管理员id
                    ManagerName = _ctx.SysUsers.Where(o => o.Id == c.UID).Select(c => c.UserName).FirstOrDefault() // 管理员姓名
                }).ToList();
                return list;
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

        #region 添加实验室信息
        public bool AddLab(LAB.MODEL.DoTempClass.LaboratoriesAdd laboratoriesAdd)
        {
            using(var transaction = _ctx.Database.BeginTransaction())
            {
                try 
                {
                    LAB.MODEL.Laboratories laboratories = new MODEL.Laboratories();
                    laboratories.UID = laboratoriesAdd.UID;
                    laboratories.AcademyId = laboratoriesAdd.AcademyId;
                    laboratories.IsDel = false;
                    laboratories.LabNumber = laboratoriesAdd.LabNumber;
                    laboratories.FloorId = laboratoriesAdd.FloorId;
                    _ctx.Laboratories.Add(laboratories);
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

        #region 修改实验室信息
        public bool EditLab(LAB.MODEL.Laboratories laboratories)
        {
            using(var transaction = _ctx.Database.BeginTransaction())
            {
                try 
                {
                    // 首先判断数据的真实性
                    var tlaboratories = _ctx.Laboratories.Where(c => c.Id == laboratories.Id).FirstOrDefault();
                    if (tlaboratories == null) return false;
                    tlaboratories.UID = laboratories.UID;
                    tlaboratories.LabNumber = laboratories.LabNumber;
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

        #region 修改实验室状态
        public bool ChangeState(int LID)
        {
            using(var transaction = _ctx.Database.BeginTransaction())
            {
                try 
                {
                    // 首先判断输入数据的真实性
                    var tLab =  _ctx.Laboratories.Where(c => c.Id == LID).FirstOrDefault();
                    if(tLab == null) return false;

                    tLab.IsDel = !tLab.IsDel;
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
    }
}
