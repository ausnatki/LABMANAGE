﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.AUTH.Server
{
    public interface ILoginService
    {
        public object  GetJwt(string username,string password);
        public object GetInfoByName(string token);
    }
}
