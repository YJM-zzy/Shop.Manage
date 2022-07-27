using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Application.MaterialService.Classes
{
    public class ClassesService : ITransient, IClassesService
    {
        protected IRepository<MaterialClass> _repository;
        public ClassesService(IRepository<MaterialClass> repository)
        {
            _repository = repository;
        }

        public void Add(MaterialClass material)
        {
            _repository.Insert(material);
        }
    }
}
