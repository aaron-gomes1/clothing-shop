using System;
using System.Collections;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;

namespace OnlineShop2
{
    /*
    ImageButton that represents a product in the wishlist
    */
    public class WishListImageButton : ItemDisplayImageButton
    {
        public WishListImageButton(int id, Func<int, int> display) : base(id, display)
        {
            this.id = id;
            this.display = display;
            Height = 70;
            Width = 50;
        }
    }

    /*
    Page that displays the products in a wishlist
    */
    public partial class WishList : SearchBar
    {
        public int displayItem(int id)
        {
            Response.Redirect("ItemInfo.aspx?id=" + id + "");
            return 0;
        }

        // Removes item from the wishlist
        public int remove(int id)
        {
            Database db = new Database("DELETE FROM WishList WHERE ProductID = @ProductID AND Username = @Username");
            db.Add("@Username", (string)Session["UserId"]);
            db.Add("@ProductID", id);

            try
            {
                db.ExecuteQuery();
            }
            catch (SqlException)
            {
            }
            db.Close();
            Response.Redirect("WishList.aspx");
            return 0;
        }

        // Loads all of the wishlist products
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx?redirect=WishList.aspx");
            }

            ArrayList ids = new ArrayList();
            ArrayList names = new ArrayList();
            ArrayList urls = new ArrayList();

            Database db = new Database("SELECT Products.ProductID AS id, Products.name, Products.URL FROM Products, WishList WHERE WishList.ProductID = Products.ProductID AND WishList.username = @Username");
            db.Add("@Username", (string)Session["UserId"]);
            while (db.HasNext())
            {
                ids.Add(Convert.ToInt32(db.Get("id")));
                names.Add(db.Get("Name"));
                urls.Add(db.Get("URL"));
            }
            db.Close();

            for (int i = 0; i < names.Count; i++)
            {
                TableRow row = new TableRow();
                TableCell textCell = new TableCell();
                TableCell imageCell = new TableCell();
                TableCell removeCell = new TableCell();

                WishListImageButton image = new WishListImageButton((int)ids[i], displayItem);
                image.ImageUrl = (string)urls[i];
                ItemDisplayButton name = new ItemDisplayButton((int)ids[i], displayItem);

                textCell.Controls.Add(name);
                imageCell.Controls.Add(image);

                name.Text = string.Format((string)names[i]);
                name.BorderColor = Color.Transparent;
                name.BackColor = Color.Transparent;
                name.CssClass = "nameText";

                ItemDisplayButton removeButton = new ItemDisplayButton((int)ids[i], remove);

                removeCell.Controls.Add(removeButton);
                removeButton.CssClass = "CustomButton";
                removeButton.Text = "Remove";

                row.Cells.Add(textCell);
                row.Cells.Add(imageCell);
                row.Cells.Add(removeCell);
                WishListTable.Rows.Add(row);
            }
        }

        protected void goBack(object sender, EventArgs e)
        {
            Response.Redirect("MainScreen.aspx");
        }
    }
}