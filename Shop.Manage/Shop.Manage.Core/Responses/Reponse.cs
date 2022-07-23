using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Core.Responses
{
    public class Response<T>
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public T Result { get; set; }
    }
}
