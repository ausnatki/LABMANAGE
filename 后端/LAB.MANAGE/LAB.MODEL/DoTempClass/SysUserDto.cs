﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.MODEL.DoTempClass
{
    public class SysUserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool Role { get; set; }
    }

}
