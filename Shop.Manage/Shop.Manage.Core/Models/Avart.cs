using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Core.Models
{
    [Table("Avart")]
    public class Avart: Entity
    {
        public byte[] ImageMessage { get; set; }
    }
}
