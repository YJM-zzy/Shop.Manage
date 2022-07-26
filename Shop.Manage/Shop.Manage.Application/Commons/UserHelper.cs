using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Application.Commons
{
    public static class UserHelper
    {
        public static UserMessage GetUser()
        {
            var userId = App.User?.FindFirstValue("Id");
            if(userId == null) { return null; }
            var user = App.GetService<IUserService>().GetUserById(Convert.ToInt32(userId));
            return user;
        }
    }
}
