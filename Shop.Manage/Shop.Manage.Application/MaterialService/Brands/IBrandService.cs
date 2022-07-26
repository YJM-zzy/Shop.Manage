using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Application.MaterialService.Brands
{
    public interface IBrandService
    {
        IQueryable<Brand> GetAll();
        Brand GetById(int id);
        void Add(Brand brand);
        bool Update(Brand brand);
        bool Delete(int id);
    }
}
