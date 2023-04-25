using System;

namespace OnlineShop2
{
    /*
    The searchbar at the top of the page
    */ 
    public partial class SearchBar : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.TextBox NavBarSearch;
        
        // Gets if the logged in user is a manager only
        protected bool getManagerOnly()
        {
            Database db = new Database("SELECT Count(*) FROM ShopManagers WHERE Username = @Username AND isAdmin = @isAdmin");
            db.Add("@Username", (string) Session["UserId"]);
            db.Add("@isAdmin", false);
            bool value = db.GetInt() > 0;
            db.Close();
            return value;
        }

        // Gets if the logged in user is a manager or owner
        protected bool getManager()
        {
            Database db = new Database("SELECT Count(*) FROM ShopManagers WHERE Username = @Username");

            db.Add("@Username", (string) Session["UserId"]);
            bool value = db.GetInt() > 0;
            db.Close();
            return value;
        }

        // Gets if the logged in user is an owner only
        protected bool getOwner()
        {
            Database db = new Database("SELECT Count(*) FROM ShopManagers WHERE Username = @Username AND isAdmin = @isAdmin");
            db.Add("@Username", (string) Session["UserId"]);
            db.Add("@isAdmin", true);
            bool value = db.GetInt() > 0;
            db.Close();
            return value;
        }

        protected void goToItems(object sender, EventArgs e)
        {
            Response.Redirect("ItemsDisplay.aspx");
        }

        protected void goToAccount(object sender, EventArgs e)
        {
            Response.Redirect("Account.aspx");
        }

        protected void goToWishList(object sender, EventArgs e)
        {
            Response.Redirect("WishList.aspx");
        }

        protected void goToBasket(object sender, EventArgs e)
        {
            Response.Redirect("Basket.aspx");
        }

        protected void logout(object sender, EventArgs e)
        {
            Session["UserId"] = null;
            Response.Redirect("MainScreen.aspx");
        }

        protected void login(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void goHome(object sender, EventArgs e)
        {
            Response.Redirect("MainScreen.aspx");
        }

        protected void goToAddProduct(object sender, EventArgs e)
        {
            Response.Redirect("AddProduct.aspx");
        }

        protected void goToOrders(object sender, EventArgs e)
        {
            Response.Redirect("ShopOrders.aspx");
        }

        protected void goToManagers(object sender, EventArgs e)
        {
            Response.Redirect("Managers.aspx");
        }

        protected void search(object sender, EventArgs e)
        {
            if (NavBarSearch.Text != "")
            {
                Response.Redirect($"ItemsDisplay.aspx?search={NavBarSearch.Text}");
            }

        }
    }
}