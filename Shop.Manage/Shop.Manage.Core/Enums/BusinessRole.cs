using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Core.Enums
{
    public enum BusinessRole
    {
        [Display(Name = "管理员")]
        ADMIN = 0,
        [Display(Name = "普通商家")]
        ODINARY = 1,
    }
}
