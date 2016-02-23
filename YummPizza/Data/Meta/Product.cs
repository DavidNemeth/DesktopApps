using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [MetadataType(typeof(ProductMeta))]
    public partial class Product
    {
    }
    internal sealed class ProductMeta
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool HasOptions { get; set; }
        public Nullable<bool> IsVegetarian { get; set; }
        public Nullable<bool> WithTomatoSauce { get; set; }
        public string SizeIds { get; set; }
    }
}
