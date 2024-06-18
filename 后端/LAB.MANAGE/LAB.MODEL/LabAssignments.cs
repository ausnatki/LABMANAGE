using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.MODEL
{
    [Table("LabAssignments")]
    public class LabAssignments // 实验室分配表
    {
        public int Id {  get; set; }
        public int LabID { get;set; } // 实验室id
        public int UserID {  get; set; } // 用户编码


        public LAB.MODEL.SysUser? SysUser { get; set; }
        public LAB.MODEL.Laboratories? Laboratories {  get; set; }
    }
}
