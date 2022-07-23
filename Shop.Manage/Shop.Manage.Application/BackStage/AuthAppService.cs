using Shop.Manage.Application.UserService.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Application.BackStage
{
    [Route("api/manage/[controller]")]
    [ApiDescriptionSettings("manage")]
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
