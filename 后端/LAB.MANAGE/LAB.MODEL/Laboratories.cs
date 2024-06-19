using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.MODEL
{
    [Table("Laboratories")]
    public class Laboratories // 实验室信息表
    {
        public int Id {  get; set; }
        public bool IsDel {  get; set; } // 是否删除
        public int AcademyId {  get; set; } // 学院id
        public int FloorId {  get; set; } // 楼层id
        public int LabNumber {  get; set; } // 实验室号码
        public int UID {  get; set; } // 管理人员 因为是多对1的关系所以不用创建中间表
        public LAB.MODEL.Academy? Academy { get; set; }
        public List<LAB.MODEL.LabAssignments>? LabAssignments { get; set; }
        public List<LAB.MODEL.DailySafetyCheck>? dailySafetyChecks { get; set; }
    }
}
