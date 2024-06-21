using LAB.DB;
using LAB.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LAB.REPOSITORY
{
    public class db_handing
    {
        private readonly LAB.DB.LabContext _ctx;

        public db_handing(LabContext ctx)
        {
            _ctx = ctx;
        }

        #region 获取所有异常信息列表
        public IEnumerable<object> GetAllHanding()
        {
            try 
            {
                var list = _ctx.labInclidentHandings.Select(c => new
                {
                    LabNumber = c.dailySafetyChecks.Laboratories.LabNumber,
                    Id = c.Id,
                    IncidentDetails = c.IncidentDetails, // 异常错误信息
                    RepairPersonnelID = c.RepairPersonnelID,
                    RepairName = _ctx.SysUsers.Where(x => x.Id == c.RepairPersonnelID).Select(c => c.UserName).FirstOrDefault(),
                    ReportedByID = c.ReportedByID,
                    ReportedName = _ctx.SysUsers.Where(x => x.Id == c.ReportedByID).Select(c => c.UserName).FirstOrDefault(),
                    InclidentTime = c.InclidentTime,
                    Semesters = c.dailySafetyChecks.Semesters.Name
                }).ToList();
                return list;
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

        #region 通过实验室管理人员获取异常信息列表
        public IEnumerable<object> GetHandingByUser(int UID)
        {
            try
            {
                var list = _ctx.labInclidentHandings.Where(c=>c.ReportedByID == UID).Select(c => new
                {
                    LabNumber = c.dailySafetyChecks.Laboratories.LabNumber,
                    Id = c.Id,
                    IncidentDetails = c.IncidentDetails, // 异常错误信息
                    RepairPersonnelID = c.RepairPersonnelID,
                    RepairName = _ctx.SysUsers.Where(x => x.Id == c.RepairPersonnelID).Select(c => c.UserName).FirstOrDefault(),
                    ReportedByID = c.ReportedByID,
                    ReportedName = _ctx.SysUsers.Where(x => x.Id == c.ReportedByID).Select(c => c.UserName).FirstOrDefault(),
                    InclidentTime = c.InclidentTime
                }).ToList();
                return list;
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

        #region 分配维修人员
        public bool AssignRepair(int HID,int UID)
        {
            using (var transaction = _ctx.Database.BeginTransaction())
            {
                try
                {
                    // 验证数据真实性
                    var th = _ctx.labInclidentHandings.Where(c => c.Id == HID).FirstOrDefault();
                    var tu = _ctx.SysUsers.Where(c => c.Id == UID).FirstOrDefault();
                    if (th == null || tu == null) return false;

                    th.RepairPersonnelID = UID; // 设置维修表的人员

                    _ctx.SaveChanges();
                    LAB.MODEL.LabEquipmentRepairs repairs = new MODEL.LabEquipmentRepairs();

                    repairs.DLabID = th.DLabID;
                    repairs.IssuesFound = th.IncidentDetails;
                    repairs.RepairPersonnelID = th.RepairPersonnelID;
                    repairs.IsDel = false;
                    repairs.ComletionStatus = false;

                    _ctx.LabEquipmentRepairs.Add(repairs);

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
