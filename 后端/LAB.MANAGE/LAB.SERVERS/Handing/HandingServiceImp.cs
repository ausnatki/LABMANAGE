using LAB.REPOSITORY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.SERVERS
{
    public class HandingServiceImp:IHandingService
    {
        private readonly LAB.REPOSITORY.db_handing db_Handing;

        public HandingServiceImp(db_handing db_Handing)
        {
            this.db_Handing = db_Handing;
        }

        #region 分配维修人员
        public bool AssignRepair(int HID, int UID)
        {
            try
            {
                return db_Handing.AssignRepair(HID, UID);
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 获取所有异常信息列表
        public IEnumerable<object> GetAllHanding()
        {
            try 
            { 
                return db_Handing.GetAllHanding();
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

        #region 通过实验室管理人员获取异常列表
        public IEnumerable<object> GetHandingByUser(int UID)
        {
            try 
            {
                return db_Handing.GetHandingByUser(UID);
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

    }
}
