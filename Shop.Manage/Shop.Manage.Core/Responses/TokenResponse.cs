﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Core.Responses
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
