using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [MetadataType(typeof(OrderStatusMeta))]
    public partial class OrderStatus
    {
    }
    internal sealed class OrderStatusMeta
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
