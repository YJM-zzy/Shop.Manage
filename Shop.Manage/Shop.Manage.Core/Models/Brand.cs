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
    /// 品牌表
    /// </summary>
    [Table("Brand")]
    public class Brand:Entity
    {
        /// <summary>
        /// 品牌标号
        /// </summary>
        public string BrandNo { get; set; }
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string BrandName { get; set; }
        public bool IsDeleted { get; set; }
        public string Img { get; set; }
    }
}
