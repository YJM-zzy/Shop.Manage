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
        protected readonly IAvatarService _avatarService;
        public UserAppService(IUserService userService, ILogger<UserAppService> logger, IAddressService addressService, IAvatarService avatarService)
        {
            _userService = userService;
            _logger = logger;
            _addressService = addressService;
            _avatarService = avatarService;
        }
        [HttpPost]
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

        [HttpPost]
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

        [HttpPost]
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

        [HttpGet]
        public Response<UserMessageDto> GetUser()
        {
            var user = UserHelper.GetUser();
            return new Response<UserMessageDto>
            {
                Success = true,
                Status = "00",
                Result = user.Adapt<UserMessageDto>()
            };
        }

        [HttpPost]
        public  Response<int> UploadAvatar(IFormFile file)
        {
            var fileLength = file.Length;
            using var stream = file.OpenReadStream();
            var bytes = new byte[fileLength];
            stream.Read(bytes, 0, (int)fileLength);
            // 这里将 bytes 存储到你想要的介质中即可
            Avart avart = new Avart();
            avart.ImageMessage = bytes;
            var newEntity = _avatarService.Add(avart);
            return new Response<int>()
            {
                Status = "00",
                Success=true,
                Result = newEntity.Id
            };
        }

        [NonUnify, HttpGet, AllowAnonymous]
        public IActionResult GetAvatar(int resourceId)
        {
            var avatar = _avatarService.GetAvartById(resourceId);
            return new FileContentResult(avatar.ImageMessage, "image/jpeg");
        }

        [HttpPost]
        public Response<bool> DelAvatar(int id)
        {
            var resp = new Response<bool>();
            try
            {
                var res = _avatarService.DelAvatarById(id);
                if (res)
                {
                    resp.Success = true;
                    resp.Status = "00";
                    resp.Result = true;
                    return resp;
                }
                resp.Success = false;
                resp.Status = "01";
                resp.Message = "头像不存在";
                resp.Result = false;
                return resp;

            }
            catch(Exception ex)
            {
                resp.Success = false;
                resp.Status = "99";
                resp.Message = ex.Message;
                resp.Result = false;
                return resp;
            }
        }

        [HttpPost]
        public Response<bool> UpdateAvatar(UpdateAvatarRequest request)
        {
            var resp = new Response<bool>();
            try
            {
                var user = UserHelper.GetUser();
                user.AvatarUrl = request.Url;
                _userService.UpdateUser(user);
                resp.Status = "00";
                resp.Success = true;
                resp.Result = true;
                return resp;
            }
            catch(Exception ex)
            {
                resp.Success = false;
                resp.Status = "99";
                resp.Message = ex.Message;
                resp.Result = false;
                return resp;
            }
        }
    }
}
