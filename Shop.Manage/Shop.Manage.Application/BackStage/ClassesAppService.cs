using Shop.Manage.Application.MaterialService.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Application.BackStage
{
    public class ClassesAppService:IDynamicApiController
    {
        protected readonly IClassesService _classesService;
        protected readonly ILogger<ClassesAppService> _logger;
        public ClassesAppService(IClassesService classesService,ILogger<ClassesAppService> logger)
        {
            _logger = logger;
            _classesService = classesService;
        }


    }
}
