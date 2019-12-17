using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VenueShareReshaped.Models
{
    public class Venue
    {
        public Guid VenueID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Area { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
        public string Images { get; set; }
        public Guid CustomerID { get; set; }
        public bool IsApproved { get; set; }
        public bool IsPromoted { get; set; }

        public void ApproveVenue()
        {
            IsApproved = true;
        }

        public void PromoteVenue()
        {
            IsPromoted = true;
        }
    }
}