using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Core.Responses
{
    public class AreaResponse
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public List<AreaResponse> Children { get; set; }
    }
}
