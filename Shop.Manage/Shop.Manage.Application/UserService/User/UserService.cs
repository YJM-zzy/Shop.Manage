namespace Shop.Manage.Application.UserService.User
{
    public class UserService : IUserService,ITransient
    {
        protected readonly IRepository<UserMessage> _repository;
        protected readonly ILogger<UserService> _logger;
        public UserService(IRepository<UserMessage> repository, ILogger<UserService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public bool AddUser(UserMessage user)
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

        public UserMessage GetUserById(int id)
        {
            return _repository.FirstOrDefault(f => f.Id == id && !f.IsDeleted);
        }

        public UserMessage GetUserByMobile(string mobile)
        {
            return _repository.FirstOrDefault(f => f.Phone == mobile && !f.IsDeleted);
        }

        public UserMessage GetUserByName(string name)
        {
            return _repository.FirstOrDefault(f => f.Name == name && !f.IsDeleted);
        }

        public bool UpdateUser(UserMessage user)
        {
            var olduser = GetUserById(user.Id);
            if(olduser != null)
            {
                olduser = user;
                olduser.UpdatedTime = DateTime.Now;
                _repository.Update(olduser);
                return true;
            }
            return false;
        }

        public UserMessage GetUserByQuery(string query)
        {
            return _repository.FirstOrDefault(f => f.Name == query || f.Phone == query);
        }

        public bool IsUsernameExit(string name)
        {
            var user = GetUserByName(name);
            if (user != null)
                return true;
            return false;
        }

        public bool IsPhoneExit(string phone)
        {
            var user = GetUserByMobile(phone);
            if (user != null)
                return true;
            return false;
        }
    }
}
