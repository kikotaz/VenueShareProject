using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VenueShareReshaped.ErrorPages
{
    public partial class UnknownError : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            string senderUrl = Request.Url.ToString();
            if (senderUrl.Contains("Admin"))
            {
                Page.MasterPageFile = "~/Admin/AdminShared.Master";
            }
            else
            {
                Page.MasterPageFile = "~/User/UserShared.Master";
            }
        }
    }
}