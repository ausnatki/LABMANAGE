using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.MODEL
{
    [Table("LabInclidentHanding")]
    public class LabInclidentHanding // 实验室异常处理表
    {
        public int Id {  get; set; }
        public int DLabID {  get; set; } // 日常处理表
        public string IncidentDetails { get; set; }// 异常错误信息
        public int RepairPersonnelID { get;set; } //维修人员id
        public int ReportedByID {  get; set; }// 上报人员
        public DateTime InclidentTime {  get; set; }// 异常发现时间
        public bool IsDel {  get; set; }// 是否删除

        public LAB.MODEL.DailySafetyCheck? dailySafetyChecks { get; set; } // 实验室日常信息表
    }
}
