using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace OnlineShop2
{
    /*
    Allows managers and owners of a shop to add a product
    */ 
    public partial class AddProduct : SearchBar
    {
        // Displays the available categories
        private void insertCategories()
        {
            Database db = new Database("SELECT Name FROM Categories");
            while (db.HasNext())
            {
                string name = db.Get("Name");
                ListItem item = new ListItem();
                item.Text = name;
                item.Value = name;
                CategoryTB.Items.Add(item);
            }
            db.Close();
        }

        // Requires users to be logged in and either mamager or owner  and inserts available categories
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx?redirect=AddProduct.aspx");
            }
            else if (!getManager())
            {
                Response.Redirect("MainScreen.aspx");
            }
            insertCategories();
        }

        // Redirects to main screen
        protected void Back(object sender, EventArgs e)
        {
            Response.Redirect("MainScreen.aspx");
        }

        // Creates a new ProductID for the product
        private int createNewProductID()
        {
            Database db = new Database("Select MAX(ProductID) from Products");
            int id = -1;
            try
            {
                id = db.GetInt();
            }
            catch { }
            db.Close();
            return id + 1;
        }

        // Checks price has been entered correctly
        private bool checkPrice()
        {
            try
            {
                Convert.ToDouble(PriceTB.Text);
                
                return PriceTB.Text.Split('.')[1].Length == 2;
            }
            catch
            {
                return false;
            }
        }

        int getShopID()
        {
            Database db = new Database("SELECT ShopID FROM ShopManagers WHERE Username = @Username");
            db.Add("@Username", (string) Session["UserId"]);
            return db.GetInt();
        }

        // Inserts the new product into the database
        private void InsertProduct()
        {
            Database db = new Database("INSERT INTO Products (ProductID, Name, ShopID, Type, Category, Description, Brand, Price, Materials, Size, Colour, URL) VALUES(@ProductID, @Name, @Shop, @Type, @Category, @Description, @Brand, @Price, @Materials, @Size, @Colour, @URL)");

            int id = createNewProductID();
            db.Add("@ProductID", id);
            db.Add("@Name", NameTB.Text);
            db.Add("@Shop", getShopID());
            db.Add("@Type", TypeTB.SelectedValue);
            db.Add("@Category", CategoryTB.SelectedValue);
            db.Add("@Description", DescriptionTB.Text);
            db.Add("@Brand", BrandTB.Text);
            db.Add("@Price", PriceTB.Text);
            db.Add("@Materials", MaterialsTB.Text);
            db.Add("@Size", SizeTB.Text);
            db.Add("@Colour", ColourTB.Text);
            db.Add("@URL", URLTB.Text);

            try
            {
                db.ExecuteQuery();
                msgLabel.Text = "Product Successfully Created";
                Response.Redirect("MainScreen.aspx");
            }
            catch (SqlException)
            {
                msgLabel.Text = "Error saving details. Please try again.";
            }
            finally
            {
                db.Close();
            }
        }

        // Checks the data input is correct, displays an error if not
        protected void SaveProduct(object sender, EventArgs e)
        {
            if (NameTB.Text == "" || DescriptionTB.Text == "" || TypeTB.SelectedValue == "" || CategoryTB.SelectedValue == "" || BrandTB.Text == "" || PriceTB.Text == "" || MaterialsTB.Text == "" || SizeTB.Text == "" || ColourTB.Text == "" || URLTB.Text == "")
            {
                msgLabel.Text = "Please fill in all the fields";
            }
            else if (NameTB.Text.Length < Constants.minNameLength)
            {
                msgLabel.Text = $"Product name too short. Enter a name between {Constants.minNameLength} and {Constants.maxNameLength} characters";
            }
            else if (NameTB.Text.Length > Constants.maxNameLength)
            {
                msgLabel.Text = $"Product name too long. Enter a name between {Constants.minNameLength} and {Constants.maxNameLength} characters";
            }
            else if (DescriptionTB.Text.Length > Constants.maxDescriptionLength)
            {
                msgLabel.Text = "Brand name is too long";
            }
            else if (BrandTB.Text.Length > Constants.maxBrandLength)
            {
                msgLabel.Text = "Brand name is too long";
            }
            else if (MaterialsTB.Text.Length > Constants.maxMaterialLength)
            {
                msgLabel.Text = "Materials is too long";
            }
            else if (SizeTB.Text.Length > Constants.maxSizeLength)
            {
                msgLabel.Text = "The size is too long";
            }
            else if (ColourTB.Text.Length > Constants.maxColourLength)
            {
                msgLabel.Text = "Colour is too long";
            }
            else if (URLTB.Text.Length < Constants.minURLLength)
            {
                msgLabel.Text = "The URL provided is too short";
            }
            else if (URLTB.Text.Length > Constants.maxURLLength)
            {
                msgLabel.Text = "The URL provided is too long";
            }
            else if (!checkPrice())
            {
                msgLabel.Text = "Please enter the correct price";
            }
            else
            {
                InsertProduct();
            }
        }
    }
}