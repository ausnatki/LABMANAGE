using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.SERVERS
{
    public interface ILaboratoriesService
    {
        public IEnumerable<object> GetAllList();

        public bool AddLab(LAB.MODEL.DoTempClass.LaboratoriesAdd laboratoriesAdd);

        public bool EditLab(LAB.MODEL.Laboratories laboratories);

        public bool ChangeState(int LID);
    }
}
