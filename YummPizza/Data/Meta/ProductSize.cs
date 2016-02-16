using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Meta
{
    [MetadataType(typeof(ProductSizeMeta))]
    public partial class ProductSize
    {
    }
    internal sealed class ProductSizeMeta
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Nullable<decimal> PremiumPrice { get; set; }
        public Nullable<decimal> ToppingPrice { get; set; }
        public Nullable<bool> IsGlutenFree { get; set; }
    }
}
