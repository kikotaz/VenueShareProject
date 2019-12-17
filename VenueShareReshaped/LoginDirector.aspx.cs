using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VenueShareReshaped
{
    /*
     * @ClassName: LoginDirector
     * @Description: This class will be the code behind for LoginDirector.aspx page. It will
     * handle all the requests and back code requirements. This class is responsible for
     * showing the proper login page to the user
     * @Developer: Karim Saleh
     */
    public partial class LoginDirector : System.Web.UI.Page
    {
        //This method will decide the proper login page to show
        protected void Page_Load(object sender, EventArgs e)
        {
            char[] separator = { '/' };

            string[] path = Request["ReturnUrl"].Split(separator);

            if (path[1] == "Admin")
            {

                Response.Redirect("/Admin/AdminLogin.aspx");

            }
            else if (path[1] == "User")
            {
                Response.Redirect("/ErrorPages/LoginRequired.aspx");
            }
        }
    }
}