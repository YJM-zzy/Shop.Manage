using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Application.UserService.Business
{
    public class BusinessService:IBusinessService,ITransient
    {
        protected readonly IRepository<BusinessMessage> _repository;
        protected readonly ILogger<BusinessService> _logger;
        public BusinessService(IRepository<BusinessMessage> repository, ILogger<BusinessService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public bool AddUser(BusinessMessage user)
        {
            if (user == null)
                return false;
            user.UpdatedTime = DateTime.Now;
            user.CreatedTime = DateTime.Now;
            user.IsDeleted = false;
            _repository.Insert(user);
            return true;
        }

        public bool DeleteUser(int id)
        {
            var user = GetUserById(id);
            if (user != null)
            {
                user.UpdatedTime = DateTime.Now;
                user.IsDeleted = true;
                _repository.Update(user);
                return true;
            }
            return false;
        }

        public BusinessMessage GetUserById(int id)
        {
            return _repository.FirstOrDefault(f => f.Id == id && !f.IsDeleted);
        }

        public BusinessMessage GetUserByMobile(string mobile)
        {
            return _repository.FirstOrDefault(f => f.Phone == mobile && !f.IsDeleted);
        }

        public BusinessMessage GetUserByName(string name)
        {
            return _repository.FirstOrDefault(f => f.Name == name && !f.IsDeleted);
        }

        public BusinessMessage GetUserByQuery(string query)
        {
            return _repository.FirstOrDefault(f => f.Name == query || f.Phone == query);
        }

        public bool IsPhoneExit(string phone)
        {
            var user = GetUserByMobile(phone);
            if (user != null)
                return true;
            return false;
        }

        public bool IsUsernameExit(string name)
        {
            var user = GetUserByName(name);
            if (user != null)
                return true;
            return false;
        }

        public bool UpdateUser(BusinessMessage user)
        {
            var olduser = GetUserById(user.Id);
            if (olduser != null)
            {
                olduser = user;
                olduser.UpdatedTime = DateTime.Now;
                _repository.Update(olduser);
                return true;
            }
            return false;
        }
    }
}
