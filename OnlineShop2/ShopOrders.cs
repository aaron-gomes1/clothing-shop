using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace OnlineShop2
{
    /*
     Custom Button to allow OrderID and status of order to be changed when clicked
     */
    public class ShopOrderChangeStatusButton : Button
    {
        private int id;
	    private string status;
        private Func<int, string, int> change;

        public ShopOrderChangeStatusButton(int id, string status, Func<int, string, int> change)
        {
            this.id = id;
		    this.status = status;
            this.change = change;
        }

        protected override void OnClick(EventArgs e)
        {
            change(id, status);
        }
    }

    /*
     Page to allow managers to look at and manage shop orders
    */
    public partial class ShopOrders : SearchBar
    {
        // Changes the status of an order
        public int changeStatus(int id, string status)
        {
            Database db = new Database("UPDATE Orders SET Status = @Status WHERE OrderID = @OrderID");
            db.Add("@Status", status);
            db.Add("@OrderID", id);
            try
            {
                db.ExecuteQuery();
            }
            catch { }
            db.Close();
            Response.Redirect("ShopOrders.aspx");
            return 0;
        }

        public int displayItem(int id)
        {
            Response.Redirect($"OrderInfo.aspx?id={id}");
            return 0;
        }

        // Loads all orders by category for a shop
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null || !getManager())
            {
                Response.Redirect("MainScreen.aspx");
            }

            ArrayList activeIds = new ArrayList();
            ArrayList activeDates = new ArrayList();
            ArrayList fufilledIds = new ArrayList();
            ArrayList fufilledDates = new ArrayList();
            ArrayList deliveredIds = new ArrayList();
            ArrayList deliveredDates = new ArrayList();

            Database db = new Database("SELECT ShopID FROM ShopManagers WHERE Username = @Username");
            db.Add("@Username", (string) Session["UserId"]);
            int shopid = db.GetInt();

            db.NewCommand("SELECT OrderID, Date FROM Orders WHERE ShopID = @ShopID AND Status = @Status");
            db.Add("@ShopID", shopid);
            db.Add("@Status", "Active");

            while (db.HasNext())
            {
                activeIds.Add(Convert.ToInt32(db.Get("OrderID")));
                DateTime date = DateTime.Parse(db.Get("Date"));
                activeDates.Add(date.Day + "/" + date.Month + "/" + date.Year);
            }

            db.NewCommand("SELECT OrderID, Date FROM Orders WHERE ShopID = @ShopID AND Status = @Status");
            db.Add("@ShopID", shopid);
            db.Add("@Status", "Fulfilled");

            while (db.HasNext())
            {
                fufilledIds.Add(Convert.ToInt32(db.Get("OrderID")));
                DateTime date = DateTime.Parse(db.Get("Date"));
                fufilledDates.Add(date.Day + "/" + date.Month + "/" + date.Year);
            }

            db.NewCommand("SELECT OrderID, Date FROM Orders WHERE ShopID = @ShopID AND Status = @Status");
            db.Add("@ShopID", shopid);
            db.Add("@Status", "Delivered");

            while (db.HasNext())
            {
                deliveredIds.Add(Convert.ToInt32(db.Get("OrderID")));
                DateTime date = DateTime.Parse(db.Get("Date"));
                deliveredDates.Add(date.Day + "/" + date.Month + "/" + date.Year);
            }
            db.Close();
            for (int i = 0; i < activeIds.Count; i++)
            {
                TableRow row = new TableRow();
                TableCell ordercell = new TableCell();
                TableCell datecell = new TableCell();
                TableCell buttoncell = new TableCell();
                ordercell.CssClass = "cell";
                datecell.CssClass = "cell";

                ItemDisplayButton order = new ItemDisplayButton((int)activeIds[i], displayItem);
                ItemDisplayButton date = new ItemDisplayButton((int)activeIds[i], displayItem);
                ShopOrderChangeStatusButton status = new ShopOrderChangeStatusButton((int)activeIds[i], "Fulfilled", changeStatus);

                order.Text = ((int)activeIds[i]).ToString();
                date.Text = (string)activeDates[i];

                ordercell.Controls.Add(order);
                datecell.Controls.Add(date);
                buttoncell.Controls.Add(status);

                order.BorderColor = System.Drawing.Color.Transparent;
                order.BackColor = System.Drawing.Color.Transparent;
                order.CssClass = "nameText";
                date.BorderColor = System.Drawing.Color.Transparent;
                date.BackColor = System.Drawing.Color.Transparent;
                date.CssClass = "nameText";
                status.CssClass = "CustomButton";
                status.Width = 160;
                status.Text = "Change to Fufilled";
                row.Cells.Add(ordercell);
                row.Cells.Add(datecell);
                row.Cells.Add(buttoncell);
                ActiveShopOrdersTable.Rows.Add(row);
            }

            for (int i = 0; i < fufilledIds.Count; i++)
            {
                TableRow row = new TableRow();
                TableCell ordercell = new TableCell();
                TableCell datecell = new TableCell();
                TableCell buttoncell = new TableCell();
                ordercell.CssClass = "cell";
                datecell.CssClass = "cell";

                ItemDisplayButton order = new ItemDisplayButton((int)fufilledIds[i], displayItem);
                ItemDisplayButton date = new ItemDisplayButton((int)fufilledIds[i], displayItem);
                ShopOrderChangeStatusButton status = new ShopOrderChangeStatusButton((int)fufilledIds[i], "Delivered", changeStatus);

                order.Text = ((int)fufilledIds[i]).ToString();
                date.Text = (string)fufilledDates[i];

                ordercell.Controls.Add(order);
                datecell.Controls.Add(date);
                buttoncell.Controls.Add(status);

                order.BorderColor = System.Drawing.Color.Transparent;
                order.BackColor = System.Drawing.Color.Transparent;
                order.CssClass = "nameText";
                date.BorderColor = System.Drawing.Color.Transparent;
                date.BackColor = System.Drawing.Color.Transparent;
                date.CssClass = "nameText";
                status.CssClass = "LoginButton";
                status.Width = 160;
                status.Text = "Change to Delivered";
                row.Cells.Add(ordercell);
                row.Cells.Add(datecell);
                row.Cells.Add(buttoncell);
                FufilledShopOrdersTable.Rows.Add(row);
            }

            for (int i = 0; i < deliveredIds.Count; i++)
            {
                TableRow row = new TableRow();
                TableCell ordercell = new TableCell();
                TableCell datecell = new TableCell();
                ordercell.CssClass = "cell";
                datecell.CssClass = "cell";

                ItemDisplayButton order = new ItemDisplayButton((int)deliveredIds[i], displayItem);
                ItemDisplayButton date = new ItemDisplayButton((int)deliveredIds[i], displayItem);

                order.Text = ((int)deliveredIds[i]).ToString();
                date.Text = (string)deliveredDates[i];

                ordercell.Controls.Add(order);
                datecell.Controls.Add(date);

                order.BorderColor = System.Drawing.Color.Transparent;
                order.BackColor = System.Drawing.Color.Transparent;
                order.CssClass = "nameText";
                date.BorderColor = System.Drawing.Color.Transparent;
                date.BackColor = System.Drawing.Color.Transparent;
                date.CssClass = "nameText";
                row.Cells.Add(ordercell);
                row.Cells.Add(datecell);
                DeliveredShopOrdersTable.Rows.Add(row);
            }
        }

        protected void goBack(object sender, EventArgs e)
        {
            Response.Redirect("MainScreen.aspx");
        }
    }
}
