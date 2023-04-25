using System;
using System.Web.UI;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace OnlineShop2
{
    /*
    A page that allows users to sign up for an account
    */
    public partial class SignUpForm : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                Response.Redirect("MainScreen.aspx");
            }
        }

        // Inserts the new user into the database
        private void InsertUser()
        {
            byte[] bytes = Encoding.ASCII.GetBytes(PasswordTB.Text);
            bytes = new SHA256Managed().ComputeHash(bytes);
            string dbpassword = System.Text.Encoding.ASCII.GetString(bytes);

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

            }
            catch (SqlException)
            {
                msgLabel.Text = "Error saving details. Please try again.";
            }
            finally
            {
                db.Close();
            }

            if (Request.QueryString["redirect"] != null)
            {
                Response.Redirect(Request.QueryString["redirect"]);
            }
            else
            {
                Response.Redirect("MainScreen.aspx");
            }
        }

        // Checks if username entered is free
        private bool checkUsernameIsFree()
        {
            Database db = new Database("SELECT Username FROM Users WHERE Username = @Username");
            db.Add("@Username", UsernameTB.Text);
            bool exists = false;
            try
            {
                exists = UsernameTB.Text == db.GetString();   
            }
            catch {}
            finally
            {
                db.Close();
            }
            return exists;
        }


        // Checks data entered
        protected void Save_User(object sender, EventArgs e)
        {
            if (UsernameTB.Text == "" || NameTB.Text == "" || AddressTB.Text == "" || PostCodeTB.Text == "" || EmailAddressTB.Text == "" || PasswordTB.Text == "" || RPTPasswordTB.Text == "")
            {
                msgLabel.Text = "Please fill in all the fields";
            }
            else if (PasswordTB.Text.Length < Constants.minPasswordLength)
            {
                msgLabel.Text = "Password too short. Enter a password between " + Constants.minPasswordLength + " and " + Constants.maxPasswordLength + " characters";
            }
            else if (PasswordTB.Text.Length > Constants.maxPasswordLength)
            {
                msgLabel.Text = "Password too short. Enter a password between " + Constants.minPasswordLength + " and " + Constants.maxPasswordLength + " characters";
            }
            else if (PasswordTB.Text != RPTPasswordTB.Text)
            {
                msgLabel.Text = "Passwords do not match";
            }
            else if (UsernameTB.Text.Length <= Constants.minUsernameLength || UsernameTB.Text.Length >= Constants.maxUsernameLength)
            {
                msgLabel.Text = "Please choose a username between " + Constants.minUsernameLength + " and " + Constants.maxUsernameLength + " characters";
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