using System;
using System.Web.UI.WebControls;

namespace OnlineShop2
{
    /*
    Custom button that displays the manager
    */
    public class UsernameDisplayButton : Button
    {
        private string username;
        private Func<string, string> display;

        public UsernameDisplayButton(string username, Func<string, string> display)
        {
            this.username = username;
            this.display = display;
        }

        protected override void OnClick(EventArgs e)
        {
            display(username);
        }
    }

    /*
    Displays the list of managers for a shop
    */
    public partial class Managers : SearchBar
    {
        public string displayItem(string id)
        {
            Response.Redirect($"Manager.aspx?username={id}");
            return "";
        }
        protected void Page_Load(object sender, EventArgs e)
        {

		    if (Session["UserId"] == null || !getOwner()) {
			    Response.Redirect("MainScreen.aspx");
		    }

            Database db = new Database("SELECT Username FROM ShopManagers WHERE isAdmin = @isAdmin AND ShopID IN (SELECT ShopID FROM ShopManagers WHERE Username = @Username)");
            db.Add("@isAdmin", false);
            db.Add("@Username", (string) Session["UserId"]);

            while (db.HasNext())
            {
			    TableCell cell = new TableCell();
                cell.CssClass = "cell";
                  
			    UsernameDisplayButton name = new UsernameDisplayButton(db.Get("Username"), displayItem);

                TableRow row = new TableRow();
                cell.Controls.Add(name);
                name.Text = string.Format(db.Get("Username"));
                name.BorderColor = System.Drawing.Color.Transparent;
                name.BackColor = System.Drawing.Color.Transparent;
                name.CssClass = "nameText";
                row.Cells.Add(cell);

                UsernameDisplayButton view = new UsernameDisplayButton(db.Get("Username"), displayItem);
                view.Text = "View Manager\nDetails";
                view.CssClass = "CustomButton";
                view.Width = 150;
                view.Height = 60;
                TableCell viewCell = new TableCell();
                viewCell.Controls.Add(view);
                row.Cells.Add(viewCell);

			    ManagersTable.Rows.Add(row);
           	}
            db.Close();      
        }

        protected void goBack(object sender, EventArgs e)
        {
            Response.Redirect("MainScreen.aspx");
        }
    }
}
