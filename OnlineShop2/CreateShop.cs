using System;
using System.Web.UI;
using System.Data.SqlClient;

namespace OnlineShop2
{
    // A page that allows a user to create a shop
    public partial class CreateShopForm : Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["CreateShop"] == null)
            {
                Response.Redirect("MainScreen.aspx");
            }
        }

        // Creates a new ShopID for the new shop
        private int createNewShopID()
        {
            Database db = new Database("Select MAX(ShopID) from Shops");
            int id = -1;
            try
            {
                id = db.GetInt();
            }
            catch (Exception) { }
            db.Close();
            return id + 1;
        }

        // Inserts the user as the owner in the database
        private void insertAdmin(int id)
        {
            Database db = new Database("INSERT INTO ShopManager (Username, ShopID, IsAdmin) VALUES(@Username, @ShopID, @IsAdmin)");
            db.Add("@Username", (string) Session["UserId"]);
            db.Add("@ShopID", id);
            db.Add("@IsAdmin", true);
            db.ExecuteQuery();
        }

        // Inserts a shop into the database
        private void InsertShop()
        {
            Database db = new Database("INSERT INTO Shops (ShopID, Name, Description, Address, Postcode, Email) VALUES(@ShopID, @Name, @Description, @Address, @PostCode, @Email)");
            int id = createNewShopID();
            db.Add("@ShopID", id);
            db.Add("@Name", NameTB.Text);
            db.Add("@Description", DescriptionTB.Text);
            db.Add("@Address", AddressTB.Text);
            db.Add("@PostCode", PostCodeTB.Text);
            db.Add("@Email", EmailAddressTB.Text);

            try
            {
                db.ExecuteQuery();
                insertAdmin(id);

                msgLabel.Text = "Records Inserted Successfully";

            }
            catch (SqlException)
            {
                msgLabel.Text = "Error saving details. Please try again.";
            }
            finally
            {
                db.Close();
            }
            Session["CreateShop"] = null;
            if (Request.QueryString["redirect"] != null)
            {
                Response.Redirect(Request.QueryString["redirect"]);
            }
            else
            {
                Response.Redirect("MainScreen.aspx");
            }
        }

        // Checks if the shop name entered is free
        private bool checkShopNameIsFree()
        {
            Database db = new Database("SELECT Name FROM Shops WHERE Name = @Name");
            db.Add("@Name", NameTB.Text);
            bool exists = false;
            try
            {
                exists = NameTB.Text == db.GetString();

            }
            catch (SqlException)
            {
            }
            finally
            {
                db.Close();
            }
            return exists;
        }

        // Checks all the details entered
        protected void SaveShop(object sender, EventArgs e)
        {
            if (NameTB.Text == "" || DescriptionTB.Text == "" || AddressTB.Text == "" || PostCodeTB.Text == "" || EmailAddressTB.Text == "")
            {
                msgLabel.Text = "Please fill in all the fields";
            }
            else if (checkShopNameIsFree())
            {
                msgLabel.Text = "Username already taken";
            }
            else
            {
                InsertShop();

            }
        }
       
        protected void Back(object sender, EventArgs e)
        {
            Session["CreateShop"] = null;
            if (Request.QueryString["redirect"] != null)
            {
                Response.Redirect("Login.aspx" + "?redirect=" + Request.QueryString["redirect"]);
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}