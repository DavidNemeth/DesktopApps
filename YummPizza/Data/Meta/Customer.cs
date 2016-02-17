using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Meta
{
    [MetadataType(typeof(CustomerMeta))]
    public partial class Customer
    {
    }
    internal sealed class CustomerMeta
    {        
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> StoreId { get; set; }        
        private string firstname;
        public string FirstName
        {
            get { return firstname; }
            set { firstname = value; }
        }
        private string lastname;

        public string LastName
        {
            get { return lastname; }
            set { lastname = value; }
        }
        private string phone;

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        private string street;

        public string Street
        {
            get { return street; }
            set { street = value; }
        }
        private string city;

        public string City
        {
            get { return city; }
            set { city = value; }
        }
        private string state;

        public string State
        {
            get { return state; }
            set { state = value; }
        }
        private string zip;

        public string Zip
        {
            get { return zip; }
            set { zip = value; }
        }       

        public string FullName { get { return FirstName + " " + LastName; } }
        
    }
}
