using LAB.DB;
using LAB.MODEL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LAB.REPOSITORY
{
    public class db_dailySafetyCheck
    {
        private LAB.DB.LabContext _ctx;

        public db_dailySafetyCheck(LabContext ctx)
        {
            _ctx = ctx;
        }

        #region 获取所有日志记录
        public IEnumerable<object> GetAllDailyCheck() 
        {
            try
            {
                var list = _ctx.DailySafetyChecks.Select(c => new
                {
                    Id = c.Id,
                    LabID = c.LabID,
                    SemesterID = c.SemesterID,
                    Semester = c.Semesters.Name,
                    UID = c.UID,
                    CheckDate = c.CheckDate,
                    Cleanliness = c.Cleanliness,
                    DoorsAndWindows = c.DoorsAndWindows,
                    ElectricalLines = c.ElectricalLines,
                    FireSafetyEquipmentAvailable = c.FireSafetyEquipmentAvailable,
                    FireSafetyEquipmentExpiry = c.FireSafetyEquipmentExpiry,
                    InstrumentEquipmentIntact = c.InstrumentEquipmentIntact,
                    InstrumentEquipmentWorking = c.InstrumentEquipmentWorking,
                    InstrumentWarningLabelsIntact = c.InstrumentWarningLabelsIntact,
                    OtherHazards = c.OtherHazards,
                    OtherSafetyHazards = c.OtherSafetyHazards,
                    IsDel = c.IsDel,
                    State = c.State,
                    LabNumber = c.Laboratories.LabNumber,
                    ManagerName = c.SysUser.UserName
                }).ToList();
                return list;
            }
            catch 
            {
                throw new Exception();
            }
        }
        #endregion

        #region 获取实验室管理人员的日志记录
        public IEnumerable<object> GetDailyCheckByUser(int UID)
        {
            try 
            {
                // 判断数据的真实性
                var user = _ctx.SysUsers.Where(c => c.Id == UID).FirstOrDefault();
                if(user == null) throw new Exception();

                var list = _ctx.DailySafetyChecks.Where(c=>c.UID == UID).Select(c => new
                {
                    Id = c.Id,
                    LabID = c.LabID,
                    SemesterID = c.SemesterID,
                    Semester = c.Semesters.Name,
                    UID = c.UID,
                    CheckDate = c.CheckDate,
                    Cleanliness = c.Cleanliness,
                    DoorsAndWindows = c.DoorsAndWindows,
                    ElectricalLines = c.ElectricalLines,
                    FireSafetyEquipmentAvailable = c.FireSafetyEquipmentAvailable,
                    FireSafetyEquipmentExpiry = c.FireSafetyEquipmentExpiry,
                    InstrumentEquipmentIntact = c.InstrumentEquipmentIntact,
                    InstrumentEquipmentWorking = c.InstrumentEquipmentWorking,
                    InstrumentWarningLabelsIntact = c.InstrumentWarningLabelsIntact,
                    OtherHazards = c.OtherHazards,
                    OtherSafetyHazards = c.OtherSafetyHazards,
                    IsDel = c.IsDel,
                    State = c.State,
                    LabNumber = c.Laboratories.LabNumber,
                    ManagerName = c.SysUser.UserName
                }).ToList();
                return list;

            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

        #region 通过实验室id查询日志检查记录
        public IEnumerable<object> GetByLab(int LID)
        {
            try
            {
                var list = _ctx.DailySafetyChecks.Where(c => c.LabID == LID && c.IsDel == false).Select(c => new
                {
                    Id = c.Id,
                    LabID = c.LabID,
                    SemesterID = c.SemesterID,
                    Semester = c.Semesters.Name,
                    UID = c.UID,
                    CheckDate = c.CheckDate,
                    Cleanliness = c.Cleanliness,
                    DoorsAndWindows = c.DoorsAndWindows,
                    ElectricalLines = c.ElectricalLines,
                    FireSafetyEquipmentAvailable = c.FireSafetyEquipmentAvailable,
                    FireSafetyEquipmentExpiry = c.FireSafetyEquipmentExpiry,
                    InstrumentEquipmentIntact = c.InstrumentEquipmentIntact,
                    InstrumentEquipmentWorking = c.InstrumentEquipmentWorking,
                    InstrumentWarningLabelsIntact = c.InstrumentWarningLabelsIntact,
                    OtherHazards = c.OtherHazards,
                    OtherSafetyHazards = c.OtherSafetyHazards,
                    IsDel = c.IsDel,
                    State = c.State,
                    LabNumber = c.Laboratories.LabNumber,
                    ManagerName = c.SysUser.UserName
                }).ToList();
                return list;
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

        #region 通过实验室获取维修记录
        public IEnumerable<object> GetRepairsByLab(int LID)
        {
            try
            {
                // 判断数据真实性
                var t = _ctx.Laboratories.Where(c => c.Id == LID).FirstOrDefault();
                if(t == null) throw new Exception();

                var list = _ctx.LabEquipmentRepairs.Where(c => c.dailySafetyChecks.LabID == LID && c.IsDel == false).Select(c => new
                {
                    Id = c.Id,
                    DLabId = c.DLabID,
                    RepairDate = c.RepairDate, // 维修时间
                    IssuesFound = c.IssuesFound, // 错误信息
                    Suggestions = c.Suggestions !=null?c.Suggestions:"无",// 维修建议
                    RepairPersonnelId = c.RepairPersonnelID, // 维修人员
                    ComletionStatus = c.ComletionStatus, // 是否完成
                    Remark = c.Remarks, // 备注
                    IsDel = c.IsDel,// 是否删除
                    RepairPersonnelName = _ctx.SysUsers.Where(x => x.Id == c.RepairPersonnelID).Select(c => c.UserName).FirstOrDefault(),
                    LabName = c.dailySafetyChecks.Laboratories.LabNumber
                }).ToList();
                return list;
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

        #region 通过实验室获取异常记录
        public IEnumerable<object> GetHandingByLab(int LID) 
        {
            try 
            {
                var list = _ctx.labInclidentHandings.Where(c => c.dailySafetyChecks.LabID == LID && c.IsDel == false).Select(c => new
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

        #region 修改日志状态
        public bool ChangeCheck(int DID)
        {
            using (var transaction = _ctx.Database.BeginTransaction())
            {
                try
                {
                    // 判断数据真实性
                    var t = _ctx.DailySafetyChecks.Where(c => c.Id == DID).FirstOrDefault();
                    if (t == null) return false;

                    t.IsDel = !t.IsDel;
                    _ctx.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        #endregion

        #region 添加日志
        public bool AddCheck(LAB.MODEL.DailySafetyCheck check)
        {
            using (var transaction = _ctx.Database.BeginTransaction())
            {
                try
                {
                    List<LAB.MODEL.LabInclidentHanding> handings = new List<MODEL.LabInclidentHanding>();
                    check.State = true;
                    _ctx.DailySafetyChecks.Add(check);
                    _ctx.SaveChanges(); // 先保存check，确保check.Id生成

                    #region 判断异常信息
                    if (!check.Cleanliness)
                    {
                        var handing = new LAB.MODEL.LabInclidentHanding
                        {
                            DLabID = check.Id,
                            InclidentTime = DateTime.Now,
                            IsDel = false,
                            ReportedByID = check.UID,
                            IncidentDetails = "卫生"
                        };
                        handings.Add(handing);
                        check.State = false;
                    }
                    if (!check.DoorsAndWindows)
                    {
                        var handing = new LAB.MODEL.LabInclidentHanding
                        {
                            DLabID = check.Id,
                            InclidentTime = DateTime.Now,
                            IsDel = false,
                            ReportedByID = check.UID,
                            IncidentDetails = "门窗"
                        };
                        handings.Add(handing);
                        check.State = false;
                    }
                    if (!check.ElectricalLines)
                    {
                        var handing = new LAB.MODEL.LabInclidentHanding
                        {
                            DLabID = check.Id,
                            InclidentTime = DateTime.Now,
                            IsDel = false,
                            ReportedByID = check.UID,
                            IncidentDetails = "电器线路"
                        };
                        handings.Add(handing);
                        check.State = false;
                    }
                    if (!check.FireSafetyEquipmentAvailable)
                    {
                        var handing = new LAB.MODEL.LabInclidentHanding
                        {
                            DLabID = check.Id,
                            InclidentTime = DateTime.Now,
                            IsDel = false,
                            ReportedByID = check.UID,
                            IncidentDetails = "消防设备不能使用"
                        };
                        handings.Add(handing);
                        check.State = false;
                    }
                    if (!check.FireSafetyEquipmentExpiry)
                    {
                        var handing = new LAB.MODEL.LabInclidentHanding
                        {
                            DLabID = check.Id,
                            InclidentTime = DateTime.Now,
                            IsDel = false,
                            ReportedByID = check.UID,
                            IncidentDetails = "消防设备异常"
                        };
                        handings.Add(handing);
                        check.State = false;
                    }
                    if (!check.InstrumentEquipmentIntact)
                    {
                        var handing = new LAB.MODEL.LabInclidentHanding
                        {
                            DLabID = check.Id,
                            InclidentTime = DateTime.Now,
                            IsDel = false,
                            ReportedByID = check.UID,
                            IncidentDetails = "仪器设备异常"
                        };
                        handings.Add(handing);
                        check.State = false;
                    }
                    if (!check.InstrumentEquipmentWorking)
                    {
                        var handing = new LAB.MODEL.LabInclidentHanding
                        {
                            DLabID = check.Id,
                            InclidentTime = DateTime.Now,
                            IsDel = false,
                            ReportedByID = check.UID,
                            IncidentDetails = "仪器设备不能使用"
                        };
                        handings.Add(handing);
                        check.State = false;
                    }
                    if (!check.InstrumentWarningLabelsIntact)
                    {
                        var handing = new LAB.MODEL.LabInclidentHanding
                        {
                            DLabID = check.Id,
                            InclidentTime = DateTime.Now,
                            IsDel = false,
                            ReportedByID = check.UID,
                            IncidentDetails = "仪器设备警示标志异常"
                        };
                        handings.Add(handing);
                        check.State = false;
                    }
                    if (check.OtherHazards != "无" && !string.IsNullOrEmpty(check.OtherHazards))
                    {
                        var handing = new LAB.MODEL.LabInclidentHanding
                        {
                            DLabID = check.Id,
                            InclidentTime = DateTime.Now,
                            IsDel = false,
                            ReportedByID = check.UID,
                            IncidentDetails = check.OtherHazards
                        };
                        handings.Add(handing);
                        check.State = false;
                    }
                    if (check.OtherSafetyHazards != "无" && !string.IsNullOrEmpty(check.OtherSafetyHazards))
                    {
                        var handing = new LAB.MODEL.LabInclidentHanding
                        {
                            DLabID = check.Id,
                            InclidentTime = DateTime.Now,
                            IsDel = false,
                            ReportedByID = check.UID,
                            IncidentDetails = check.OtherSafetyHazards
                        };
                        handings.Add(handing);
                        check.State = false;
                    }
                    #endregion

                    _ctx.labInclidentHandings.AddRange(handings);
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

        #region 指派维修人员
        public bool Assignment(int DHLID,int UID)
        {
            using(var transaction = _ctx.Database.BeginTransaction ()) 
            {
                try 
                {
                    // 传递来的是异常检查表 和维修人员id
                    // 验证数据的真实性
                    var tlab = _ctx.labInclidentHandings.Where(c=>c.Id  == DHLID).FirstOrDefault();
                    var tuser = _ctx.SysUsers.Where(c=>c.Id == UID).FirstOrDefault();
                    if (tlab == null && tuser == null) return false;

                    tlab.RepairPersonnelID = UID;
                    // 添加维修表
                    LAB.MODEL.LabEquipmentRepairs reparie = new MODEL.LabEquipmentRepairs();
                    reparie.DLabID = tlab.DLabID;
                    reparie.IssuesFound = tlab.IncidentDetails;
                    reparie.Suggestions = "";
                    reparie.RepairPersonnelID = UID;
                    reparie.ComletionStatus = false;
                    reparie.Remarks = "";
                    reparie.IsDel = false;
                    // 维修时间没有填写

                    _ctx.labInclidentHandings.Add(tlab);
                    _ctx.SaveChanges ();
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    return false;
                }
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

        #region 通过日志记录获取异常信息
        public IEnumerable<object> GetHandingByDaily(int DLID)
        {
            try 
            {
                var list = _ctx.labInclidentHandings.Where(c=>c.DLabID == DLID).Select(c => new
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

        #region 通过日志记录获取维修信息
        public IEnumerable<object> GetRepairByDaily(int DLID)
        {
            try
            {
                var list = _ctx.LabEquipmentRepairs.Where(c => c.DLabID == DLID).Select(c => new
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

        #region 实验室日志页面的初始信息
        public IEnumerable<object> GetNotifyInitdata(int UID)
        {
            try
            {
                // 获取当前日期
                DateTime today = DateTime.Today;

                // 查询实验室管理人员管理的所有实验室
                var managedLabs = _ctx.Laboratories
                    .Where(lab => lab.LabAssignments.Any(assignment => assignment.UserID == UID) && !lab.IsDel)
                    .ToList();

                // 查询今天的日志记录
                var todayChecks = _ctx.DailySafetyChecks
                    .Where(check => check.UID == UID && check.CheckDate.Date == today && !check.IsDel)
                    .Select(check => check.LabID)
                    .ToList();

                // 查询没有检查的实验室
                var uncheckedLabs = managedLabs
                    .Where(lab => !todayChecks.Contains(lab.Id))
                    .Select(lab => new
                    {
                        LabName = lab.LabNumber,
                        LabID = lab.Id
                    })
                    .ToList();

                return uncheckedLabs;
            }
            catch (Exception ex)
            {
                // 捕获并处理异常，可以记录日志或其他处理方式
                throw new Exception("Error occurred while retrieving data: " + ex.Message);
            }
        }
        #endregion
    }
}
