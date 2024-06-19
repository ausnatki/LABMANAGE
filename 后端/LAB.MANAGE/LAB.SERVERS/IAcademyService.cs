using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.SERVERS
{
    public interface IAcademyService
    {
        public IEnumerable<object> GetList();
        public bool AddAcademy(LAB.MODEL.Academy academy);
        public IEnumerable<object> GetSelectCheckList();
        public bool ChangeState(int CID);
        public bool Edit(LAB.MODEL.Academy academy);
    }
}
