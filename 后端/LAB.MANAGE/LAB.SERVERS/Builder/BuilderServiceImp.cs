using LAB.MODEL;
using LAB.REPOSITORY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.SERVERS
{
    public class BuilderServiceImp : IBuilderService
    {
        private readonly LAB.REPOSITORY.db_builder db_Builder;

        public BuilderServiceImp(db_builder db_Builder)
        {
            this.db_Builder = db_Builder;
        }

        #region 添加房楼
        public bool AddBuilder(SingleBuilding singleBuilding)
        {
            try 
            {
               return db_Builder.AddBuilder(singleBuilding);

            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 改变房楼状态
        public bool ChangeState(int BID)
        {
            try 
            {
                return db_Builder.ChangeState(BID);
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 修改房楼状态
        public bool EditBuilder(SingleBuilding singleBuilding)
        {
            try 
            {
                return db_Builder.EditBuilder(singleBuilding);
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 获取全部列表
        public IEnumerable<object> GetAllList()
        {
            try 
            {
                return db_Builder.GetAllList();
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

    }
}
