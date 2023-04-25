using System;

namespace OnlineShop2
{
    /*
    Main page
    */ 
    public partial class MainScreen : SearchBar
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // Gets the CategoryID associated with the name of category 
        private int getID(string id)
        {
            Database database = new Database("SELECT CategoryID FROM Categories WHERE Name = @Name");
            database.Add("@Name", id);
            return database.GetInt();
        }

        // Redirect to the dresses filter
        protected void goToDresses(object sender, EventArgs e)
        {
            Response.Redirect("ItemsDisplay.aspx?category=" + getID("Dress"));
        }

        // Redirects to the formal filter
        protected void goToFormal(object sender, EventArgs e)
        {
            Response.Redirect("ItemsDisplay.aspx?category=" + getID("Formal"));
        }

        // Redirects to the shoes filter
        protected void goToShoes(object sender, EventArgs e)
        {
            Response.Redirect("ItemsDisplay.aspx?category=" + getID("Shoes"));
        }
    }
}