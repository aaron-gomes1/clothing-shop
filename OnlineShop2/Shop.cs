using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace OnlineShop2
{
    /*
    Displays information about a shop and the products it sells
    */
    public partial class Shop : SearchBar
    {
        // ShopID
        private int id;

        // Redirects to information about an item
        public int displayItem(int id)
        {
            Response.Redirect($"ItemInfo.aspx?id={id}");
            return 0;
        }

        // Displays the list of shops
        private void displayShops()
        {
            Database db = new Database("SELECT Name, Description, Address, Email, PostCode FROM Shops WHERE ShopID = @ShopID");
            db.Add("@ShopID", id);
            if (db.HasNext())
            {
                ShopNameLabel.Text = db.Get("Name");
                ShopDescriptionLabel.Text = db.Get("Description");
                ShopAddressLabel.Text = db.Get("Address");
                ShopPostCodeLabel.Text = db.Get("PostCode");
                ShopEmailLabel.Text = db.Get("Email");
            }
            else
            {
                Response.Redirect("MainScreen.aspx");
            }

            db.NewCommand("SELECT ProductID, Name, URL, Price FROM Products WHERE ShopID = @ShopID");
            db.Add("@ShopID", id);

            ArrayList ids = new ArrayList();
            ArrayList images = new ArrayList();
            ArrayList names = new ArrayList();
            ArrayList prices = new ArrayList();

            while (db.HasNext())
            {
                if (db.Get("ProductID") != null)
                {
                    ids.Add(Convert.ToInt32(db.Get("ProductID")));
                    images.Add(db.Get("URL"));
                    names.Add(db.Get("Name"));
                    prices.Add(Convert.ToDouble(db.Get("Price").ToString().Substring(0, db.Get("Price").ToString().Length - 2)));
                }
            }

            int columns = 3;

            TableRow row = new TableRow();
            for (int i = 0; i < names.Count; i++)
            {
                if (i % columns == 0 && i != 0)
                {
                    ShopProductsTable.Rows.Add(row);
                    row = new TableRow();
                }
                TableCell cell = new TableCell();
                cell.CssClass = "cell";

                ItemDisplayImageButton image = new ItemDisplayImageButton((int)ids[i], displayItem);
                image.ImageUrl = (string)images[i];
                ItemDisplayButton name = new ItemDisplayButton((int)ids[i], displayItem);

                cell.Controls.Add(image);
                cell.Controls.Add(name);

                name.Text = string.Format(names[i] + "\n£" + prices[i]);
                name.BorderColor = System.Drawing.Color.Transparent;
                name.BackColor = System.Drawing.Color.Transparent;
                name.CssClass = "nameText";
                row.Cells.Add(cell);
            }
            if (row.Cells.Count != 0)
            {
                ShopProductsTable.Rows.Add(row);
            }
        }

        // Checks the ShopID to make sure it is real
        private bool checkID()
        {
            try
            {
                id = Convert.ToInt32(Request.QueryString["id"]);
            }
            catch
            {
                return false;
            }

            if (Request.QueryString["id"] == null)
            {
                return false;
            }
            Database database = new Database("SELECT COUNT(ShopID) FROM Shops WHERE ShopID = @ShopID");
            database.Add("@ShopID", id);
            if (database.GetInt() != 1)
            {
                database.Close();
                return false;
            }
            database.Close();
            return true;
        }

        // Checks the ShopID is valid
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!checkID())
            {
                Response.Redirect("MainScreen.aspx");
            }
            id = Convert.ToInt32(Request.QueryString["id"]);

            Database db = new Database("SELECT COUNT(ShopID) FROM Shops WHERE ShopID = @ShopID");
            db.Add("@ShopID", id);
            int num = db.GetInt();
            db.Close();

            if (num == 0)
            {
                Response.Redirect("MainScreen.aspx");
            }
            else
            {
                displayShops();
            }
        }

        protected void goBack(object sender, EventArgs e)
        {
            Response.Redirect("Shops.aspx");
        }
    }
}