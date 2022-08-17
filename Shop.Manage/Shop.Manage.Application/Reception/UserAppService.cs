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
        protected readonly IAreaService _areaService;
        public UserAppService(IUserService userService, ILogger<UserAppService> logger, IAddressService addressService, IAvatarService avatarService, IAreaService areaService)
        {
            _userService = userService;
            _logger = logger;
            _addressService = addressService;
            _avatarService = avatarService;
            _areaService = areaService;
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 上传头像
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 获取头像
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        [NonUnify, HttpGet, AllowAnonymous]
        public IActionResult GetAvatar(int resourceId)
        {
            var avatar = _avatarService.GetAvartById(resourceId);
            return new FileContentResult(avatar.ImageMessage, "image/jpeg");
        }
        /// <summary>
        /// 删除头像
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 获取收货地址
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Response<List<AddressInfoResponse>> GetUserAddress()
        {
            var user = UserHelper.GetUser();
            var addrs = _addressService.GetAllByUserId(user.Id);
            var addrsDto = addrs.Adapt<List<AddressInfoResponse>>();
            foreach(var addr in addrsDto)
            {
                if(addr.Province == addr.City)
                {
                    addr.AddrCombination = $"{_areaService.GetProvinceNameById(addr.Province)}{_areaService.GetAreaNameById(addr.District)}";
                }
                else
                {
                    addr.AddrCombination = $"{_areaService.GetProvinceNameById(addr.Province)}{_areaService.GetCityNameById(addr.City)}{_areaService.GetAreaNameById(addr.District)}";
                }
                addr.MobileHide = addr.Mobile.Remove(3, 4).Insert(3, "****");
            }
            return new Response<List<AddressInfoResponse>>()
            {
                Success = true,
                Status = "00",
                Result = addrsDto
            };
        }

        /// <summary>
        /// 删除收货地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<bool> DeleteUserAddr(int id)
        {
            _logger.LogError($"(/api/app/user/deleteuseraddr request) - 地址ID：{id}");
            var resp = new Response<bool>();
            try
            {
                var res = _addressService.Delete(id);
                if (res)
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
            catch (Exception ex)
            {
                resp.Success = false;
                resp.Message = ex.Message;
                resp.Status = "99";
                resp.Result = false;
                _logger.LogError($"(/api/app/user/deleteuseraddr response) - {JsonConvert.SerializeObject(resp)}");
                return resp;
            }

        }

        /// <summary>
        /// 修改收货地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<bool> UpdateUserAddr(UpdateUserAddrRequest request)
        {
            _logger.LogError($"(/api/app/user/updateuseraddr request) - {JsonConvert.SerializeObject(request)}");
            var resp = new Response<bool>();
            var res = false;
            try
            {
                var user = UserHelper.GetUser();
                var useraddr = request.Adapt<UserAddr>();
                useraddr.UserId = user.Id;
                //UserAddr deaddr = new UserAddr();
                if (request.IsDefault)
                {
                    var deaddr = _addressService.GetDefaultAddr(user.Id);
                    if (deaddr != null)
                    {
                        if (deaddr.Id != useraddr.Id)
                        {
                            deaddr.IsDefault = false;
                            _addressService.Update(deaddr);
                        }
                    }
                }
                res = _addressService.Update( useraddr );
                if (res)
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
            catch (Exception ex)
            {
                resp.Success = false;
                resp.Message = ex.Message;
                resp.Status = "99";
                resp.Result = false;
                _logger.LogError($"(/api/app/user/updateuseraddr response) - {JsonConvert.SerializeObject(resp)}");
                return resp;
            }
        }

        /// <summary>
        /// 添加收货地址信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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
                var addrs = _addressService.GetAllByUserId(user.Id);
                if(!addrs.ToList().Any())
                {
                    useraddr.IsDefault = true;
                }
                else
                {
                    if (request.IsDefault)
                    {
                        var deaddr = addrs.FirstOrDefault(x => x.IsDefault && !x.IsDeleted);
                        if(deaddr != null)
                        {
                            deaddr.IsDefault = false;
                            _addressService.Update( deaddr);
                        }
                    }
                }
                _addressService.Add(useraddr);
                resp.Success = true;
                resp.Message = "成功！";
                resp.Status = "00";
                resp.Result = true;
                _logger.LogError($"(/api/app/user/adduseraddr response) - {JsonConvert.SerializeObject(resp)}");
                return resp;

            }
            catch (Exception ex)
            {
                resp.Success = false;
                resp.Message = ex.Message;
                resp.Status = "99";
                resp.Result = false;
                _logger.LogError($"(/api/app/user/adduseraddr response) - {JsonConvert.SerializeObject(resp)}");
                return resp;
            }
        }

        [HttpGet]
        public Response<List<AreaResponse>> GetAreaList()
        {
            var resp = new Response<List<AreaResponse>>();
            var provinceList = _areaService.GetProvinceList();
            var areaList = new List<AreaResponse>();
            if (provinceList.Any())
            {
                areaList = provinceList.Adapt<List<AreaResponse>>();
                foreach(var city in areaList)
                {
                    var cityList = _areaService.GetCityListByPid(city.Value); 
                    var citys = cityList.Adapt<List<AreaResponse>>();
                    city.Children = citys;
                    if (city.Children.Any())
                    {
                        foreach (var area in city.Children)
                        {
                            var areas = _areaService.GetAreaListByCid(area.Value).Adapt<List<AreaResponse>>();
                            area.Children = areas;
                        }
                    }
                }
            }
            resp.Result = areaList;
            return resp;
        }
    }
}
