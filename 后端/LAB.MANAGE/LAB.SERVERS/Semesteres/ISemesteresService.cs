using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.SERVERS
{
    public interface ISemesteresService
    {
        public IEnumerable<object> GetAllList();
        public bool AddSemesteres(LAB.MODEL.Semesters semesters);

        public bool EditSemesteres(LAB.MODEL.Semesters semesters);

        public IEnumerable<object> GetCheckList();
    }
}
