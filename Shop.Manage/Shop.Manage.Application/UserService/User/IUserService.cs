namespace Shop.Manage.Application.UserService.User
{
    public interface IUserService
    {
        bool AddUser(UserMessage user);
        bool UpdateUser(UserMessage user);
        bool DeleteUser(int id);
        UserMessage GetUserByName(string name);
        UserMessage GetUserById(int id);
        UserMessage GetUserByMobile(string mobile);
        UserMessage GetUserByQuery(string query);
        bool IsUsernameExit(string name);
        bool IsPhoneExit(string phone);
    }
}
