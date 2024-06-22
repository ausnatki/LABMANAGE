using LAB.MODEL;
using LAB.REPOSITORY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LAB.SERVERS
{
    public class DailySafetyCheckServiceImp : IDailySafetyCheckService
    {
        private readonly LAB.REPOSITORY.db_dailySafetyCheck db_DailySafetyCheck;

        public DailySafetyCheckServiceImp(db_dailySafetyCheck db_DailySafetyCheck)
        {
            this.db_DailySafetyCheck = db_DailySafetyCheck;
        }

        #region 通过实验室id获取日志记录
        public IEnumerable<object> GetByLab(int LID)
        {
            try
            {
                return db_DailySafetyCheck.GetByLab(LID);
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

        #region 通过实验室id获取异常记录
        public IEnumerable<object> GetRepairsByLab(int LID)
        {
            try
            {
                return db_DailySafetyCheck.GetRepairsByLab(LID);
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

        #region 通过实验室id获取维修记录
        public IEnumerable<object> GetHandingByLab(int LID)
        {
            try
            {
                return db_DailySafetyCheck.GetHandingByLab(LID);
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

        #region 获取所有日常日志检查信息
        public IEnumerable<object> GetAllDailyCheck()
        {
            try
            {
                return db_DailySafetyCheck.GetAllDailyCheck();
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

        #region 改变日志状态
        public bool ChangeCheck(int DID)
        {
            try
            {
                return db_DailySafetyCheck.ChangeCheck(DID);
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 添加检查日志
        public bool AddCheck(DailySafetyCheck check)
        {
            try
            {
                return db_DailySafetyCheck.AddCheck(check);
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 添加维修人员分配
        public bool Assignment(int DHLID, int UID)
        {
            try
            {
                return db_DailySafetyCheck.Assignment(DHLID, UID);
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 填写维修记录
        public bool UpdataRepairs(LabEquipmentRepairs repairs)
        {
            try
            {
                return db_DailySafetyCheck.UpdataRepairs(repairs);
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 通过日志记录获取维修记录
        public IEnumerable<object> GetRepairByDaily(int DLID)
        {
            try
            {
                return db_DailySafetyCheck.GetRepairByDaily(DLID);
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

        #region 通过日志记录获取异常信息
        public IEnumerable<object> GetHandingByDaily(int DLID)
        {
            try
            {
                return db_DailySafetyCheck.GetHandingByDaily(DLID);
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

        #region 获取实验室管理人员的日志记录表
        public IEnumerable<object> GetDailyCheckByUser(int UID)
        {
            try 
            {
                return db_DailySafetyCheck.GetDailyCheckByUser(UID);
            }
            catch 
            {
                throw new Exception();
            }
        }
        #endregion

        #region 获取通知消息
        public IEnumerable<object> GetNotifyInitdata(int UID)
        {
            try
            {
                return db_DailySafetyCheck.GetNotifyInitdata(UID);
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion
    }
}
