using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LAB.MODEL
{
    [Table("Semesters")]
    public class Semesters
    {
        public int Id {  get; set; }
        public string Name { get; set; }// 学期列表
        public bool IsDel {  get; set; }

        public List<LAB.MODEL.DailySafetyCheck>? dailySafetyChecks { get; set; }
    }
}
