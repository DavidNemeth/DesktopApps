using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [MetadataType(typeof(OrderMeta))]
    public partial class Order
    {
    }
    internal sealed class OrderMeta
    {
        public long Id { get; set; }
        public Nullable<System.Guid> StoreId { get; set; }
        public System.Guid CustomerId { get; set; }
        public int OrderStatusId { get; set; }
        public System.DateTime OrderDate { get; set; }
        public string Phone { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public Nullable<decimal> DeliveryCharge { get; set; }
        public string DeliveryStreet { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryState { get; set; }
        public string DeliveryZip { get; set; }
        public decimal ItemsTotal { get; set; }
    }
}
