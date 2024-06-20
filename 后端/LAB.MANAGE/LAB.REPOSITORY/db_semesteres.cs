using LAB.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LAB.REPOSITORY
{
    public class db_semesteres
    {
        private readonly LAB.DB.LabContext Ctx;

        public db_semesteres(LabContext context)
        {
            this.Ctx = context;
        }


        #region 获取所有学期列表
        public IEnumerable<object> GetAllList()
        {
            try
            {
                var list = Ctx.Semesters.Select(c => new
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

        #region 添加学期
        public bool Add(LAB.MODEL.Semesters semesters)
        {
            using(var transacton =  Ctx.Database.BeginTransaction()) 
            {
                try 
                {
                    semesters.IsDel = !semesters.IsDel;
                    Ctx.Semesters.Add(semesters);
                    Ctx.SaveChanges();
                    transacton.Commit();
                    return true;
                }
                catch 
                {
                    return false;
                }

            }
        }
        #endregion

        #region 修改学期
        public bool EditSemesteres(LAB.MODEL.Semesters semesters)
        {
            using(var transaction = Ctx.Database.BeginTransaction())
            {
                try 
                {
                    // 验证数据的真实性
                    var tsemesters = Ctx.Semesters.Where(c => c.Id == semesters.Id).FirstOrDefault();
                    if (tsemesters == null) throw new Exception();

                    tsemesters.Name = semesters.Name;
                    tsemesters.IsDel = !semesters.IsDel;
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
