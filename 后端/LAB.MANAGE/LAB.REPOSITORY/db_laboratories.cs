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
                var list = (from c in _ctx.Laboratories
                            join f in _ctx.Floor on c.FloorId equals f.Id
                            join b in _ctx.SingleBuilding on f.BuildingId equals b.Id
                            join a in _ctx.Academys on c.AcademyId equals a.Id
                            join la in _ctx.LabAssignments on c.Id equals la.LabID into laGroup
                            from la in laGroup.DefaultIfEmpty()
                            join su in _ctx.SysUsers on la.UserID equals su.Id into suGroup
                            from su in suGroup.DefaultIfEmpty()
                            select new
                            {
                                Id = c.Id,
                                Name = c.LabNumber, // 实验室号码
                                Builder = b.Name, // 所属楼房
                                Academy = a.Name, // 所属院校
                                AcademyId = c.AcademyId, // 所属院校ID
                                IsDel = c.IsDel, // 状态
                                ManageId = (int?)la.UserID, // 管理员ID
                                ManagerName = su == null ? "无" : su.UserName // 管理员姓名
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
                    // 验证数据真实性
                    var list = _ctx.Floor.Where(c => c.Id == laboratoriesAdd.FloorId).FirstOrDefault();
                    if (list == null) return false;
                    var cnt = _ctx.Laboratories.Where(c => c.FloorId == list.Id).Count();

                    LAB.MODEL.Laboratories laboratories = new MODEL.Laboratories();
                    //laboratories.UID = laboratoriesAdd.UID;
                    laboratories.AcademyId = laboratoriesAdd.AcademyId; // 学院id
                    laboratories.IsDel = false; // 状态
                    laboratories.LabNumber = laboratoriesAdd.LabNumber+(list.Number+cnt+1).ToString();
                    laboratories.FloorId = laboratoriesAdd.FloorId; // 楼层id
                    _ctx.Laboratories.Add(laboratories);
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
                    tlaboratories.LabNumber = laboratories.LabNumber;
                    tlaboratories.IsDel = !laboratories.IsDel;
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

        #region 查看指定楼层的实验室
        public IEnumerable<object> GetByFloor(int FID)
        {
            try 
            {
                var list = (from c in _ctx.Laboratories
                            join f in _ctx.Floor on c.FloorId equals f.Id
                            join b in _ctx.SingleBuilding on f.BuildingId equals b.Id
                            join a in _ctx.Academys on c.AcademyId equals a.Id
                            join la in _ctx.LabAssignments on c.Id equals la.LabID into laGroup
                            from la in laGroup.DefaultIfEmpty()
                            join su in _ctx.SysUsers on la.UserID equals su.Id into suGroup
                            from su in suGroup.DefaultIfEmpty()
                            where c.FloorId == FID
                            select new
                            {
                                Id = c.Id,
                                Name = c.LabNumber, // 实验室号码
                                Builder = b.Name, // 所属楼房
                                Academy = a.Name, // 所属院校
                                AcademyId = c.AcademyId, // 所属院校ID
                                IsDel = c.IsDel, // 状态
                                ManageId = (int?)la.UserID, // 管理员ID
                                ManagerName = su == null ? "无" : su.UserName // 管理员姓名
                            }).ToList();


                return list;

            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

        #region 分配实验室管理人员
        public bool AssignMent(int LID,int UID)
        {
            using(var transaction = _ctx.Database.BeginTransaction())
            {
                try 
                {
                    // 验证数据的真实性
                    var tl = _ctx.Laboratories.Where(c => c.Id == LID).FirstOrDefault();
                    var tu = _ctx.SysUsers.Where(c=>c.Id == UID).FirstOrDefault();
                    if (tl == null || tu == null) return false;
                    
                    // 先查找分配表如果没有的话 就执行添加逻辑
                    var ta = _ctx.LabAssignments.Where(c=>c.LabID == LID).FirstOrDefault();
                    if(ta == null)
                    {
                        LAB.MODEL.LabAssignments labAssignments= new LAB.MODEL.LabAssignments();
                        labAssignments.LabID = LID;
                        labAssignments.UserID = UID;
                        _ctx.LabAssignments.Add(labAssignments);
                        _ctx.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    else
                    {
                        ta.UserID = UID;
                        _ctx.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
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
