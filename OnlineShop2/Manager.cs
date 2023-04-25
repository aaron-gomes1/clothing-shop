using System;

namespace OnlineShop2
{
    /*
    Page showing the owner of the shop the details for a manager
    */ 
    public partial class Manager : SearchBar
    {
        private string username;

        // Loading the managers details
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["username"] == null || Session["UserID"] == null || !getOwner())
            {
                Response.Redirect("MainScreen.aspx");
            }

            username = Request.QueryString["username"];
            
            Database db = new Database("SELECT Count(Username) FROM ShopManagers WHERE isAdmin = @isAdmin AND ShopID IN (SELECT ShopID FROM ShopManagers WHERE Username = @Username)");
            db.Add("@Username", username);
            db.Add("@isAdmin", false);
            int num = db.GetInt();
            db.Close();

            if (num == 0)
            {
                Response.Redirect("MainScreen.aspx");
            }
            else
            {
                db = new Database("SELECT Name, Address, Email, PostCode FROM Users WHERE Username = @Username");
                db.Add("@Username", username);
                if (db.HasNext())
                {
			        ManagerUserNameLabel.Text = username;
                    ManagerNameLabel.Text = db.Get("Name");  
                    ManagerAddressLabel.Text = db.Get("Address");
                    ManagerPostCodeLabel.Text = db.Get("PostCode");
                    ManagerEmailLabel.Text = db.Get("Email");
                    db.Close();
                }
                else
                {
                    db.Close();
                    Response.Redirect("MainScreen.aspx");
                }
            }
        }

        protected void goBack(object sender, EventArgs e)
        {
            Response.Redirect("Managers.aspx");
        }
    }
}
