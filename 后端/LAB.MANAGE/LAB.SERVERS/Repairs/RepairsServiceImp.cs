using LAB.MODEL;
using LAB.REPOSITORY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.SERVERS
{
    public class RepairsServiceImp :IRepairsService
    {
        private readonly LAB.REPOSITORY.db_repairs db_Repairs;

        public RepairsServiceImp(db_repairs db_Repairs)
        {
            this.db_Repairs = db_Repairs;
        }

        public IEnumerable<object> GetAllRepairs()
        {
            try 
            {
                return db_Repairs.GetAllRepairs();
            }
            catch 
            {
                throw new Exception();
            }
        }

        public IEnumerable<object> GetRepairsByUser(int UID)
        {
            try 
            {
                return db_Repairs.GetRepairsByUser(UID);
            }
            catch 
            {
                throw new Exception();
            }
        }

        public bool UpdataRepairs(LabEquipmentRepairs repairs)
        {
            try 
            {
                return db_Repairs.UpdataRepairs(repairs);
            }
            catch
            {
                return false;
            }
        }
    }
}
