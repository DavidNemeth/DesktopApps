using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Meta
{
    [MetadataType(typeof(ProductOptionMeta))]
    public partial class ProductOption
    {
    }
    internal sealed class ProductOptionMeta
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public int Factor { get; set; }
        public bool IsPizzaOption { get; set; }
        public bool IsSaladOption { get; set; }
    }
}
