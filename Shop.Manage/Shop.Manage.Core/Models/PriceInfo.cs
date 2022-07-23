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
    /// 价格表
    /// </summary>
    [Table("PriceInfo")]
    public class PriceInfo:Entity
    {
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MatNo { get; set; }
        /// <summary>
        /// 商家Id
        /// </summary>
        public int BusinessId { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
    }
}
