using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.MODEL
{
    [Table("Academy")]
    public class Academy // 学院表
    {
         public int Id { get; set; }
        public string Name {  get; set; }
        public bool IsDel {  get; set; }
        public List<LAB.MODEL.SysUser>? sysUsers { get; set; }
        //public List<LAB.MODEL.Laboratories>?  laboratories { get; set; }
    }
}
