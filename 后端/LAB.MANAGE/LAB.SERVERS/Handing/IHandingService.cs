using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.SERVERS
{
    public interface IHandingService
    {
        public IEnumerable<object> GetAllHanding();
        public IEnumerable<object> GetHandingByUser(int UID);
        public bool AssignRepair(int HID, int UID);
    }
}
