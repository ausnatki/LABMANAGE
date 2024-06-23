using LAB.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LAB.REPOSITORY
{
    public class db_builder
    {
        private readonly LAB.DB.LabContext _ctx;

        public db_builder(LabContext ctx)
        {
            _ctx = ctx;
        }

        #region 获取全部列表
        public IEnumerable<object> GetAllList()
        {
            try
            {
                var list = _ctx.SingleBuilding.Select(c => new
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsDel = c.IsDel,
                    Number = c.Number,
                    Lng = c.Lng,
                    Lat = c.Lat,
                }).ToList();
                return list;
            }
            catch 
            {
                throw new Exception();
            }
        }
        #endregion

        #region 添加房楼
        public bool AddBuilder(LAB.MODEL.SingleBuilding singleBuilding)
        {
            using(var transaction = _ctx.Database.BeginTransaction()) 
            {
                try 
                {
                    // 检查数据真实性
                    if(singleBuilding.Name == null) { return false; }
                    _ctx.SingleBuilding.Add(singleBuilding);
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

        #region 修改房楼信息
        public bool EditBuilder(LAB.MODEL.SingleBuilding editing) 
        {
            using(var transaction = _ctx.Database.BeginTransaction())
            {
                try 
                {
                    // 检查数据真实性
                    var tdata= _ctx.SingleBuilding.Where(c=>c.Id == editing.Id).FirstOrDefault();
                    if (tdata == null) return false;
                    tdata.Name = editing.Name;
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

        #region 修改房楼状态
        public bool ChangeState(int BID) 
        {
            using(var transaction = _ctx.Database.BeginTransaction()) 
            {
                try 
                {
                    //验证数据真实性
                    var builder = _ctx.SingleBuilding.Where(c=>c.Id == BID).FirstOrDefault();
                    if (builder == null) return false;
                    builder.IsDel = !builder.IsDel;
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
