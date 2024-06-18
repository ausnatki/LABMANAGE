﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.MODEL
{
    [Table("Roles")]
    public class Roles //角色表
    {
        public int Id {  get; set; }
        public bool IsDel {  get; set; }
        public string RoleName{ get; set; }

        public List<LAB.MODEL.UserRoles>? user_roles { get; set; }
    }
}
