using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.SERVERS
{
    public interface ISysUserService
    {
        public IEnumerable<object> GetAllList();
        public bool AddUser(LAB.MODEL.DoTempClass.AddUser addUser);
        public IEnumerable<object> GetRoleList();
        public bool EditUser(LAB.MODEL.DoTempClass.AddUser doUser);

        public IEnumerable<object> GetByAcademy(int CID);
    }
}
