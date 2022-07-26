using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Core.Requests
{
    public class AddBrandRequest
    {
        public string BrandNo { get; set; }
        public string BrandName { get; set; }
        public string Img { get; set; }
    }
}
