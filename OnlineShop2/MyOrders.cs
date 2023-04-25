using System;
using System.Drawing;
using System.Collections;
using System.Web.UI.WebControls;

namespace OnlineShop2
{
    /*
    Displays all a users orders
    */
    public partial class MyOrders : SearchBar
    {
        public int displayOrder(int id)
        {
            Response.Redirect("OrderInfo.aspx?id=" + id);
            return 0;
        }

        // Loads all the orders for th user
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx?redirect=MyOrders.aspx");
            }

            ArrayList ids = new ArrayList();
            ArrayList dates = new ArrayList();
            ArrayList statuses = new ArrayList();

            Database db = new Database("SELECT OrderID, Date, Status FROM Orders WHERE Username = @Username");
            db.Add("@Username", (string)Session["UserId"]);
            while (db.HasNext())
            {
                ids.Add(Convert.ToInt32(db.Get("OrderID")));
                    dates.Add(db.Get("Date"));
                    statuses.Add(db.Get("Status"));
            }

            db.Close();

            for (int i = 0; i < ids.Count; i++)
            {
                TableRow row = new TableRow();
                TableCell textCell = new TableCell();
                TableCell viewCell = new TableCell();

                ItemDisplayButton name = new ItemDisplayButton((int)ids[i], displayOrder);

                name.Text = string.Format("" + ids[i] + "\t" + ((string)dates[i]).Split(' ')[0] + "\t" + statuses[i]);
                name.BorderColor = Color.Transparent;
                name.BackColor = Color.Transparent;
                name.CssClass = "nameText";

                ItemDisplayButton viewButton = new ItemDisplayButton((int)ids[i], displayOrder);

                textCell.Controls.Add(name);
                viewCell.Controls.Add(viewButton);
                viewButton.CssClass = "CustomButton";
                viewButton.Text = "View";

                row.Cells.Add(textCell);
                row.Cells.Add(viewCell);

                MyOrdersTable.Rows.Add(row);
            }
        }

        protected void goBack(object sender, EventArgs e)
        {
            Response.Redirect("MainScreen.aspx");
        }
    }
}