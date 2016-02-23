using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [MetadataType(typeof(OrderItemOptionMeta))]
    public partial class OrderItemOption
    {
    }
    internal sealed class OrderItemOptionMeta
    {
        public long Id { get; set; }
        public Nullable<System.Guid> StoreId { get; set; }
        public long OrderItemId { get; set; }
        public int ProductOptionId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
