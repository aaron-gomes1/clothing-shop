using System;
using System.Web.UI;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Drawing;

namespace OnlineShop2
{
    /*
    Form for editing account details
    */
    public partial class EditAccountDetailsForm : SearchBar
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx?redirect=EditAccountDetails.aspx");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    // Displays current account data
                    Database db = new Database("SELECT * FROM Users WHERE Username = @UserName");
                    db.Add("@UserName", (string)Session["UserId"]);
                    if (db.HasNext())
                    {
                        NewNameTB.Text = db.Get("Name");
                        NewAddressTB.Text = db.Get("Address");
                        NewEmailAddressTB.Text = db.Get("Email");
                        NewPostCodeTB.Text = db.Get("Postcode");
                    }

                    db.Close();
                }
            }
        }

        // Updates the details edited
        private void UpdateDetails()
        {
            string password = NewPasswordTB.Text;

            if (password == "")
            {
                Database database = new Database("SELECT Password FROM Users WHERE Username = @UserName");
                database.Add("@UserName", (string)Session["UserId"]);
                password = database.GetString();
                database.Close();
            }
            else
            {
                byte[] bytes = Encoding.ASCII.GetBytes(password);
                bytes = new SHA256Managed().ComputeHash(bytes);
                password = Encoding.ASCII.GetString(bytes);
            }

            // Updates account details in database
            Database db = new Database("UPDATE Users SET Name = @Name, Address = @Address, PostCode = @PostCode, Email = @Email, Password = @Password WHERE Username = @UserName");
            db.Add("@UserName", (string) Session["UserId"]);
            db.Add("@Name", NewNameTB.Text);
            db.Add("@Address", NewAddressTB.Text);
            db.Add("@PostCode", NewPostCodeTB.Text);
            db.Add("@Email", NewEmailAddressTB.Text);
            db.Add("@Password", password);
            try
            {
                db.ExecuteQuery();
                editMsgLabel.ForeColor = Color.Green;
                editMsgLabel.Text = "Successfully Updated Details";
            }
            catch (SqlException ex)
            {
                editMsgLabel.Text = $"Error saving details. Please try again. {ex}";
            }
            finally
            {
                db.Close();
            }
        }

        // Checks edited account details
        protected void Save_User(object sender, EventArgs e)
        {
            editMsgLabel.ForeColor = Color.Red;
            if (NewNameTB.Text == "" || NewAddressTB.Text == "" || NewPostCodeTB.Text == "" || NewEmailAddressTB.Text == "")
            {
                editMsgLabel.Text = "Please fill in all the fields";
            }
            else if (NewPasswordTB.Text != "" || NewRPTPasswordTB.Text != "")
            {
                if (NewRPTPasswordTB.Text != "" && NewPasswordTB.Text == "")
                {
                    editMsgLabel.Text = "Please fill in all the fields";
                }
                else if (NewPasswordTB.Text.Length <= Constants.minPasswordLength)
                {
                    editMsgLabel.Text = $"Password too short. Enter a password between {Constants.minPasswordLength} and {Constants.maxPasswordLength} characters";
                }
                else if (NewPasswordTB.Text.Length >= Constants.maxPasswordLength)
                {
                    editMsgLabel.Text = $"Password too long. Enter a password between {Constants.minPasswordLength} and {Constants.maxPasswordLength} characters";
                }
                else if (NewPasswordTB.Text == Session["Password"].ToString())
                {
                    editMsgLabel.Text = "Please enter a different password";
                }
                else if (NewPasswordTB.Text != NewRPTPasswordTB.Text)
                {
                    editMsgLabel.Text = "Passwords do not match";
                }
                else
                {
                    UpdateDetails();
                }
            }
            else
            {
                UpdateDetails();
            }
        }

        protected void Back(object sender, EventArgs e)
        {
            Response.Redirect("Account.aspx");
        }
    }
}