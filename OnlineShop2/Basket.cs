using System;
using System.Collections;
using System.Drawing;
using System.Web.UI.WebControls;

namespace OnlineShop2
{
    /*
    Basket for products to be purchased
    */ 
    public partial class Basket : SearchBar
    {
        public int displayItem(int id)
        {
            Response.Redirect($"ItemInfo.aspx?id={id}");
            return 0;
        }

        // Removes an item from the basket
        public int remove(int id)
        {
            ArrayList purchased = (ArrayList) Session["Basket"];
            purchased.Remove(id);
            Session["Basket"] = purchased;
            Response.Redirect("Basket.aspx");
            return 0;
        }
        
        // Redirects to pay for items
        protected void Pay(object sender, EventArgs e)
        {
            if (Session["Basket"] != null)
            {
                Response.Redirect("PayScreen.aspx");
            }
        }

        // Displays all items in basket
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx?redirect=Basket.aspx");
            }
            if (Session["Basket"] == null)
            {
                Session["Basket"] = new ArrayList();
                PayButton.Visible = false;
            }

            ArrayList ids = (ArrayList) Session["Basket"];
            ArrayList names = new ArrayList();
            ArrayList urls = new ArrayList();
            ArrayList prices = new ArrayList();

            Database db = new Database();

            foreach (int id in (ArrayList)Session["Basket"])
            {
                db.NewCommand("SELECT Name, Price, URL FROM Products WHERE ProductID = @ProductID");
                db.Add("@ProductID", id);
                db.HasNext();
                names.Add(db.Get("Name"));
                prices.Add(Convert.ToDouble(db.Get("Price")));
                urls.Add(db.Get("URL"));
            }

            double total = 0;
            foreach (double price in prices)
            {
                total += price;
            }
            PriceLabel.Text = "Total: £" + total.ToString();
            PriceLabel.CssClass = "textLabel";

            db.Close();

            for (int i = 0; i < names.Count; i++)
            {
                TableRow row = new TableRow();
                TableCell textCell = new TableCell();
                TableCell imageCell = new TableCell();
                TableCell priceCell = new TableCell();
                TableCell removeCell = new TableCell();

                WishListImageButton image = new WishListImageButton((int)ids[i], displayItem);
                image.ImageUrl = (string)urls[i];
                ItemDisplayButton name = new ItemDisplayButton((int)ids[i], displayItem);
                ItemDisplayButton price = new ItemDisplayButton((int)ids[i], displayItem);

                textCell.Controls.Add(name);
                imageCell.Controls.Add(image);
                priceCell.Controls.Add(price);

                name.Text = string.Format((string)names[i]);
                name.BorderColor = Color.Transparent;
                name.BackColor = Color.Transparent;
                name.CssClass = "nameText";

                price.Text = "£" + prices[i];
                price.BorderColor = Color.Transparent;
                price.BackColor = Color.Transparent;
                price.CssClass = "nameText";

                ItemDisplayButton removeButton = new ItemDisplayButton((int)ids[i], remove);

                removeCell.Controls.Add(removeButton);
                removeButton.CssClass = "CustomButton";
                removeButton.Text = "Remove";

                row.Cells.Add(textCell);
                row.Cells.Add(imageCell);
                row.Cells.Add(priceCell);
                row.Cells.Add(removeCell);
                BasketTable.Rows.Add(row);
            }
        }

        protected void goBack(object sender, EventArgs e)
        {
            Response.Redirect("MainScreen.aspx");
        }
    }
}