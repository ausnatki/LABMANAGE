using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.SERVERS
{
    public interface IBuilderService
    {
        public IEnumerable<object> GetAllList();
        public bool AddBuilder(LAB.MODEL.SingleBuilding singleBuilding);
        public bool EditBuilder(LAB.MODEL.SingleBuilding singleBuilding);
        public bool ChangeState(int BID);
        public IEnumerable<object> GetCheckList();
    }
}
