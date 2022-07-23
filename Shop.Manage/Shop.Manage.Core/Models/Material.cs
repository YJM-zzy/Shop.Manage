using Furion.DatabaseAccessor;
using Shop.Manage.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Core.Models
{
    /// <summary>
    /// 物料表
    /// </summary>
    [Table("Material")]
    public class Material:Entity
    {
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MatNo { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public int? ClassId { get; set; }
        [ForeignKey("ClassId")]
        public MaterialClass Class { get; set; }
        /// <summary>
        /// 规格型号
        /// </summary>
        public string Spec { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public MaterialStatusEnum Status { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public int? PriceId { get; set; }
        [ForeignKey("PriceId")]
        public PriceInfo Price { get; set; }
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Img { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public int? BrandId { get; set; }
        public Brand Brands { get; set; }

    }
}
