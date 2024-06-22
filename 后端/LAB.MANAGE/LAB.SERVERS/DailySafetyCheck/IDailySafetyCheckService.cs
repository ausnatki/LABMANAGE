using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.SERVERS
{
    public interface IDailySafetyCheckService
    {
        public IEnumerable<object> GetByLab(int LID);

        public IEnumerable<object> GetRepairsByLab(int LID);

        public IEnumerable<object> GetAllDailyCheck();

        public IEnumerable<object> GetHandingByLab(int LID);

        public bool ChangeCheck(int DID);

        public bool AddCheck(LAB.MODEL.DailySafetyCheck check);
        public bool Assignment(int DHLID, int UID);
        public bool UpdataRepairs(LAB.MODEL.LabEquipmentRepairs repairs);

        public IEnumerable<object> GetHandingByDaily(int DLID);

        public IEnumerable<object> GetRepairByDaily(int DLID);

        public IEnumerable<object> GetDailyCheckByUser(int UID);

        public IEnumerable<object> GetNotifyInitdata(int UID);
    }
}
