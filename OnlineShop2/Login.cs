using System;
using System.Security.Cryptography;
using System.Text;

namespace OnlineShop2
{

    /*
     Page to allow users to log in to account
     */ 
    public partial class LoginForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                Response.Redirect("MainScreen.aspx");
            }
        }

        // Logs user in
        private void Login()
        {
            byte[] bytes = Encoding.ASCII.GetBytes(LoginPasswordTB.Text);
            bytes = new SHA256Managed().ComputeHash(bytes);
            string dbpassword = Encoding.ASCII.GetString(bytes);

            Database db = new Database("SELECT Password from Users WHERE Username = @Username");
            db.Add("@Username", LoginUserNameTB.Text);
            if (db.HasNext())
            {
                string password = String.Format("{0}", db.Get("Password"));
                if (password == dbpassword)
                {
                    Session["UserId"] = LoginUserNameTB.Text;
                    if (Request.QueryString["redirect"] != null)
                    {
                        Response.Redirect(Request.QueryString["redirect"]);
                    }
                    else {
                        Response.Redirect("MainScreen.aspx");
                    }
                }
                else
                {
                    loginLabel.Text = "Invalid Username/Password";
                }
            }
            else
            {
                loginLabel.Text = "Invalid Username/Password";
            }
        }

        protected void Loginbtn_Click(object sender, EventArgs e)
        {
            if (LoginUserNameTB.Text == "" || LoginPasswordTB.Text == "")
            {
                loginLabel.Text = "Invalid Username/Password";
            }
            else {
                Login();
            }
        }

        // Redirects to sign up
        protected void Signupbtn_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["redirect"] != null)
            {
                Response.Redirect($"SignUp.aspx?redirect={Request.QueryString["redirect"]}");
            }
            else
            {
                Response.Redirect("SignUp.aspx");
            }
        }

        // Redirects to manager sign in
        protected void goToManagerSignUp(object sender, EventArgs e)
        {
            if (Request.QueryString["redirect"] != null)
            {
                Response.Redirect($"ManagerSignUp.aspx?redirect={Request.QueryString["redirect"]}");
            }
            else
            {
                Response.Redirect("ManagerSignUp.aspx");
            }
        }
    }
}