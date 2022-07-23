using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Application.UserService.Business
{
    public interface IBusinessService
    {
        bool AddUser(BusinessMessage user);
        bool UpdateUser(BusinessMessage user);
        bool DeleteUser(int id);
        BusinessMessage GetUserByName(string name);
        BusinessMessage GetUserById(int id);
        BusinessMessage GetUserByMobile(string mobile);
        BusinessMessage GetUserByQuery(string query);
        bool IsUsernameExit(string name);
        bool IsPhoneExit(string phone);
    }
}
