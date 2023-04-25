using System;
using System.Collections;
using System.Drawing;
using System.Web.UI.WebControls;

namespace OnlineShop2
{
    /*
    Page displays the information about an order
    */ 
    public partial class OrderInfo : SearchBar
    {
        int id;

        public int displayItem(int id)
        {
            Response.Redirect($"ItemInfo.aspx?id={id}");
            return 0;
        }

        // Displays information about an order
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("MainScreen.aspx");
            }
            else if (Session["UserId"] == null)
            {
                Response.Redirect($"Login.aspx?redirect=Order.aspx?id={Request.QueryString["id"]}");
            }
            else
            {
                id = Convert.ToInt32(Request.QueryString["id"]);
                ArrayList ids = new ArrayList();
                ArrayList quantities = new ArrayList();
                ArrayList names = new ArrayList();
                ArrayList urls = new ArrayList();


                Database db = new Database("SELECT Date, Status FROM Orders WHERE OrderID = @OrderID");
                db.Add("@OrderID", id);
                db.HasNext();
                string date = db.Get("Date").Split(' ')[0];
                string status = db.Get("Status");

                db.ChangeQuery("SELECT ProductID, Quantity FROM ProductOrders WHERE OrderID = @OrderID");
                db.Add("@OrderID", id);
                while (db.HasNext())
                {
                    ids.Add(Convert.ToInt32(db.Get("ProductID")));
                    quantities.Add(Convert.ToInt32(db.Get("Quantity")));
                }

                foreach (int product in ids)
                {
                    db.ChangeQuery("SELECT name, URL FROM Products WHERE ProductID = @ProductID");
                    db.Add("@ProductID", product);
                    db.HasNext();
                    names.Add(db.Get("name"));
                    urls.Add(db.Get("url"));
                }

                db.Close();

                TableRow dateRow = new TableRow();
                TableCell dateLabel = new TableCell();
                dateLabel.Text = "Date of Order";
                TableCell dateText = new TableCell();
                dateText.Text = date;
                dateRow.Cells.Add(dateLabel);
                dateRow.Cells.Add(new TableCell());
                dateRow.Cells.Add(dateText);
                OrderInfoTable.Rows.Add(dateRow);

                TableRow header = new TableRow();
                TableCell nameC = new TableCell();
                nameC.Text = "Item";
                nameC.CssClass = "nameText";
                TableCell quantity = new TableCell();
                quantity.Text = "Quantity";
                quantity.CssClass = "nameText";

                header.Cells.Add(nameC);
                header.Cells.Add(new TableCell());
                header.Cells.Add(quantity);

                TableRow statusRow = new TableRow();
                TableCell statusCell = new TableCell();
                TableCell statusNameCell = new TableCell();
                statusCell.Text = "Status";
                statusNameCell.Text = status;

                statusRow.Cells.Add(statusCell);
                statusRow.Cells.Add(new TableCell());
                statusRow.Cells.Add(statusNameCell);
                OrderInfoTable.Rows.Add(statusRow);

                OrderInfoTable.Rows.Add(new TableRow());
                OrderInfoTable.Rows.Add(header);
                for (int i = 0; i < ids.Count; i++)
                {
                    TableRow row = new TableRow();
                    TableCell textCell = new TableCell();
                    TableCell imageCell = new TableCell();
                    TableCell quantityCell = new TableCell();

                    WishListImageButton image = new WishListImageButton((int)ids[i], displayItem);
                    image.ImageUrl = (string)urls[i];
                    ItemDisplayButton name = new ItemDisplayButton((int)ids[i], displayItem);

                    textCell.Controls.Add(name);
                    imageCell.Controls.Add(image);

                    name.Text = string.Format((string)names[i]);
                    name.BorderColor = Color.Transparent;
                    name.BackColor = Color.Transparent;

                    quantityCell.Text = quantities[i].ToString();

                    row.Cells.Add(textCell);
                    row.Cells.Add(imageCell);
                    row.Cells.Add(quantityCell);
                    OrderInfoTable.Rows.Add(row);
                }
            }
        }

        protected void goBack(object sender, EventArgs e)
        {
            Response.Redirect("Account.aspx");
        }
    }
}