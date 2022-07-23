using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Core.Requests
{
    public class LoginRequest
    {
        public string Query { get; set; }
        public string Password { get; set; }
    }
}
