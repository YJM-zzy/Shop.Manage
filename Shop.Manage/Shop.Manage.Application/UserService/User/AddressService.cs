﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Application.UserService.User
{
    public class AddressService : IAddressService, ITransient
    {
        protected readonly IRepository<UserAddr> _respository;
        public AddressService(IRepository<UserAddr> respository)
        {
            _respository = respository;
        }
        public bool Add(UserAddr userAddr)
        {
            if (userAddr == null)
                return false;
            userAddr.UpdatedTime = DateTime.Now;
            userAddr.CreatedTime = DateTime.Now;
            userAddr.IsDeleted = false;
            _respository.Insert(userAddr);
            return true;
        }

        public bool Delete(int id)
        {
            var useraddr = Get(id);
            if (useraddr != null)
            {
                useraddr.IsDeleted = true;
                useraddr.UpdatedTime = DateTime.Now;
                _respository.Update(useraddr);
                return true;
            }
            return false;
        }

        public UserAddr Get(int id)
        {
            return _respository.FirstOrDefault(f => f.Id == id && !f.IsDeleted);
        }

        public IQueryable<UserAddr> GetAllByUserId(int userId)
        {
            return _respository.Where(w => w.UserId == userId && !w.IsDeleted).OrderByDescending(x=>x.IsDefault);
        }

        public bool Update(UserAddr userAddr)
        {
            userAddr.UpdatedTime = DateTime.Now;
            _respository.Update(userAddr);
            return true;
        }

        public UserAddr GetDefaultAddr(int userid)
        {
            return _respository.FirstOrDefault(x => x.UserId == userid && x.IsDefault);
        }
    }
}
