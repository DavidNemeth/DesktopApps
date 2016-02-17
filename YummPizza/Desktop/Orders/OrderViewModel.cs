using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Orders
{
    class OrderViewModel : BindableBase
    {
        private Guid _CustomerId;
        public Guid CustomerId
        {
            get { return _CustomerId; }
            set { SetProperty(ref _CustomerId, value); }
        }
    }
}
