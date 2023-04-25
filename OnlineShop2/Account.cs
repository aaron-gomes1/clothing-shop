using System;

namespace OnlineShop2
{
    /*
    Shows account details and navigation
    */
    public partial class Account : SearchBar
    {

        // Shows account details on screen
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx?redirect=Account.aspx");
            }

            Database db = new Database("SELECT * FROM Users WHERE Username = @UserName");
            db.Add("@UserName", (string)Session["UserId"]);
            if (db.HasNext())
            {
                AccountUserNameLabel.Text = (string) Session["UserId"];
                AccountNameLabel.Text = db.Get("Name");
                AccountAddressLabel.Text = db.Get("Address");
                AccountEmailLabel.Text = db.Get("Email");
                AccountPostCodeLabel.Text = db.Get("PostCode");
            }
            db.Close();
        }

        // Redirects page to edit account details
        protected void goToEditAccountDetails(object sender, EventArgs e)
        {
            Response.Redirect("EditAccountDetails.aspx");
        }

        // Redirects page to users orders
        protected void goToMyOrders(object sender, EventArgs e)
        {
            Response.Redirect("MyOrders.aspx");
        }

        // Redirects to main screen
        protected void goBack(object sender, EventArgs e)
        {
            Response.Redirect("MainScreen.aspx");
        }
    }
}
