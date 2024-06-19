using LAB.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LAB.REPOSITORY
{
    public class db_floor
    {
        private readonly LAB.DB.LabContext _ctx;

        public db_floor(LabContext ctx)
        {
            _ctx = ctx;
        }

        #region 获取所有楼层信息
        public IEnumerable<object> GetAllList()
        {
            try
            {
                var list = _ctx.Floor.Select(c => new
                {
                    Id = c.Id,
                    Number = c.Number,
                    Builder = _ctx.SingleBuilding.Where(x => x.Id == c.BuildingId).Select(x => x.Name).FirstOrDefault(),
                    IsDel = c.IsDel
                }).ToList();
                return list;

            }
            catch 
            {
                throw new Exception();
            }
        }
        #endregion

        #region 修改楼层信息
        public bool EditFloor(LAB.MODEL.Floor floor)
        {
            using(var transaction = _ctx.Database.BeginTransaction())
            {
                try 
                {
                    // 判断数据的真实性
                    var tfloor = _ctx.Floor.Where(c => c.Id == floor.Id).FirstOrDefault();
                    if(tfloor == null) return false;
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

        #region 修改楼层状态
        public bool ChangeState(int FID)
        {
            using(var transaction = _ctx.Database.BeginTransaction())
            {
                try 
                {
                    // 首先判断数据的真实性
                    var tfloor = _ctx.Floor.Where(c=>c.Id == FID).FirstOrDefault();
                    if(tfloor == null) return false;
                    tfloor.IsDel = !tfloor.IsDel;
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

        #region 添加楼层
        public bool AddFloor(int BID)
        {
            using(var transaction = _ctx.Database.BeginTransaction())
            {
                try 
                {
                    // 判断数据真实性
                    var tBuilder = _ctx.SingleBuilding.Where(c=>c.Id == BID).FirstOrDefault();
                    if(tBuilder == null) return false; 
                    LAB.MODEL.Floor floor  = new MODEL.Floor();
                    floor.Number = tBuilder.Number + 1;
                    floor.BuildingId = BID;
                    floor.IsDel = false;
                    _ctx.Floor.Add(floor);
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
    }
}
