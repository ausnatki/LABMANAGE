using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.MODEL.DoTempClass
{
    public class AddUser
    {
        public int Id { get; set; }
        public string LoginName {  get; set; }
        public string Email {  get; set; }
        public string Password { get; set; }    
        public string UserName { get; set; }
        public string Phone {  get; set; }
        public bool Sex {  get; set; }
        public int CID {  get; set; }// 学院id
        public int RID {  get; set; }// 角色id
    }
}
