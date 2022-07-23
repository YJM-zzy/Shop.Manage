using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Manage.Core.Enums;

namespace Shop.Manage.Core.Requests
{
    public class AddBusiness
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string CreditCode { get; set; }
        public BusinessRole Role { get; set; }
    }
}
