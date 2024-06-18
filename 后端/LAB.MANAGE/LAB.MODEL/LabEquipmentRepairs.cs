using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.MODEL
{
    [Table("LabEquipmentRepairs")]
    public class LabEquipmentRepairs // 维修表
    {
        public int Id {  get; set; }
        public int DLabID {  get; set; }// 日常检查id
        public DateTime RepairDate {  get; set; }// 维修时间
        public string IssuesFound {  get; set; }// 存在的问题
        public string? Suggestions {  get; set; }// 整改意见
        public int RepairPersonnelID {  get; set; }// 维修人员
        public bool ComletionStatus {  get; set; }//完成情况
        public string? Remarks {  get; set; }// 备注
        public bool IsDel {  get; set; }//是否删除

        public LAB.MODEL.DailySafetyCheck? dailySafetyChecks { get; set; } // 实验室日常信息表
    }
}
