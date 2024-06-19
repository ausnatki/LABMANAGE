using LAB.MODEL;
using LAB.REPOSITORY;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.SERVERS
{
    public class AcademyServiceImp:IAcademyService
    {

        private readonly LAB.REPOSITORY.db_academy db_Academy;

        public AcademyServiceImp(db_academy db_Academy)
        {
            this.db_Academy = db_Academy;
        }

        #region 学院添加
        public bool AddAcademy(Academy academy)
        {
            try 
            {
                return db_Academy.AddAcademy(academy);
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 获取全部列表
        public IEnumerable<object> GetList()
        {
            try
            {
                var list = db_Academy.GetList();
                return list;
            }
            catch
            {
                throw new Exception();
            }
          
        }
        #endregion

        #region 获取所有多选框列表
        public IEnumerable<object> GetSelectCheckList()
        {
            try 
            {
                return db_Academy.GetSelectCheckList();
            }
            catch 
            {
                throw new Exception();
            }
        }
        #endregion

        #region 改变状态
        public bool ChangeState(int CID)
        {
            try 
            {
                return db_Academy.ChangeState(CID);
            }
            catch 
            {
                return false;
            }
        }


        #endregion

        #region 修改学院信息
        public bool Edit(Academy academy)
        {
            try 
            {
                return db_Academy.EditAcademy(academy);
            }
            catch
            {
                return false;
            }
        }
        #endregion

    }
}
