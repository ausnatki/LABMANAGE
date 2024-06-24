using LAB.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LAB.REPOSITORY
{
    public class db_academy
    {

        private readonly LAB.DB.LabContext Ctx;

        public db_academy(LabContext ctx)
        {
            Ctx = ctx;
        }

        #region 获取学院列表
        public IEnumerable<object> GetList()
        {
            try
            {
                var list = Ctx.Academys.Select(c => new
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsDel = c.IsDel
                }).ToList();
                return list;
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

        #region 多选框的学院列表
        public IEnumerable<object> GetSelectCheckList()
        {
            try
            {
                var list = Ctx.Academys.Where(c=>c.IsDel == false).Select(c => new
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsDel = c.IsDel
                }).ToList();
                return list;
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

        #region 学院添加
        public bool AddAcademy(LAB.MODEL.Academy academy)
        {
            using (var transaction = Ctx.Database.BeginTransaction())
            {
                try
                {
                    academy.IsDel = !academy.IsDel;
                    Ctx.Academys.Add(academy);
                    Ctx.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        #endregion

        #region 改变状态
        public bool ChangeState(int CID)
        {
            using (var transaction = Ctx.Database.BeginTransaction())
            {
                try
                {
                    var academy = Ctx.Academys.Where(c => c.Id == CID).FirstOrDefault();
                    if (academy == null) throw new Exception();
                    academy.IsDel = !academy.IsDel;
                    Ctx.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
        #endregion

        #region 修改信息
        public bool EditAcademy(LAB.MODEL.Academy academy)
        {
            using (var transaction = Ctx.Database.BeginTransaction())
            {
                try
                {
                   // 判断信息真实性
                   var tacademy = Ctx.Academys.Where(c=>c.Id == academy.Id).FirstOrDefault();
                    if(tacademy == null) throw new Exception();
                    

                    tacademy.Name = academy.Name;
                    tacademy.IsDel = !academy.IsDel;

                    Ctx.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
        #endregion
    }
}
