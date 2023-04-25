using System;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace OnlineShop2
{
    /*
    Form for editing account details
    */
    public partial class EditProductDetailsForm : SearchBar
    {
        private int id;

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
                NewCategoryTB.Items.Add(item);
            }
            db.Close();
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

        // Converts CategoryID to name
        private string ConvertCategoryIDToName(int id)
        {
            Database db = new Database("SELECT Name FROM Categories WHERE CategoryID = @CategoryID");
            db.Add("@CategoryID", id);
            string name = db.GetString();
            db.Close();
            return name;
        }

        // Converts name to CategoryID
        private int ConvertNameToCategoryID(string name)
        {
            Database db = new Database("SELECT CategoryID FROM Categories WHERE Name = @Name");
            db.Add("@Name", name);
            int id = db.GetInt();
            db.Close();
            return id;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else if (!getManager() || !checkID())
            {
                Response.Redirect("MainScreen.aspx");
            }
            id = Convert.ToInt32(Request.QueryString["id"]);

            insertCategories();
            if (!Page.IsPostBack)
            {
                // Displays current product data
                Database db = new Database("SELECT * FROM Products WHERE ProductID = @ProductID");
                db.Add("@ProductID", id);
                if (db.HasNext())
                {
                    NewNameTB.Text = db.Get("Name");
                    NewDescriptionTB.Text = db.Get("Description");
                    NewTypeTB.SelectedValue = db.Get("Type");
                    NewCategoryTB.SelectedValue = ConvertCategoryIDToName(Convert.ToInt32(db.Get("Category")));
                    NewBrandTB.Text = db.Get("Brand");
                    NewSizeTB.Text = db.Get("Size");
                    NewPriceTB.Text = Convert.ToDouble(db.Get("Price")).ToString();
                    NewMaterialsTB.Text = db.Get("Materials");
                    NewColourTB.Text = db.Get("Colour");
                    NewURLTB.Text = db.Get("URL");
                }

                db.Close();
            }
        }

        // Inserts the new product into the database
        private void UpdateDetails()
        {
            Database db = new Database("UPDATE Products SET Name =  @Name, Type = @Type, Category = @Category, Description = @Description, Brand = @Brand, Price = @Price, Materials = @Materials, Size = @Size, Colour = @Colour, URL = @URL WHERE ProductID = @ProductID");
            db.Add("@ProductID", id);
            db.Add("@Name", NewNameTB.Text);
            db.Add("@Type", NewTypeTB.SelectedValue);
            db.Add("@Category", ConvertNameToCategoryID(NewCategoryTB.SelectedValue).ToString());
            db.Add("@Description", NewDescriptionTB.Text);
            db.Add("@Brand", NewBrandTB.Text);
            db.Add("@Price", NewPriceTB.Text);
            db.Add("@Materials", NewMaterialsTB.Text);
            db.Add("@Size", NewSizeTB.Text);
            db.Add("@Colour", NewColourTB.Text);
            db.Add("@URL", NewURLTB.Text);

            //try
            //{
                db.ExecuteQuery();
                editMsgLabel.Text = "Product Successfully Updated";
                Response.Redirect("MainScreen.aspx");
            //}
            //catch (SqlException)
            //{
            //   editMsgLabel.Text = "Error saving details. Please try again.";
            //}
            //finally
           // {
            //    db.Close();
            //}
        }

        // Checks price has been entered correctly
        private bool checkPrice()
        {
            try
            {
                Convert.ToDouble(NewPriceTB.Text);

                return NewPriceTB.Text.Split('.')[1].Length == 2;
            }
            catch
            {
                return false;
            }
        }

        // Checks the data input is correct, displays an error if not
        protected void SaveProduct(object sender, EventArgs e)
        {
            if (NewNameTB.Text == "" || NewDescriptionTB.Text == "" || NewTypeTB.SelectedValue == "" || NewCategoryTB.SelectedValue == "" || NewBrandTB.Text == "" || NewPriceTB.Text == "" || NewMaterialsTB.Text == "" || NewSizeTB.Text == "" || NewColourTB.Text == "" || NewURLTB.Text == "")
            {
               editMsgLabel.Text = "Please fill in all the fields";
            }
            else if (NewNameTB.Text.Length < Constants.minNameLength)
            {
               editMsgLabel.Text = $"Product name too short. Enter a name between {Constants.minNameLength} and {Constants.maxNameLength} characters";
            }
            else if (NewNameTB.Text.Length > Constants.maxNameLength)
            {
               editMsgLabel.Text = $"Product name too long. Enter a name between {Constants.minNameLength} and {Constants.maxNameLength} characters";
            }
            else if (NewDescriptionTB.Text.Length > Constants.maxDescriptionLength)
            {
               editMsgLabel.Text = "Brand name is too long";
            }
            else if (NewBrandTB.Text.Length > Constants.maxBrandLength)
            {
               editMsgLabel.Text = "Brand name is too long";
            }
            else if (NewMaterialsTB.Text.Length > Constants.maxMaterialLength)
            {
               editMsgLabel.Text = "Materials is too long";
            }
            else if (NewSizeTB.Text.Length > Constants.maxSizeLength)
            {
               editMsgLabel.Text = "The size is too long";
            }
            else if (NewColourTB.Text.Length > Constants.maxColourLength)
            {
               editMsgLabel.Text = "Colour is too long";
            }
            else if (NewURLTB.Text.Length < Constants.minURLLength)
            {
               editMsgLabel.Text = "The URL provided is too short";
            }
            else if (NewURLTB.Text.Length > Constants.maxURLLength)
            {
               editMsgLabel.Text = "The URL provided is too long";
            }
            else if (!checkPrice())
            {
               editMsgLabel.Text = "Please enter the correct price";
            }
            else
            {
                UpdateDetails();
            }
        }

        protected void Back(object sender, EventArgs e)
        {
            Response.Redirect("ItemInfo.aspx?id=" + id);
        }
    }
}