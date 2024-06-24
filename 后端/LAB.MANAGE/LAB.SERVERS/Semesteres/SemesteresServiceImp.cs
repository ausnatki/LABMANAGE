using LAB.MODEL;
using LAB.REPOSITORY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.SERVERS
{
    public class SemesteresServiceImp : ISemesteresService
    {
        private readonly LAB.REPOSITORY.db_semesteres db_Semesteres;

        public SemesteresServiceImp(db_semesteres db_Semesteres)
        {
            this.db_Semesteres = db_Semesteres;
        }

        #region 添加学期
        public bool AddSemesteres(Semesters semesters)
        {
            try
            {
                return db_Semesteres.Add(semesters);

            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 获取所有学期列表
        public IEnumerable<object> GetAllList()
        {
            try
            {
                return db_Semesteres.GetAllList();
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

        #region 修改学期
        public bool EditSemesteres(LAB.MODEL.Semesters semesters)
        {
            try 
            {
                return db_Semesteres.EditSemesteres(semesters);
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 获取所有多选框列表
        public IEnumerable<object> GetCheckList()
        {
            try
            {
                return db_Semesteres.GetCheckList();
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion


    }
}
