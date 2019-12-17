using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VenueShareReshaped.Models
{
    public class Customer
    {
        public Guid CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string HashedPassword { get; set; }
    }
}