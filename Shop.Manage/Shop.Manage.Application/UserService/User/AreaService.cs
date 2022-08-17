using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Application.UserService.User
{
    public class AreaService:ITransient,IAreaService
    {
        protected readonly IRepository<Area> _areaRepository;
        protected readonly IRepository<Province> _provinceRepository;
        protected readonly IRepository<City> _cityRepository;
        public AreaService(IRepository<Area> areaRepository, IRepository<Province> provibceRepository, IRepository<City> cityRepository)
        {
            _areaRepository = areaRepository;
            _provinceRepository = provibceRepository;
            _cityRepository = cityRepository;
        }

        public List<Area> GetAreaListByCid(string cid)
        {
            return _areaRepository.Where(w=>w.CityId == cid).ToList();
        }

        public string GetAreaNameById(string aid)
        {
            return _areaRepository.FirstOrDefault(f => f.AreaId == aid)?.Name;
        }

        public List<City> GetCityListByPid(string pid)
        {
            return _cityRepository.Where(w => w.ProvinceId == pid).ToList();
        }

        public string GetCityNameById(string cid)
        {
            return _cityRepository.FirstOrDefault(f => f.CityId == cid)?.Name;
        }

        public List<Province> GetProvinceList()
        {
            return _provinceRepository.AsQueryable().ToList();
        }

        public string GetProvinceNameById(string pid)
        {
            return _provinceRepository.FirstOrDefault(f => f.ProvinceId == pid)?.Name;
        }
    }
}
