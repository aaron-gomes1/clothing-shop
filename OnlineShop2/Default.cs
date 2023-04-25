using System;
using System.Collections;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace OnlineShop2
{
    /**
     Contains the constants used in the software 
     */
    public class Constants
    {
        public static string DB_CONNECTION = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog='Shopping database';Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        
        public static int minUsernameLength = 5;
        public static int maxUsernameLength = 20;
        public static int minPasswordLength = 8;
        public static int maxPasswordLength = 20;

        public static int minNameLength = 10;
        public static int maxNameLength = 50;
        public static int maxDescriptionLength = 250;
        public static int maxBrandLength = 50;
        public static int maxMaterialLength = 50;
        public static int minURLLength = 15;
        public static int maxURLLength = 250;
        public static int maxColourLength = 30;
        public static int maxSizeLength = 20;

        public static int cardNumLength = 16;
        public static int CVVLength = 3;
    }

    /*
     Allow Database queries to be created and executed, with parameters passed in
     */
    public class Database
    {
        private string query;
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;

        public Database()
        {
            connection = new SqlConnection(Constants.DB_CONNECTION);
            command = new SqlCommand("", connection);
            connection.Open();
        }

        public Database(string query)
        {
            this.query = query;
            connection = new SqlConnection(Constants.DB_CONNECTION);
            connection.Open();
            command = new SqlCommand(this.query, connection);
        }

        public void Add(string param, string value)
        {
            command.Parameters.AddWithValue(param, value);
        }

        public void Add(string param, int value)
        {
            command.Parameters.AddWithValue(param, value);
        }

        public void Add(string param, bool value)
        {
            command.Parameters.AddWithValue(param, value);
        }

        public bool HasNext()
        {
            if (reader == null)
            {
                reader = command.ExecuteReader();
            }
            return reader.Read();
        }

        public int GetInt()
        {
            return (int) command.ExecuteScalar();
        }

        public string GetString()
        {
            return (string) command.ExecuteScalar();
        }

        public string Get(string param)
        {
            return reader[param].ToString();
        }

        public void ExecuteQuery()
        {
            command.ExecuteNonQuery();
        }

        public void NewCommand(string query)
        {
            this.query = query;
            try
            {
                if (reader != null) {
                    reader.Close();
                }
            }
            catch { }
            reader = null;
            command = new SqlCommand(query, this.connection);
        }

        public void ChangeQuery(string query)
        {
            this.query = query;
            command.CommandText = this.query;
        }

        public void Close()
        {
            connection.Close();
        }
    }

    /*
     Page that sets up the software and databse, after setup redirects to MainScreen
     */
    public partial class DefaultForm : System.Web.UI.Page
    {
        private SqlConnection con;

        private void openDatabase()
        {
            con = new SqlConnection(Constants.DB_CONNECTION);
            con.Open();
        }

        private void closeDatabase()
        {
            con.Close();
        }

        private void checkSeed(string table, Func<int> seed)
        {
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM " + table, con);
            try
            {
                int count = Convert.ToInt32(command.ExecuteScalar());
                if (count == 0)
                {
                    seed();
                }
            }
            catch (Exception)
            {
            }
        }

        private void insertSeed(string table, ArrayList names, ArrayList values)
        {
            string query = "INSERT INTO " + table + "(" + names[0];

            for (int i = 1; i < names.Count; i++)
            {
                query += ", " + names[i];
            }
            query += ") VALUES(@" + names[0];
            for (int i = 1; i < names.Count; i++)
            {
                query += ", @" + names[i];
            }
            query += ")";
            SqlCommand cmd = new SqlCommand(query, con);

            for (int i = 0; i < names.Count; i++)
            {
                cmd.Parameters.AddWithValue("@" + names[i], values[i]);
            }

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
            }
        }

        private void createDatabase()
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand cmd = new SqlCommand("CREATE DATABASE \"Shopping Database\"", connection);
            connection.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
            }
            finally
            {
                connection.Close();
            }
        }

        private void createTables()
        {
            ArrayList commands = new ArrayList()
            {
                "CREATE TABLE Users (Username VARCHAR (20) NOT NULL, Password VARCHAR (50) NOT NULL, Name VARCHAR (50) NOT NULL, Address VARCHAR (50) NOT NULL, Postcode VARCHAR (50) NOT NULL, Email VARCHAR (50) NOT NULL, PRIMARY KEY (Username));",
                "CREATE TABLE Shops(ShopID INT NOT NULL, Name VARCHAR(30) NOT NULL, Description VARCHAR(100) NOT NULL, Address VARCHAR (50) NOT NULL, Postcode VARCHAR (50) NOT NULL, Email VARCHAR (50) NOT NULL, PRIMARY KEY(ShopID));",
                "CREATE TABLE Categories (CategoryID INT NOT NULL, Name VARCHAR (50) NOT NULL, Description VARCHAR (50) NOT NULL, PRIMARY KEY (CategoryID));",
                "CREATE TABLE Products (ProductID INT NOT NULL, Name VARCHAR(50)  NOT NULL, ShopID INT NOT NULL, Type VARCHAR(30) NOT NULL, Category INT NOT NULL, Description VARCHAR(250) NOT NULL, Brand VARCHAR(50)  NOT NULL, Price SMALLMONEY NOT NULL, Materials VARCHAR(50) NOT NULL, Size VARCHAR(50)  NOT NULL, Colour VARCHAR(50)  NOT NULL, URL VARCHAR(250) NOT NULL, PRIMARY KEY (ProductID), FOREIGN KEY(ShopID) REFERENCES Shops(ShopID), FOREIGN KEY(Category) REFERENCES Categories(CategoryID));",
                "CREATE TABLE WishList (Username VARCHAR(20) NOT NULL, ProductID INT NOT NULL, PRIMARY KEY (Username, ProductID), FOREIGN KEY (Username) REFERENCES Users(Username), FOREIGN KEY (ProductID) REFERENCES Products(ProductID));",
                "CREATE TABLE Orders(OrderID INT NOT NULL, Username VARCHAR(20) NOT NULL, ShopID INT NOT NULL, Date DATE NOT NULL, Status VARCHAR(15) NOT NULL, PRIMARY KEY(OrderID), FOREIGN KEY(Username) REFERENCES Users(Username), FOREIGN KEY(ShopID) REFERENCES Shops(ShopID));",
                "CREATE TABLE ProductOrders(OrderID INT NOT NULL, ProductID INT NOT NULL, Quantity INT NOT NULL, PRIMARY KEY(OrderID, ProductID), FOREIGN KEY (OrderID) REFERENCES Orders(OrderID), FOREIGN KEY (ProductID) REFERENCES Products(ProductID));",
                "CREATE TABLE ShopManagers(ShopID INT NOT NULL, Username VARCHAR(20) NOT NULL, IsAdmin BIT NOT NULL, PRIMARY KEY(ShopID, Username), FOREIGN KEY (ShopID) REFERENCES Shops(ShopID), FOREIGN KEY (Username) REFERENCES Users(Username))",
                "CREATE TABLE Ratings(ProductID INT NOT NULL, Username VARCHAR(20), Rating INT NOT NULL, PRIMARY KEY(ProductID, Username), FOREIGN KEY(ProductID) REFERENCES Products(ProductID), FOREIGN KEY(Username) REFERENCES Users(Username))"
            };
            foreach (string command in commands)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(command, con);
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                }
            }
        }

        private int seedCategories()
        {
            ArrayList names = new ArrayList() {
                "Top",
                "Trousers",
                "Dress",
                "Jumper",
                "Coat",
                "Shoes",
                "Socks",
                "Summer Wear",
                "Winter Wear",
                "Formal",
                "Underwear",
                "Thermal Wear",
                "Accessories",
                "Other"
            };

            ArrayList descriptions = new ArrayList() {
                "Shirts and t-shirts",
                "Trousers",
                "Dresses",
                "Jumpers and hoodies",
                "Coats",
                "Shoes",
                "Socks",
                "Clothes for the summer",
                "Clothes for the winter",
                "Formal blazers",
                "Underwear",
                "Thermal Wear",
                "Accessories",
                "Any clothes that don\'t fit the other categories"
            };

            for (int i = 0; i < names.Count; i++)
            {
                ArrayList columns = new ArrayList() { "CategoryID", "Name", "Description" };
                ArrayList values = new ArrayList() { i+1, (string)names[i], (string)descriptions[i] };
                
                insertSeed("Categories", columns, values);
            }
            return 0;
        }

        private int seedProducts()
        {
            ArrayList names = new ArrayList() { 
                "Pink Stripe Jumper",
                "Blue Jumper",
                "Animal Print Dress",
                "Womens Formal Blazer",
                "Black Check Polo Shirt",
                "Black Denim Shorts",
                "Stone Patterned T-Shirt",
                "Girls Waterproof Jacket",
                "Mens Blue Jeans"
            };
            ArrayList descriptions = new ArrayList() {
                "Pink Stripy Jumper for Women",
                "A nice Blue Jumper",
                "A nice Dress",
                "A Formal Blazer for Women",
                "Black Smart Check Panel Polo Shirt",
                "Black Relaxed Denim Shorts",
                "A Front Stone Panelled T-Shirt With plain back",
                "Girls Grey Waterproof Jacket for 5-7 year olds",
                "Blue jeans for Men, slim fit"
            };
            ArrayList shops = new ArrayList() { 1, 2, 1, 2, 1, 2, 1, 2, 1};
            ArrayList categories = new ArrayList() { 4, 4, 3, 10, 1, 2, 1, 5, 2 };
            ArrayList types = new ArrayList() { "Womens", "Womens", "Womens", "Womens", "Mens", "Mens", "Mens", "Girls", "Mens" };
            ArrayList brands = new ArrayList() { "BooHoo", "Nike", "Papaya", "Et Vous", "Et Vous", "Papaya", "George", "George", "Denim" };
            ArrayList prices = new ArrayList() { 9.99, 24.99, 36.00, 79.99, 13.00, 15.00, 10.00, 35.00, 14.00  };
            ArrayList materials = new ArrayList() { "Wool", "Cotton", "Polyester", "Polyester", "Cotton", "Cotton", "Cotton", "Polyester", "Cotton" };
            ArrayList sizes = new ArrayList() { "Medium", "Small", "Small", "Medium", "Large", "Medium", "Small", "5-7yrs", "Small" };
            ArrayList colours = new ArrayList() { "Pink", "Blue", "Multicoloured", "Black", "Black and white", "Black", "Grey", "Grey", "Blue" };
            ArrayList urls = new ArrayList()
            {
                "https://matalan-content.imgix.net/uploads/asset_file/asset_file/447012/1668178737.3864925-S2933731_C323_Alt1.jpg?ixlib=rails-4.2.0&cs=tinysrgb&auto=compress%2Cformat&fm=pjpg&w=300&fit=crop&ar=300%3A0&s=eb54fdd8be74b444fc15be880123bb67",
                "https://matalan-content.imgix.net/uploads/asset_file/asset_file/446967/1668177795.2478518-S2933735_C128_Alt1.jpg?ixlib=rails-4.2.0&cs=tinysrgb&auto=compress%2Cformat&fm=pjpg&w=300&fit=crop&ar=300%3A0&s=96054e868039221a88ea432b0b061145",
                "https://matalan-content.imgix.net/uploads/asset_file/asset_file/447158/1668179884.6264195-S2934960_C586_Alt1.jpg?ixlib=rails-4.2.0&cs=tinysrgb&auto=compress%2Cformat&fm=pjpg&w=300&fit=crop&ar=300%3A0&s=9cc13659d88c967ce355568",
                "https://matalan-content.imgix.net/uploads/asset_file/asset_file/406145/1655391813.3994746-S2907890_C101_Alt1.jpg?ixlib=rails-4.2.0&cs=tinysrgb&auto=compress%2Cformat&fm=pjpg&w=300&fit=crop&ar=300%3A0&s=de890a38224854ee95bc18173fd2ab1b",
                "https://ml.thcdn.com/productimg/401/456/14474431-1225037187087548.jpg",
                "https://ml.thcdn.com/productimg/960/960/14516817-1285040841753113.jpg",
                "https://ml.thcdn.com/productimg/960/960/14517468-9275040315546645.jpg",
                "https://ml.thcdn.com/productimg/381/533/14492853-3315036992534512.jpg",
                "https://ml.thcdn.com/productimg/960/960/14413143-1975036990622447.jpg",
            };

            for (int i = 0; i < names.Count; i++)
            {
                ArrayList columns = new ArrayList() { "ProductID", "Name", "ShopID", "Type", "Category", "Description", "Brand", "Price", "Materials", "Size", "Colour", "URL" };
                ArrayList values = new ArrayList() { i+1, (string)names[i], (int)shops[i], (string)types[i], (int)categories[i], (string)descriptions[i], (string)brands[i], (double)prices[i], (string)materials[i], (string)sizes[i], (string)colours[i], (string)urls[i] };
                
                insertSeed("Products", columns, values);
            }
            return 0;
        }

        private int seedShops()
        {
            ArrayList names = new ArrayList() { "Matalan", "TK Maxx" };
            ArrayList descriptions = new ArrayList() { "High Quality and fashionable clothes", "Affordable clothes for you" };
            ArrayList addresses = new ArrayList() { "123 Matalan Way", "10 Oxford Street" };
            ArrayList postcodes = new ArrayList() { "E15 1FP", "WC1 7VE" };
            ArrayList emails = new ArrayList() { "queries@matalan.co.uk", "info@tkmaxx.com" };

            for (int i = 0; i < names.Count; i++)
            {
                ArrayList columns = new ArrayList() { "ShopID", "Name", "Description", "Address", "Postcode", "Email" };
                ArrayList values = new ArrayList() { i+1, (string)names[i], (string)descriptions[i], (string)addresses[i], (string)postcodes[i], (string)emails[i] };

                insertSeed("Shops", columns, values);
            }
            return 0;
        }

        private int seedUsers()
        {
            ArrayList usernames = new ArrayList() { "johnsmith" };
            ArrayList passwords = new ArrayList() { "password" };
            ArrayList names = new ArrayList() { "John Smith" };
            ArrayList addresses = new ArrayList() { "1 The Drive" };
            ArrayList postcodes = new ArrayList() { "IG8 0TP" };
            ArrayList emails = new ArrayList() { "johnsmith@gmail.com" };

            for (int i = 0; i < usernames.Count; i++)
            {
                byte[] bytes = Encoding.ASCII.GetBytes((string)passwords[i]);
                bytes = new SHA256Managed().ComputeHash(bytes);
                string password = System.Text.Encoding.ASCII.GetString(bytes);

                ArrayList columns = new ArrayList() { "Username", "Name", "Password", "Address", "Postcode", "Email" };
                ArrayList values = new ArrayList() { (string)usernames[i], (string)names[i], password, (string)addresses[i], (string)postcodes[i], (string)emails[i] };

                insertSeed("Users", columns, values);
            }
            return 0;
        }

        private int seedShopManagers()
        {
            ArrayList usernames = new ArrayList() { "johnsmith" };
            ArrayList shopids = new ArrayList() { 1 };
            ArrayList admins = new ArrayList() { true };

            for (int i = 0; i < usernames.Count; i++)
            {
                ArrayList columns = new ArrayList() { "Username", "ShopID", "IsAdmin" };
                ArrayList values = new ArrayList() { (string)usernames[i], (int)shopids[i], (bool)admins[i] };

                insertSeed("ShopManagers", columns, values);
            }
            return 0;
        }

        private int seedWishList()
        {
            ArrayList usernames = new ArrayList() { "johnsmith", "johnsmith" };
            ArrayList products = new ArrayList() { 1, 2 };

            for (int i = 0; i < usernames.Count; i++)
            {
                ArrayList columns = new ArrayList() { "Username", "ProductID" };
                ArrayList values = new ArrayList() { (string) usernames[i], (int) products[i] };
                
                insertSeed("WishList", columns, values);
            }
            return 0;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            createDatabase();
            openDatabase();
            createTables();
            checkSeed("Users", seedUsers);
            checkSeed("Shops", seedShops);
            checkSeed("ShopManagers", seedShopManagers);
            checkSeed("Categories", seedCategories);
            checkSeed("Products", seedProducts);
            checkSeed("WishList", seedWishList);
            closeDatabase();
            Response.Redirect("MainScreen.aspx");
        }
    }
}