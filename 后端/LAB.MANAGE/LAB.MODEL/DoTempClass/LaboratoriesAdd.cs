using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.MODEL.DoTempClass
{
    public class LaboratoriesAdd
    {
        public string LabNumber {  get; set; } // 实验室号码（这里应该是楼房名称）
        public int AcademyId {  get; set; }// 所属学院id
        public int FloorId {  get; set; }// 楼层id
        //public int UID {  get; set; } // 实验室管理人员
    }
}
