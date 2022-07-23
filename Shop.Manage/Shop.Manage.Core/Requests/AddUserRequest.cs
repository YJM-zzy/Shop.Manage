using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Core.Requests
{
    public class AddUserRequest
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}
