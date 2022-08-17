using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Core.Requests
{
    public class AddUserAddrRequest
    {
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Detail { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public string Tag { get; set; }
    }
}
