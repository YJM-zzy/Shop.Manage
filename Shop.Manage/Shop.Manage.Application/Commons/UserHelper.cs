using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Application.Commons
{
    public static class UserHelper
    {
        static readonly IUserService _userService ;
        static UserHelper()
        {
            _userService = App.GetService<IUserService>();
        }

        public static UserMessage GetUser()
        {
            var userId = App.User?.FindFirstValue("Id");
            if(userId == null) { return null; }
            var user = _userService.GetUserById(Convert.ToInt32(userId));
            return user;
        }
    }
}
