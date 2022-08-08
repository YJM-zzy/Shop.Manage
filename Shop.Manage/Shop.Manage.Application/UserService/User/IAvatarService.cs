using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Application.UserService.User
{
    public interface IAvatarService
    {
        Avart Add(Avart avart);

        Avart GetAvartById(int id);
        bool DelAvatarById(int id);
    }
}
