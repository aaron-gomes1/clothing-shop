using System;
using System.Web.UI;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;

namespace OnlineShop2
{
    public partial class ManagerSignUpForm : Page
    {
        // Lists the available a manager can manage
        private void loadShops()
        {
            Database db = new Database("SELECT Name FROM Shops");
            ShopTB.Items.Add("I want to create a new shop");
            while (db.HasNext())
            {
                ShopTB.Items.Add(db.Get("Name"));
            }
            db.Close();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                Response.Redirect("MainScreen.aspx");
            }
            loadShops();
        }

        // Gets the ShopID of the shop the account will manage
        private int getShopID(string name)
        {
            Database db = new Database("SELECT ShopID FROM Shops WHERE Name = @Name");
            db.Add("@Name", name);
            return db.GetInt();
        }

        // Inserts the new manager into the database
        private void insertManager()
        {
            Database db = new Database("INSERT INTO ShopManagers (Username, ShopID, isAdmin) VALUES(@Username, @ShopID, @isAdmin)");
            db.Add("@Username", UsernameTB.Text);
            db.Add("@ShopID", getShopID(ShopTB.SelectedItem.Text));
            db.Add("@isAdmin", false);
            db.ExecuteQuery();
        }

        // Inserts the new manager as a new user
        private void InsertUser()
        {
            byte[] bytes = Encoding.ASCII.GetBytes(PasswordTB.Text);
            bytes = new SHA256Managed().ComputeHash(bytes);
            string dbpassword = Encoding.ASCII.GetString(bytes);

            string newShopString = "I want to create a new shop";

            Database db = new Database("INSERT INTO Users (Username, Name, Address, Postcode, Email,Password) VALUES(@UserName, @Name, @Address, @PostCode,@Email,@Password)");
            db.Add("@UserName", UsernameTB.Text);
            db.Add("@Name", NameTB.Text);
            db.Add("@Address", AddressTB.Text);
            db.Add("@PostCode", PostCodeTB.Text);
            db.Add("@Email", EmailAddressTB.Text);
            db.Add("@Password", dbpassword);

            try
            {
                db.ExecuteQuery();
                Session["UserId"] = UsernameTB.Text;
                msgLabel.Text = "Records Inserted Successfully";

                if (ShopTB.SelectedItem.Text != newShopString)
                {
                    insertManager();
                }
            }
            catch (SqlException)
            {
                msgLabel.Text = "Error saving details. Please try again.";
            }
            finally
            {
                db.Close();
            }

            if (ShopTB.SelectedItem.Text == newShopString) {
                Session["CreateShop"] = true;
                if (Request.QueryString["redirect"] != null)
                {
                    Response.Redirect($"CreateShop.aspx?{Request.QueryString["redirect"]}");
                }
                else
                {
                    Response.Redirect("CreateShop.aspx");
                }
            }
            else
            {
                if (Request.QueryString["redirect"] != null)
                {
                    Response.Redirect(Request.QueryString["redirect"]);
                }
                else
                {
                    Response.Redirect("MainScreen.aspx");
                }
            }
        }

        // Checks if the username entered is free
        private bool checkUsernameIsFree()
        {
            Database db = new Database("SELECT Username FROM Users WHERE Username = @Username");
            db.Add("@Username", UsernameTB.Text);
            bool exists = false;
            try
            {
                exists = UsernameTB.Text == db.GetString();
                
            }
            catch (SqlException){}
            finally
            {
                db.Close();
            }
            return exists;
        }

        // Checks the details enters are valid
        protected void Save_User(object sender, EventArgs e)
        {
            if (UsernameTB.Text == "" || NameTB.Text == "" || AddressTB.Text == "" || PostCodeTB.Text == "" || EmailAddressTB.Text == "" || PasswordTB.Text == "" || RPTPasswordTB.Text == "")
            {
                msgLabel.Text = "Please fill in all the fields";
            }
            else if (PasswordTB.Text.Length < Constants.minPasswordLength)
            {
                msgLabel.Text = $"Password too short. Enter a password between {Constants.minPasswordLength} and {Constants.maxPasswordLength} characters";
            }
            else if (PasswordTB.Text.Length > Constants.maxPasswordLength)
            {
                msgLabel.Text = $"Password too short. Enter a password between {Constants.minPasswordLength} and {Constants.maxPasswordLength} characters";
            }
            else if (PasswordTB.Text != RPTPasswordTB.Text)
            {
                msgLabel.Text = "Passwords do not match";
            }
            else if (UsernameTB.Text.Length < Constants.minUsernameLength || UsernameTB.Text.Length > Constants.maxUsernameLength)
            {
                msgLabel.Text = $"Please choose a username between {Constants.minUsernameLength} and {Constants.maxUsernameLength} characters";
            }
            else if (checkUsernameIsFree())
            {
                msgLabel.Text = "Username already taken";
            }
            else
            {
                InsertUser();
            }
        }

        protected void Back(object sender, EventArgs e)
        {
            if (Request.QueryString["redirect"] != null)
            {
                Response.Redirect($"Login.aspx?redirect={Request.QueryString["redirect"]}");
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}