using LAB.MODEL;
using LAB.MODEL.DoTempClass;
using LAB.REPOSITORY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.SERVERS
{
    public class LaboratoriesServiceImp :ILaboratoriesService
    {
        private readonly LAB.REPOSITORY.db_laboratories db_Laboratories;

        public LaboratoriesServiceImp(db_laboratories db_Laboratories)
        {
            this.db_Laboratories = db_Laboratories;
        }

        public bool AddLab(LaboratoriesAdd laboratoriesAdd)
        {
            try 
            {
                return db_Laboratories.AddLab(laboratoriesAdd);
            }
            catch 
            {
                return false;
            }
        }

        public bool ChangeState(int LID)
        {
            try 
            {
                return db_Laboratories.ChangeState(LID);
            }
            catch
            {
                return false;
            }
        }

        public bool EditLab(Laboratories laboratories)
        {
            try 
            {
                return db_Laboratories.EditLab(laboratories);
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<object> GetAllList()
        {
            try 
            {
                return db_Laboratories.GetAllList();
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
