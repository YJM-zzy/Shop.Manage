namespace Shop.Manage.Application.Reception
{
    [Route("api/app/[controller]")]
    [Authorize]
    [ApiDescriptionSettings("App")]
    public class UserAppService : IDynamicApiController
    {
        protected readonly IUserService _userService;
        protected readonly ILogger<UserAppService> _logger;
        public UserAppService(IUserService userService, ILogger<UserAppService> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public Response<bool> UpdateUser(AddUserRequest userRequest)
        {
            _logger.LogError($"(/api/app/user/updateuser request) - {JsonConvert.SerializeObject(userRequest)}");
            var resp = new Response<bool>();
            try
            {
                var user = UserHelper.GetUser();
                if(user == null)
                {
                    resp.Success = false;
                    resp.Message = "用户未登录";
                    resp.Status = "01";
                    _logger.LogError($"(/api/app/user/updateuser response) - {JsonConvert.SerializeObject(resp)}");
                    return resp;
                }
                bool isexit = _userService.IsUsernameExit(userRequest.Phone);
                if(isexit && user.Name != userRequest.Name)
                {
                    resp.Success = false;
                    resp.Message = $"用户名[{userRequest.Name}]已存在";
                    resp.Status = "01";
                    _logger.LogError($"(/api/app/user/updateuser response) - {JsonConvert.SerializeObject(resp)}");
                    return resp;
                }
                isexit = _userService.IsPhoneExit(userRequest.Phone);
                if(isexit && user.Phone != userRequest.Phone)
                {
                    resp.Success = false;
                    resp.Message = $"手机号[{userRequest.Phone}]已存在";
                    resp.Status = "01";
                    _logger.LogError($"(/api/app/user/updateuser response) - {JsonConvert.SerializeObject(resp)}");
                    return resp;
                }
                user.Phone = userRequest.Phone;
                user.Password = MD5Encryption.Encrypt(userRequest.Password);
                user.Name = userRequest.Name;
                _userService.UpdateUser(user);
                resp.Success = true;
                resp.Result = true;
                resp.Status = "00";
                _logger.LogError($"(/api/app/user/updateuser response) - {JsonConvert.SerializeObject(resp)}");
                return resp;
            }
            catch(Exception ex)
            {
                resp.Success = false;
                resp.Status = "99";
                resp.Message = ex.Message;
                resp.Result = false;
                _logger.LogError($"(/api/app/user/updateuser response) - {JsonConvert.SerializeObject(resp)}");
                return resp;
            }
        }
    }
}
