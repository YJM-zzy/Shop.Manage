using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Application.UserService.User
{
    public interface IAddressService
    {
        bool Add(UserAddr userAddr);
        bool Update(UserAddr userAddr);
        bool Delete(int id);
        UserAddr Get(int id);
        IQueryable<UserAddr> GetAllByUserId(int userId);
    }
}
