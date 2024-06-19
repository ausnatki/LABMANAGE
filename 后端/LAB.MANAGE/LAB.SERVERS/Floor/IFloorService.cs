using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.SERVERS
{
    public interface IFloorService
    {
        public IEnumerable<object> GetAllList();
        public bool EditFloor(LAB.MODEL.Floor floor);// 暂定 不知道是否需要这个方法实现
        public bool ChangeState(int FID);
        public bool AddFloor(int BID);
    }
}
