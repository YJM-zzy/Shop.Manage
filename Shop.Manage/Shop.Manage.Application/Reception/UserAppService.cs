using Shop.Manage.Application.Reception.Dtos;

namespace Shop.Manage.Application.Reception
{
    [Route("api/app/[controller]")]
    [Authorize]
    [ApiDescriptionSettings("App")]
    public class UserAppService : IDynamicApiController
    {
        protected readonly IUserService _userService;
        protected readonly IAddressService _addressService;
        protected readonly ILogger<UserAppService> _logger;
        public UserAppService(IUserService userService, ILogger<UserAppService> logger, IAddressService addressService)
        {
            _userService = userService;
            _logger = logger;
            _addressService = addressService;
        }
        [HttpPut]
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

        [HttpPost]
        public Response<bool> AddUserAddr(AddUserAddrRequest request)
        {
            _logger.LogError($"(/api/app/user/adduseraddr request) - {JsonConvert.SerializeObject(request)}");
            var resp = new Response<bool>();
            try
            {
                var user = UserHelper.GetUser();
                var useraddr = request.Adapt<UserAddr>();
                useraddr.UserId = user.Id;
                _addressService.Add(useraddr);
                resp.Success = true;
                resp.Message = "成功！";
                resp.Status = "00";
                resp.Result = true;
                _logger.LogError($"(/api/app/user/adduseraddr response) - {JsonConvert.SerializeObject(resp)}");
                return resp;
                
            }
            catch(Exception ex)
            {
                resp.Success = false;
                resp.Message = ex.Message;
                resp.Status = "99";
                resp.Result = false;
                _logger.LogError($"(/api/app/user/adduseraddr response) - {JsonConvert.SerializeObject(resp)}");
                return resp;
            }
        }

        [HttpPut]
        public Response<bool> UpdateUserAddr(UpdateUserAddrRequest request)
        {
            _logger.LogError($"(/api/app/user/updateuseraddr request) - {JsonConvert.SerializeObject(request)}");
            var resp = new Response<bool>();
            try
            {
                var user = UserHelper.GetUser();
                var useraddr = request.Adapt<UserAddr>();
                useraddr.UserId = user.Id;
                var res = _addressService.Update(useraddr);
                if(res)
                {
                    resp.Success = true;
                    resp.Message = "成功";
                    resp.Status = "00";
                    resp.Result = true;
                    _logger.LogError($"(/api/app/user/updateuseraddr response) - {JsonConvert.SerializeObject(resp)}");
                    return resp;
                }
                resp.Success = false;
                resp.Message = "改地址不存在或已经被删除";
                resp.Status = "01";
                resp.Result = false;
                _logger.LogError($"(/api/app/user/updateuseraddr response) - {JsonConvert.SerializeObject(resp)}");
                return resp;

            }
            catch(Exception ex)
            {
                resp.Success = false;
                resp.Message = ex.Message;
                resp.Status = "99";
                resp.Result = false;
                _logger.LogError($"(/api/app/user/updateuseraddr response) - {JsonConvert.SerializeObject(resp)}");
                return resp;
            }
        }

        [HttpDelete]
        public Response<bool> DeleteUserAddr(int id)
        {
            _logger.LogError($"(/api/app/user/deleteuseraddr request) - 地址ID：{id}");
            var resp = new Response<bool>();
            try
            {
                var res = _addressService.Delete(id);
                if(res)
                {
                    resp.Success = true;
                    resp.Message = "成功";
                    resp.Status = "00";
                    resp.Result = true;
                    _logger.LogError($"(/api/app/user/deleteuseraddr response) - {JsonConvert.SerializeObject(resp)}");
                    return resp;
                }
                resp.Success = false;
                resp.Message = "地址不存在或已经被删除";
                resp.Status = "01";
                resp.Result = false;
                _logger.LogError($"(/api/app/user/deleteuseraddr response) - {JsonConvert.SerializeObject(resp)}");
                return resp;
            }
            catch(Exception ex)
            {
                resp.Success = false;
                resp.Message = ex.Message;
                resp.Status = "99";
                resp.Result = false;
                _logger.LogError($"(/api/app/user/deleteuseraddr response) - {JsonConvert.SerializeObject(resp)}");
                return resp;
            }

        }

        [HttpGet]
        public Response<List<UserAddrDto>> GetUserAddr()
        {
            var resp = new Response<List<UserAddrDto>>();
            var user = UserHelper.GetUser();
            var list = _addressService.GetAllByUserId(user.Id).ToList();
            resp.Success = true;
            resp.Status = "00";
            resp.Result = list.Adapt<List<UserAddrDto>>();
            _logger.LogError($"(/api/app/user/GetUserAddr response) - {JsonConvert.SerializeObject(resp)}");
            return resp;
        }
    }
}
