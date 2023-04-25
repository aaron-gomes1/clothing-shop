using System;
using System.Collections;
using System.Collections.Generic;

namespace OnlineShop2
{
    /*
    Page to pay for items
    */
    public partial class PayScreen : SearchBar
    {
        // Creates new OrderID
        private int createNewOrderID()
        {
            Database db = new Database("Select MAX(OrderID) from Orders");
            int id = -1;
            try
            {
                id = db.GetInt();
            }
            catch (Exception) { }
            db.Close();
            return id + 1;
        }

        // Inserts the order into the database
        private void insertOrder(ArrayList record, Dictionary<int, int> products)
        {
            DateTime date = DateTime.Now;
            string d = date.Year + "-" + date.Month + "-" + date.Day;

            int orderid = createNewOrderID();
            int shopid = (int)record[0];

            Database db = new Database("INSERT INTO Orders (OrderID, Username, ShopID, Date, Status) VALUES(@OrderID, @Username, @ShopID, @Date, @Status);");
            db.Add("@OrderID", orderid);
            db.Add("@Username", (string)Session["UserId"]);
            db.Add("@ShopID", shopid);
            db.Add("@Date", d);
            db.Add("@Status", "Active");
            try
            {
                db.ExecuteQuery();
            }
            catch (Exception) { }

            for (int x = 1; x < record.Count; x++)
            {
                db.NewCommand("INSERT INTO ProductOrders (OrderID, ProductID, Quantity) VALUES(@OrderID, @ProductID, @Quantity);");
                db.Add("@OrderID", orderid);
                db.Add("@ProductID", (int) record[x]);
                db.Add("@Quantity", products[(int) record[x]]);
                try
                {
                    db.ExecuteQuery();
                }
                catch (Exception) { }
            }
        }

        // Processes the payment
        private void process()
        {
            Database db = new Database();

            ArrayList ids = (ArrayList)Session["Basket"];
            ArrayList tmp = new ArrayList();

            HashSet<int> uniqueIds = new HashSet<int>();
            foreach (int id in ids)
            {
                uniqueIds.Add(id);
            }
            Dictionary<int, int> pdcts = new Dictionary<int, int>();
            foreach (int unique in uniqueIds)
            {
                tmp.Add(unique);
                int count = 0;
                foreach (int id in ids)
                {
                    if (unique == id)
                    {
                        count += 1;
                    }
                }
                pdcts.Add(unique, count);
            }

            string commandString = "SELECT ShopID, ProductID FROM Products";
            int idcount = 1;
            foreach (int prodid in uniqueIds)
            {
                if (idcount == 1)
                {
                    commandString += " WHERE ProductID = @ProductID" + idcount;
                    db.Add("@ProductID" + idcount, prodid);
                }
                else
                {
                    commandString += " OR ProductID = @ProductID" + idcount;
                    db.Add("@ProductID" + idcount, prodid);
                }
                idcount++;
            }
            commandString += " ORDER BY ShopID;";
            db.ChangeQuery(commandString);
            ArrayList records = new ArrayList();

            ArrayList tm = new ArrayList();

            db.HasNext();
            int current = Convert.ToInt32(db.Get("ShopID"));
            tm.Add(current);
            tm.Add(Convert.ToInt32(db.Get("ProductID")));

            while (db.HasNext())
            {
                if (Convert.ToInt32(db.Get("ShopID")) != current)
                {
                    records.Add(tm);
                    current = Convert.ToInt32(db.Get("ShopID"));
                    tm = new ArrayList();
                    tm.Add(current);
                }
                tm.Add(Convert.ToInt32(db.Get("ProductID")));
            }
            records.Add(tm);
            db.Close();
            foreach (ArrayList record in records)
            {
                insertOrder(record, pdcts);
            }
            
            Session["Basket"] = null;
            Response.Redirect("MainScreen.aspx");
        }

        protected void Back(object sender, EventArgs e)
        {
            Response.Redirect("MainScreen.aspx");
        }

        // Checks a number entered is a number
        private bool checkNum(string num)
        {
            foreach (char i in num)
            {
                try
                {
                    int.Parse(i.ToString());
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        // Checks the date
        private bool checkDate(string date)
        {
            if (date.Length != 5 || date[2] != '/')
            {
                return false;
            }
            foreach (char i in date)
            {
                if (i != '/') {
                    try
                    {
                        int.Parse(i.ToString());
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // Checks the details entered are valid
        protected void Pay(object sender, EventArgs e)
        {
            if (NameTB.Text == "" || CardNumTB.Text == "" || ExpiryTB.Text == "" || CVVTB.Text == "")
            {
                msgLabel.Text = "Please fill in all the fields";
            }
            else if (CardNumTB.Text.Length != Constants.cardNumLength || !checkNum(CardNumTB.Text))
            {
                msgLabel.Text = "Card Number is wrong";
            }
            else if (CVVTB.Text.Length != Constants.CVVLength || !checkNum(CVVTB.Text))
            {
                msgLabel.Text = "CVV Number is wrong";
            }
            else if (!checkDate(ExpiryTB.Text))
            {
                msgLabel.Text = "Expiry date is wrong";
            }
            else
            {
                process();
            }
        }

        // Gets the total price to be paid
        private double getTotal()
        {

            Database db = new Database();
            double total = 0;
            foreach (int id in (ArrayList)Session["Basket"])
            {
                db.NewCommand("SELECT Price FROM Products WHERE ProductID = @ProductID");
                db.Add("@ProductID", id);
                db.HasNext();
                total+=Convert.ToDouble(db.Get("Price"));
            }
            db.Close();
            return total;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx?redirect=PayScreen.aspx");
            }
            if (Session["Basket"] == null)
            {
                Response.Redirect("MainScreen.aspx");
            }
            PriceLabel.Text = $"To Pay: £{getTotal()}";
            PriceLabel.CssClass = "textLabel";
        }
    }
}