using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.AUTH.Models
{
    [Table("Laboratories")]
    public class Laboratories // 实验室信息表
    {
        public int Id {  get; set; }
        public bool IsDel {  get; set; } // 是否删除
        public int AcademyId {  get; set; } // 学院id
        public int LabNumber {  get; set; } // 实验室号码

        public LAB.AUTH.Models.Academy? Academy { get; set; }
        public List<LAB.AUTH.Models.LabAssignments>? LabAssignments { get; set; }
        public List<LAB.AUTH.Models.DailySafetyCheck>? dailySafetyChecks { get; set; }
    }
}
