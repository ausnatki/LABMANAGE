using LAB.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.REPOSITORY
{
    public class db_repairs
    {
        private readonly LAB.DB.LabContext _ctx;

        public db_repairs(LabContext ctx)
        {
            _ctx = ctx;
        }

        #region 获取所有维修信息
        public IEnumerable<object> GetAllRepairs()
        {
            try 
            {
                var list = _ctx.LabEquipmentRepairs.Select(c => new
                {
                    Id = c.Id,
                    DLabID = c.DLabID,
                    LabNumber = c.dailySafetyChecks.Laboratories.LabNumber,
                    RepairDate = c.RepairDate,
                    IssuesFound = c.IssuesFound,
                    RepairPersonnelID = c.RepairPersonnelID,
                    RepairName = _ctx.SysUsers.Where(x => x.Id == c.RepairPersonnelID).Select(c => c.UserName).FirstOrDefault(),
                    ComletionStatus = c.ComletionStatus,
                    Remarks = c.Remarks,
                }).ToList();
                return list;
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

        #region 通过维修人员获取危获取维修信息
        public IEnumerable<object> GetRepairsByUser(int UID)
        {
            try
            {
                var list = _ctx.LabEquipmentRepairs.Where(c=>c.RepairPersonnelID== UID).Select(c => new
                {
                    Id = c.Id,
                    DLabID = c.DLabID,
                    LabNumber = c.dailySafetyChecks.Laboratories.LabNumber,
                    RepairDate = c.RepairDate,
                    IssuesFound = c.IssuesFound,
                    RepairPersonnelID = c.RepairPersonnelID,
                    RepairName = _ctx.SysUsers.Where(x => x.Id == c.RepairPersonnelID).Select(c => c.UserName).FirstOrDefault(),
                    ComletionStatus = c.ComletionStatus,
                    Remarks = c.Remarks,
                }).ToList();
                return list;
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

        #region 填写维修信息
        public bool UpdataRepairs(LAB.MODEL.LabEquipmentRepairs repairs)
        {
            using (var transaction = _ctx.Database.BeginTransaction())
            {
                try
                {
                    // 验证数据真实性
                    var tr = _ctx.LabEquipmentRepairs.Where(c => c.Id == repairs.Id).FirstOrDefault();
                    if (tr == null) return false;

                    tr.RepairDate = repairs.RepairDate;
                    tr.Suggestions = repairs.Suggestions;
                    tr.ComletionStatus = repairs.ComletionStatus;
                    tr.Remarks = repairs.Remarks;
                    tr.ComletionStatus = repairs.ComletionStatus;

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
