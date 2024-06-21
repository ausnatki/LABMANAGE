using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.SERVERS
{
    public interface IRepairsService
    {
        public IEnumerable<object> GetAllRepairs();
        public IEnumerable<object> GetRepairsByUser(int UID);
        public bool UpdataRepairs(LAB.MODEL.LabEquipmentRepairs repairs);
    }
}
