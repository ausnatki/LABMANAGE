using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.AUTH.Models
{
    [Table("TB_DailySafetyCheck")]
    public class DailySafetyCheck // 实验室管理人员日常检查表
    {
        public int Id { get; set; }

        public int LabID { get; set; } // 实验室ID
        public int SemesterID { get; set; } // 学期id
        public int UID {  get; set; } // 检查人员
        public DateTime CheckDate { get; set; } // 检查日期
        public bool Cleanliness { get; set; } // 实验室是否清洁
        public bool DoorsAndWindows { get; set; } // 实验室门窗是否安全
        public bool ElectricalLines { get; set; } // 线路是否安全

        public bool FireSafetyEquipmentAvailable { get; set; } // 消防设备有无
        public bool FireSafetyEquipmentExpiry { get; set; } // 消防设施是否在使用期
        public bool InstrumentEquipmentIntact { get; set; } // 仪器设备是否正常使用
        public bool InstrumentEquipmentWorking { get; set; } // 仪器设备是否完好
        public bool InstrumentWarningLabelsIntact { get; set; } // 仪器设备警示标志是否完好
        public string? OtherHazards { get; set; } //其他安全隐患
        public string? OtherSafetyHazards { get; set; } //仪器设配其他安全隐患
        public bool IsDel { get; set; }


        public LAB.AUTH.Models.SysUser? SysUser {  get; set; }
        public LAB.AUTH.Models.Laboratories? Laboratories { get; set; }
        public LAB.AUTH.Models.Semesters? Semesters { get; set; }
        public List<LAB.AUTH.Models.LabInclidentHanding>? labInclidentHanding {  get; set; }
        public List<LAB.AUTH.Models.LabEquipmentRepairs>? labEquipmentRepairs { get; set; }
    }
}
