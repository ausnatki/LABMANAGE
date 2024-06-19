using LAB.MODEL;
using LAB.REPOSITORY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LAB.SERVERS
{
    public class FloorServiceImp : IFloorService 
    {

        private readonly LAB.REPOSITORY.db_floor db_Floor;

        public FloorServiceImp(db_floor db_Floor)
        {
            this.db_Floor = db_Floor;
        }

        public bool AddFloor(int BID)
        {
            try 
            {
                return db_Floor.AddFloor(BID);
            } 
            catch 
            {
                return false;
            }
        }

        public bool ChangeState(int FID)
        {
            try 
            {
                return db_Floor.ChangeState(FID);
            }
            catch
            {
                return false;
            }
        }

        public bool EditFloor(Floor floor)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetAllList()
        {
            try 
            {
                return db_Floor.GetAllList();
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
