using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Core.Enums
{
    public enum MaterialStatusEnum
    {
        [Display(Name = "上架")]
        SHELEVS = 0,
        [Display(Name = "下架")]
        DOWN = 1,
    }
}
