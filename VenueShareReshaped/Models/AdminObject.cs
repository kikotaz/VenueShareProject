using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VenueShareReshaped.Models
{
    public class AdminObject
    {
        public Guid AdminID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}