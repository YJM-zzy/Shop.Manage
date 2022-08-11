using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Core.Models
{
    [Table("TY_Province")]
    public class Province: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProvinceId { get; set; }

    }
}
