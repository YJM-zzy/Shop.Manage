using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furion.DatabaseAccessor;
using Shop.Manage.Core.Enums;

namespace Shop.Manage.Core.Models
{
    /// <summary>
    /// 商家表
    /// </summary>
    [Table("BusinessMessage")]
    public class BusinessMessage : Entity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 统一信用码
        /// </summary>
        public string CreditCode { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public BusinessRole Role { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public int? AvartId { get; set; }
        [ForeignKey("AvartId")]
        public Avart Avart { get; set; }
        public bool IsDeleted { get; set; }
        
    }
}
