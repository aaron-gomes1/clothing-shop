using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace OnlineShop2
{
    /*
     Button for rating a product
    */ 
    public class RatingButton : LinkButton
    {
        private int id;
        private Func<int, int> display;

        public RatingButton(int id, Func<int, int> display)
        {
            this.id = id;
            this.display = display;
        }

        protected override void OnClick(EventArgs e)
        {
            display(id);
        }
    }

    /*
    Page for displaying product
    */
    public partial class ItemInfo : SearchBar
    {
        // ProductID for the product 
        private int id;


        // Adds item to basket
        protected void buy(object sender, EventArgs e)
        {
            if (Session["Basket"] == null)
            {
                Session["Basket"] = new ArrayList { id };
            }
            else
            {
                ((ArrayList)Session["Basket"]).Add(id);
            }
        }

        // Updates the product as a user favourite
        protected void favourite(object sender, EventArgs e)
        {
            if (Session["userId"] == null)
            {
                Response.Redirect("Login.aspx?redirect=ItemInfo.aspx?id=" + id);
            }

            Database db = new Database("SELECT COUNT(ProductID) FROM WishList WHERE ProductID = @ProductID AND Username = @Username");
            db.Add("@Username", (string)Session["UserId"]);
            db.Add("@ProductID", id);
            int value = db.GetInt();

            string query = "INSERT INTO WishList(Username, ProductID) VALUES (@Username, @ProductID)";
            WishListButton.Text = "Add To Wish List";
            if (value == 1)
            {
                WishListButton.Text = "Remove From Wish List";
                query = "DELETE FROM WishList WHERE ProductID = @ProductID AND Username = @Username";
            }

            db.NewCommand(query);
            db.Add("@Username", (string)Session["UserId"]);
            db.Add("@ProductID", id);
            db.ExecuteQuery();

            db.Close();
            Response.Redirect("ItemInfo.aspx?id=" + id);
        }

        // Updates the users rating for the product
        private int updateRating(int rating)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            Database db = new Database("SELECT COUNT(Rating) From Ratings WHERE ProductID = @ProductID AND Username = @Username");
            db.Add("@Username", (string) Session["UserId"]);
            db.Add("@ProductID", id);
            int value = db.GetInt();
            db.Close();

            string query = "INSERT INTO Ratings(ProductID, Username, Rating) Values(@ProductID, @Username, @Rating)";
            if (value == 1)
            {
                query = "UPDATE Ratings SET Rating = @Rating WHERE ProductID = @ProductID AND Username = @Username";
            }

            db = new Database(query);
            db.Add("@Username", (string)Session["UserId"]);
            db.Add("@ProductID", id);
            db.Add("@Rating", rating);
            db.Close();

            Response.Redirect("ItemInfo.aspx?id=" + id);
            return 0;
        }

        // Gets product rating from database
        private int getRating()
        {
            Database db = new Database("SELECT COUNT(Rating) From Ratings WHERE ProductID = @ProductID AND Username = @Username");
            db.Add("@Username", (string)Session["UserId"]);
            db.Add("@ProductID", id);

            if (db.GetInt() == 1)
            {
                db.Close();
                Database data = new Database("SELECT Rating From Ratings WHERE ProductID = @ProductID AND Username = @Username");
                data.Add("@Username", (string)Session["UserId"]);
                data.Add("@ProductID", id);
                int rating = data.GetInt();
                data.Close();
                return rating;
            }
            else
            {
                db.Close();
                return 0;
            }
        }

        // Gets the average rating for a product
        private ArrayList getProductRatings()
        {
            int num = 0;
            int rating = 0;
            Database db = new Database("SELECT Rating From Ratings WHERE ProductID = @ProductID");
            db.Add("@ProductID", id);
            while (db.HasNext())
            {
                rating += Convert.ToInt32(db.Get("Rating"));
                ++num;
            }
            db.Close();

            if (num == 0)
            {
                return new ArrayList() { 0.0, 0 };
            }
            return new ArrayList() { Math.Round(Convert.ToDouble(rating / num), 1), num };
        }


        // Displays the ratings for a product
        private void createProductRatings()
        {
            ArrayList ratings = getProductRatings();
            int value = Convert.ToInt32(Math.Round((double)ratings[0]));
            int num = (int) ratings[1];
            
            TableRow row = new TableRow();
            for (int i = 1; i <= 5; i++)
            {
                Label label = new Label();
                label.CssClass = "fa-regular fa-star";
                if (value > 0)
                {
                    label.CssClass = "fa-solid fa-star checked";
                    --value;
                }
                label.Width = 30;
                label.Height = 30;
                TableCell c = new TableCell();
                c.Controls.Add(label);
                row.Cells.Add(c);
            }
            TableCell info = new TableCell();
            Label infoLabel = new Label();
            infoLabel.Text = ratings[0] + "/5 (" + num + " ratings)";
            info.Controls.Add(infoLabel);
            row.Cells.Add(info);
            UserRatingTable.Rows.Add(row);
        }


        // Displays the users rating and gets them to enter a rating
        private void createRating()
        {
            int value = 0;
            if (Session["UserId"] != null)
            {
                value = getRating();
            }
            TableRow row = new TableRow();
            for (int i = 1; i <= 5; i++)
            {

                RatingButton btn = new RatingButton(i, updateRating);
                btn.CssClass = "fa-regular fa-star";
                if (value > 0)
                {
                    btn.CssClass = "fa-solid fa-star checked";
                    --value;
                }
                btn.Width = 30;
                btn.Height = 30;
                TableCell c = new TableCell();
                c.Controls.Add(btn);
                row.Cells.Add(c);
            }
            RatingTable.Rows.Add(row);
        }

        // Checks the ProductID to make sure it is real
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
            Database database = new Database("SELECT COUNT(ProductID) FROM Products WHERE ProductID = @ProductID");
            database.Add("@ProductID", id);
            if (database.GetInt() != 1)
            {
                database.Close();
                return false;
            }
            database.Close();
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!checkID())
            {
                Response.Redirect("MainScreen.aspx");
            }
            id = Convert.ToInt32(Request.QueryString["id"]);

            Database db = new Database("SELECT COUNT(ProductID) FROM WishList WHERE ProductID = @ProductID AND Username = @Username");
            if (Session["UserId"] != null)
            {
                db.Add("@Username", (string)Session["UserId"]);
                db.Add("@ProductID", id);
                if (db.GetInt() == 1)
                {
                    WishListButton.Text = "Remove From Wish List";
                }
            }
            db.NewCommand("SELECT Products.Name, Products.Type, Products.Description, Products.Price, Products.Brand, Products.Materials, Products.Size, Products.Colour, Products.URL, Categories.Name AS Category, Shops.Name AS Shop FROM Products, Categories, Shops WHERE Products.ShopID = Shops.ShopID AND Products.Category = Categories.CategoryID AND ProductID = @ProductID");
            db.Add("@ProductID", id);
            if (db.HasNext())
            {
                ItemInfoNameLabel.Text = db.Get("Name");
                ItemInfoDescriptionLabel.Text = db.Get("Description");
                ItemInfoPriceLabel.Text = "£" + db.Get("Price").ToString().Substring(0, db.Get("Price").ToString().Length - 2);
                ItemInfoTypeLabel.Text = db.Get("Type");
                ItemInfoBrandLabel.Text = db.Get("Brand");
                ItemInfoCategoryLabel.Text = db.Get("Category");
                ItemInfoShopLabel.Text = db.Get("Shop");
                ItemInfoMaterialsLabel.Text = db.Get("Materials");
                ItemInfoSizeLabel.Text = db.Get("Size");
                ItemInfoColourLabel.Text = db.Get("Colour");
                ItemInfoImage.ImageUrl = db.Get("URL");
                db.Close();
            }
            else
            {
                db.Close();
                Response.Redirect("MainScreen.aspx");
            }
            
            createProductRatings();
            createRating();
        }

        protected void editProduct(object sender, EventArgs e)
        {
            Response.Redirect("EditProductDetails.aspx?id=" + id);
        }

        protected void goBack(object sender, EventArgs e)
        {
            Response.Redirect("ItemsDisplay.aspx");
        }
    }
}
