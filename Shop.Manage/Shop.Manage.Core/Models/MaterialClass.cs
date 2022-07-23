using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Core.Models
{
    /// <summary>
    /// 物料分类表
    /// </summary>
    [Table("MaterialClass")]
    public class MaterialClass:Entity
    {
        public string ClassNo { get; set; }
        public string Name { get; set; }
        [ForeignKey("MaterialClassId")]
        public MaterialClass MaterialClasses { get; set; }
        public int? MaterialClassId { get; set; }
        public string Img { get; set; }
        public bool IsDeleted { get; set; }
    }
}
