namespace Shop.Manage.Application.Reception
{
    [Route("api/app/[controller]")]
    [ApiDescriptionSettings("App")]
    public class AuthAppService:IDynamicApiController
    {
        protected readonly IUserService _userService;
        protected readonly ILogger<AuthAppService> _logger;
        public AuthAppService(IUserService userService, ILogger<AuthAppService> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        public Response<bool> Register(AddUserRequest addUserRequest)
        {
            _logger.LogError($"(/api/app/auth/register request) - {JsonConvert.SerializeObject(addUserRequest)}");
            Response<bool> resp = new Response<bool>();
            try
            {
                var user = _userService.GetUserByName(addUserRequest.Name);
                if (user != null)
                {
                    resp.Success = false;
                    resp.Message = $"用户名[{addUserRequest.Name}]已存在";
                    resp.Status = "01";
                    resp.Result = false;
                    _logger.LogError($"(/api/app/auth/register response) - {JsonConvert.SerializeObject(resp)}");
                    return resp;
                }
                user = _userService.GetUserByMobile(addUserRequest.Phone);
                if (user != null)
                {
                    resp.Success = false;
                    resp.Message = $"手机号[{addUserRequest.Phone}]已存在";
                    resp.Status = "01";
                    resp.Result = false;
                    _logger.LogError($"(/api/app/auth/register response) - {JsonConvert.SerializeObject(resp)}");
                    return resp;
                }
                user = addUserRequest.Adapt<UserMessage>();
                var res = _userService.AddUser(user);
                if (res)
                {
                    resp.Success = true;
                    resp.Status = "00";
                    resp.Result = true;
                    _logger.LogError($"(/api/app/auth/register response) - {JsonConvert.SerializeObject(resp)}");
                    return resp;
                }
                resp.Success = false;
                resp.Status = "01";
                resp.Message = "插入数据库出错";
                resp.Result = false;
                _logger.LogError($"(/api/app/auth/register response) - {JsonConvert.SerializeObject(resp)}");
                return resp;
            }
            catch (Exception ex)
            {
                resp.Success = false;
                resp.Message = ex.Message;
                resp.Status = "99";
                _logger.LogError($"(/api/app/auth/register response) - {JsonConvert.SerializeObject(resp)}");
                return resp;
            }
        }

        [HttpPost]
        public Response<TokenResponse> Login(LoginRequest loginRequest)
        {
            _logger.LogError($"(/api/app/auth/Login request) - {JsonConvert.SerializeObject(loginRequest)}");
            Response<TokenResponse> resp = new Response<TokenResponse>();
            var user = _userService.GetUserByQuery(loginRequest.Query);
            if(user == null)
            {
                resp.Success = false;
                resp.Message = "当前用户未注册，请前往注册";
                resp.Status = "01";
                _logger.LogError($"(/api/app/auth/Login response) - {JsonConvert.SerializeObject(resp)}");
                return resp;
            }
            if(user.Password != MD5Encryption.Encrypt(loginRequest.Password))
            {
                resp.Success =false;
                resp.Message = "用户名或密码有误";
                resp.Status = "01";
                _logger.LogError($"(/api/app/auth/Login response) - {JsonConvert.SerializeObject(resp)}");
                return resp;
            }
            var token = JWTEncryption.Encrypt(new Dictionary<string, object>()
            {
                { "id", user.Id },
                {"type", "app" }
            }, 60);
            var refreshToken = JWTEncryption.GenerateRefreshToken(token);
            resp.Success = true;
            resp.Status = "00";
            resp.Result = new TokenResponse()
            {
                Token = token,
                RefreshToken = refreshToken
            };
            _logger.LogError($"(/api/app/auth/Login response) - {JsonConvert.SerializeObject(resp)}");
            return resp;
        }
    }
}
