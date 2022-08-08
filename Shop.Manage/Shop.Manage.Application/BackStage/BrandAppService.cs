using Shop.Manage.Application.BackStage.Dtos;
using Shop.Manage.Application.MaterialService.Brands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Application.BackStage
{
    [Route("api/manage/[controller]")]
    [ApiDescriptionSettings("Manage")]
    [Authorize]
    public class BrandAppService : IDynamicApiController
    {
        protected readonly ILogger<BrandAppService> _logger;
        protected readonly IBrandService _brandService;
        public BrandAppService(ILogger<BrandAppService> logegr, IBrandService brandService)
        {
            _logger = logegr;
            _brandService = brandService;
        }

        [HttpPost]
        public Response<bool> AddBrand(AddBrandRequest request)
        {
            _logger.LogError($"(/api/manage/brand/addbrand request) - {JsonConvert.SerializeObject(request)}");
            var resp = new Response<bool>();
            try
            {
                var brand = request.Adapt<Brand>();
                _brandService.Add(brand);
                resp.Success = true;
                resp.Result = true;
                resp.Status = "00";
                resp.Message = "成功";
                _logger.LogError($"(/api/manage/brand/addbrand response) - {JsonConvert.SerializeObject(resp)}");
                return resp;
            }
            catch(Exception ex)
            {
                resp.Success = false;
                resp.Result = false;
                resp.Status = "99";
                resp.Message = ex.Message;
                _logger.LogError($"(/api/manage/brand/addbrand response) - {JsonConvert.SerializeObject(resp)}");
                return resp;
            }
        }

        [HttpPost]
        public Response<bool> UpdateBrand(UpdateBrandRequest request)
        {
            _logger.LogError($"(/api/manage/brand/updatebrand request) - {JsonConvert.SerializeObject(request)}");
            var resp = new Response<bool>();
            try
            {
                var brand = request.Adapt<Brand>();
                bool res = _brandService.Update(brand);
                if(res)
                {
                    resp.Success = false;
                    resp.Result = false;
                    resp.Status = "00";
                    resp.Message = "成功";
                    _logger.LogError($"(/api/manage/brand/updatebrand response) - {JsonConvert.SerializeObject(resp)}");
                    return resp;
                }
                resp.Success = false;
                resp.Result = false;
                resp.Status = "01";
                resp.Message = "品牌更新失败-品牌不存在";
                _logger.LogError($"(/api/manage/brand/updatebrand response) - {JsonConvert.SerializeObject(resp)}");
                return resp;
            }
            catch(Exception ex)
            {
                resp.Success = false;
                resp.Result = false;
                resp.Status = "99";
                resp.Message = ex.Message;
                _logger.LogError($"(/api/manage/brand/updatebrand response) - {JsonConvert.SerializeObject(resp)}");
                return resp;
            }
        }

        [HttpGet]
        public Response<List<BrandDto>> GetBrandList()
        {
            var resp =new Response<List<BrandDto>>();
            var brandlist = _brandService.GetAll().ToList().Adapt<List<BrandDto>>();
            resp.Result = brandlist;
            resp.Success = true;
            resp.Status = "00";
            _logger.LogError($"(/api/manage/brand/getbrandlist response) - {JsonConvert.SerializeObject(resp)}");
            return resp;
        }

        [HttpGet]
        public Response<bool> DeleteBrand(int id)
        {
            var resp = new Response<bool>();
            _logger.LogError($"(/api/app/manage/deletebrand request) - 品牌ID：{id}");
            try
            {
                bool res = _brandService.Delete(id);
                if (res)
                {
                    resp.Success = false;
                    resp.Result = false;
                    resp.Status = "00";
                    resp.Message = "成功";
                    _logger.LogError($"(/api/manage/brand/updatebrand response) - {JsonConvert.SerializeObject(resp)}");
                    return resp;
                }
                resp.Success = false;
                resp.Result = false;
                resp.Status = "01";
                resp.Message = "品牌删除失败-品牌不存在";
                _logger.LogError($"(/api/manage/brand/updatebrand response) - {JsonConvert.SerializeObject(resp)}");
                return resp;
            }
            catch(Exception ex)
            {
                resp.Success = false;
                resp.Result = false;
                resp.Status = "99";
                resp.Message = ex.Message;
                _logger.LogError($"(/api/manage/brand/deletebrand response) - {JsonConvert.SerializeObject(resp)}");
                return resp;
            }
        }
    }
}
