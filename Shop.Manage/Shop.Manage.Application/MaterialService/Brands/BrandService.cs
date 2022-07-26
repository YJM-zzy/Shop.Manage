using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Application.MaterialService.Brands
{
    public class BrandService : ITransient, IBrandService
    {
        protected readonly IRepository<Brand> _repository;
        public BrandService(IRepository<Brand> repository)
        {
            _repository = repository;
        }
        public void Add(Brand brand)
        {
            brand.IsDeleted = false;
            brand.UpdatedTime = DateTime.Now;
            brand.CreatedTime = DateTime.Now;
            _repository.Insert(brand);
        }

        public bool Delete(int id)
        {
            var brand = GetById(id);
            if(brand != null)
            {
                brand.UpdatedTime = DateTime.Now;
                brand.IsDeleted = true;
                _repository.Update(brand);
                return true;
            }
            return false;
        }

        public IQueryable<Brand> GetAll()
        {
           return _repository.AsQueryable();
        }

        public Brand GetById(int id)
        {
           return _repository.FirstOrDefault(f=>f.Id == id && !f.IsDeleted);
        }

        public bool Update(Brand brand)
        {
            var oldbrand = GetById(brand.Id);
            if(oldbrand!=null)
            {
                oldbrand = brand;
                oldbrand.UpdatedTime = DateTime.Now;
                _repository.Update(oldbrand);
                return true;
            }
            return false;

        }
    }
}
