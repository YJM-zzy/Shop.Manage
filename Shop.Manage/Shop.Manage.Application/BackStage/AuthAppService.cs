using Shop.Manage.Application.UserService.Business;

namespace Shop.Manage.Application.BackStage
{
    [Route("api/manage/[controller]")]
    [ApiDescriptionSettings("Manage")]
    public class AuthAppService:IDynamicApiController
    {
        protected readonly IBusinessService _businessService;
        protected readonly ILogger<AuthAppService> _logger;

        public AuthAppService(IBusinessService businessService, ILogger<AuthAppService> logger)
        {
            _businessService = businessService;
            _logger = logger;
        }

        [HttpPost]
        public Response<bool> Register(AddBusiness request)
        {
            _logger.LogError($"(/api/manage/Auth/Register request) - {JsonConvert.SerializeObject(request)}");
            var resp = new Response<bool>();
            try
            {
                var isexit = _businessService.IsPhoneExit(request.Name);
                if (isexit)
                {
                    resp.Success = false;
                    resp.Message = $"用户[{request.Name}已存在]";
                    resp.Result = false;
                    resp.Status = "01";
                    _logger.LogError($"(/api/manage/auth/register response) response - {JsonConvert.SerializeObject(resp)}");
                    return resp;
                }
                isexit = _businessService.IsPhoneExit(request.Phone);
                if (isexit)
                {
                    resp.Success = false;
                    resp.Message = $"用户[{request.Phone}已存在]";
                    resp.Result = false;
                    resp.Status = "01";
                    _logger.LogError($"(/api/manage/auth/register response) response - {JsonConvert.SerializeObject(resp)}");
                    return resp;
                }

                var user = request.Adapt<BusinessMessage>();
                _businessService.AddUser(user);
                resp.Success = true;
                resp.Result = true;
                resp.Status = "00";
                _logger.LogError($"(/api/manage/auth/register response) response - {JsonConvert.SerializeObject(resp)}");
                return resp;
            }
            catch (Exception e)
            {
                resp.Success = false;
                resp.Message = e.Message;
                resp.Result = false;
                resp.Status = "99";
                _logger.LogError($"(/api/manage/auth/register response) response - {JsonConvert.SerializeObject(resp)}");
                return resp;
            }
        }

        [HttpPost]
        public Response<TokenResponse> Login(LoginRequest loginRequest)
        {
            _logger.LogError($"(/api/manage/auth/Login request) - {JsonConvert.SerializeObject(loginRequest)}");
            Response<TokenResponse> resp = new Response<TokenResponse>();
            var user = _businessService.GetUserByQuery(loginRequest.Query);
            if (user == null)
            {
                resp.Success = false;
                resp.Message = "当前用户未注册，请前往注册";
                resp.Status = "01";
                _logger.LogError($"(/api/manage/auth/Login response) - {JsonConvert.SerializeObject(resp)}");
                return resp;
            }
            if (user.Password != MD5Encryption.Encrypt(loginRequest.Password))
            {
                resp.Success = false;
                resp.Message = "用户名或密码有误";
                resp.Status = "01";
                _logger.LogError($"(/api/manage/auth/Login response) - {JsonConvert.SerializeObject(resp)}");
                return resp;
            }
            var token = JWTEncryption.Encrypt(new Dictionary<string, object>()
            {
                { "id", user.Id },
                {"type", "manage" }
            }, 60);
            var refreshToken = JWTEncryption.GenerateRefreshToken(token);
            resp.Success = true;
            resp.Status = "00";
            resp.Result = new TokenResponse()
            {
                Token = token,
                RefreshToken = refreshToken
            };
            _logger.LogError($"(/api/manage/auth/Login response) - {JsonConvert.SerializeObject(resp)}");
            return resp;
        }
    }
}
