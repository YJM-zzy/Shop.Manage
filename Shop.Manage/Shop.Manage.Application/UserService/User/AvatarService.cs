using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Application.UserService.User
{
    public class AvatarService : ITransient, IAvatarService
    {
        protected readonly IRepository<Avart> _repositiry;
        public AvatarService(IRepository<Avart> repository)
        {

            _repositiry = repository;

        }
        public Avart Add(Avart avart)
        {
            avart.UpdatedTime = DateTime.Now;
            avart.CreatedTime = DateTime.Now;
            var newAvatar = _repositiry.InsertNow(avart);
            return newAvatar.Entity;
        }

        public bool DelAvatarById(int id)
        {
            var avatar = GetAvartById(id);
            if(avatar==null)
                return false;
            _repositiry.Delete(avatar);
            return true;
        }

        public Avart GetAvartById(int id)
        {
            return _repositiry.FirstOrDefault(x => x.Id == id);
        }
    }
}
