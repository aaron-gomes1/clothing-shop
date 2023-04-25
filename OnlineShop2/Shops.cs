using System;
using System.Web.UI.WebControls;

namespace OnlineShop2
{
    /*
    A page loading all the shops
    */ 
    public partial class Shops : SearchBar
    {
        public int displayItem(int id)
        {
            Response.Redirect("Shop.aspx?id=" + id.ToString());
            return 0;
        }

        // Loads all the shops
        protected void Page_Load(object sender, EventArgs e)
        {
            Database db = new Database("SELECT ShopID, Name FROM Shops");
            while (db.HasNext())
            {
			    TableCell cell = new TableCell();
                cell.CssClass = "cell";
                  
			    ItemDisplayButton name = new ItemDisplayButton(Convert.ToInt32(db.Get("ShopID")), displayItem);

                TableRow row = new TableRow();
                cell.Controls.Add(name);
                name.Text = string.Format(db.Get("Name"));
                name.BorderColor = System.Drawing.Color.Transparent;
                name.BackColor = System.Drawing.Color.Transparent;
                name.CssClass = "nameText";
                row.Cells.Add(cell);

                ItemDisplayButton view = new ItemDisplayButton(Convert.ToInt32(db.Get("ShopID")), displayItem);
                view.Text = "View Shop";
                view.CssClass = "CustomButton";
                view.Width = 55;
                view.Height = 30;
                TableCell viewCell = new TableCell();
                viewCell.Controls.Add(view);
                row.Cells.Add(viewCell);

			    ShopsTable.Rows.Add(row);
           	}
            db.Close();      
        }

        protected void goBack(object sender, EventArgs e)
        {
            Response.Redirect("MainScreen.aspx");
        }
    }
}
