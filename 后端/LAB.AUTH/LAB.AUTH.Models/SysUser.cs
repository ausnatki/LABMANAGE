using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.AUTH.Models
{
    [Table("SysUser")]
    public class SysUser
    {
        public int Id {  get; set; }
        public string UserName {  get; set; }
        public string LoginName {  get; set; }
        public string Email {  get; set; }
        public string Password { get; set; }
        public string Phone {  get; set; }
        public bool Sex {  get; set; }
        public int CID { get; set; } // 学院表id
        public LAB.AUTH.Models.Academy? Academy { get; set; } 
        public List<LAB.AUTH.Models.DailySafetyCheck>? DailySafetyCheck {  get; set; } // 一对多 
        public List<LAB.AUTH.Models.UserRoles>? user_roles { get; set; }
        public List<LAB.AUTH.Models.LabAssignments>? LabAssignments { get; set; }
    }
}
