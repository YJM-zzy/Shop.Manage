using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Application.UserService.User
{
    public interface IAreaService
    {
        List<Province> GetProvinceList();
        List<City> GetCityListByPid(string pid);
        List<Area> GetAreaListByCid(string cid);
        string GetCityNameById(string cid);
        string GetProvinceNameById(string pid);
        string GetAreaNameById(string aid);
    }
}
