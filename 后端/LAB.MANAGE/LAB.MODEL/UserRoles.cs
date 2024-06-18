using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.MODEL
{
    [Table("UserRoles")]
    public class UserRoles
    {
        public int Id { get; set; }
        public int RID { get; set; }
        public int UID { get; set; }
        public SysUser? sysUser { get; set; }
        public Roles? Role { get; set; }
    }
}
