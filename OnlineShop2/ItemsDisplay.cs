using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;

namespace OnlineShop2
{
    /*
     Manages the filter section and generates URL on submit
     */
    public class FilterCheckBoxManager
    {
        private ArrayList filterCheckBoxes;

        public FilterCheckBoxManager()
        {
            filterCheckBoxes = new ArrayList();
        }

        // Adds a filter item
        public void Add(CheckBox button)
        {
            filterCheckBoxes.Add(button);
        }

        // Gets all the filter items that were checked
        public ArrayList getAllChecked()
        {
            ArrayList tmp = new ArrayList() { };
            foreach(CheckBox button in filterCheckBoxes)
            {
                if (button.Checked)
                {
                    tmp.Add(button.ID);
                }
            }
            return tmp;
        }

        // Generate URL query for filter
        public string generateQuery(bool multi, string filter)
        {
            ArrayList brands = getAllChecked();
            string tmp = "";
            if (multi)
            {
                tmp = "&";
            }
            tmp += filter + "=";
            tmp += brands[0];
            for (int pos = 1; pos < brands.Count; pos++)
            {
                tmp += "," + brands[pos];
            }
            return tmp;
        }
    }

    /*
     Custom Button to allows ProductID to be passed to function whyen pressed
     */
    public class ItemDisplayButton : Button
    {
        private int id;
        private Func<int, int> display;

        public ItemDisplayButton(int id, Func<int, int> display)
        {
            this.id = id;
            this.display = display;
        }

        protected override void OnClick(EventArgs e)
        {
            display(id);
        }
    }

    /*
     Custom ImageButton to allow ProductID to be passed to function when pressed
     */
    public class ItemDisplayImageButton : ImageButton
    {
        protected int id;
        protected Func<int, int> display;

        public ItemDisplayImageButton(int id, Func<int, int> display)
        {
            this.id = id;
            this.display = display;
            Height = 450;
            Width = 300;
        }

        protected override void OnClick(ImageClickEventArgs e)
        {
            display(id);
        }
    }

    /*
    Page for displaying and filtering through items
    */ 
    public partial class ItemsDisplay : SearchBar
    {
        // Filters
        FilterCheckBoxManager shopListener;
        FilterCheckBoxManager sizeListener;
        FilterCheckBoxManager brandListener;
        FilterCheckBoxManager colourListener;
        FilterCheckBoxManager categoryListener;
        FilterCheckBoxManager typeListener;

        private static int filterDisplayLimit = 7;

        public int displayItem(int id)
        {
            Response.Redirect($"ItemInfo.aspx?id={id}");
            return 0;
        }

        // Removes filter
        protected int clearFilter(int i)
        {
            Response.Redirect("ItemsDisplay.aspx");
            return 0;
        }

        // Gets all the database queries based on some parameters
        private ArrayList convertToIDs(ArrayList names, string table, string name, string id)
        {
            ArrayList ids = new ArrayList();

            Database db = new Database();
            foreach (string n in names)
            {
                db.NewCommand($"SELECT {id} FROM {table} WHERE {name} = @{name}");
                db.Add("@" + name, n);
                ids.Add(db.GetInt());
            }

            db.Close();
            return ids;
        }

        // Generates URL query for search
        private string generateIDSearchQuery(ArrayList ids, string filter, bool multi)
        {
            string tmp = "";
            if (multi)
            {
                tmp = "&";
            }
            tmp += filter + "=";
            tmp += ids[0];
            for (int pos = 1; pos < ids.Count; pos++)
            {
                tmp += "," + ids[pos];
            }
            return tmp;
        }

        // Creates filters
        protected int searchFilter(int i)
        {
            string query = "ItemsDisplay.aspx?";
            bool multi = false;
            if (MinPrice.Text != null && MaxPrice.Text != null)
            {
                try
                {
                    double minPrice = double.Parse(MinPrice.Text);
                    double maxPrice = double.Parse(MaxPrice.Text);
                    if (maxPrice >= minPrice)
                    {
                        query += $"price={minPrice},{maxPrice}";
                        multi = true;
                    }
                }
                catch (Exception)
                {
                }
            }

            ArrayList listeners = new ArrayList() { sizeListener, brandListener, colourListener, typeListener};
            ArrayList filters = new ArrayList() { "size", "brand", "colour", "type"};

            for (int listener = 0; listener < listeners.Count; listener++)
            {
                FilterCheckBoxManager filter = (FilterCheckBoxManager) listeners[listener];
                if (filter.getAllChecked().Count > 0)
                {
                    query += filter.generateQuery(multi, (string)filters[listener]);
                    multi = true;
                }
            }

            if (shopListener.getAllChecked().Count > 0)
            {
                ArrayList ids = convertToIDs(shopListener.getAllChecked(), "Shops", "Name", "ShopID");

                query += generateIDSearchQuery(ids, "shop", multi);
                multi = true;
            }
            if (categoryListener.getAllChecked().Count > 0)
            {
                ArrayList ids = convertToIDs(categoryListener.getAllChecked(), "Categories", "Name", "CategoryID");

                query += generateIDSearchQuery(ids, "category", multi);
            }
            Response.Redirect(query);
            return 0;
        }

        // Creates Buttons for filters
        private void createFilterButtons()
        {
            TableRow bottomRow = new TableRow();

            ItemDisplayButton search = new ItemDisplayButton(0, searchFilter);
            search.Text = "Search";
            search.CssClass = "CustomButton";
            search.Width = 50;
            search.Height = 30;

            ItemDisplayButton clear = new ItemDisplayButton(0, clearFilter);
            clear.OnClientClick = "clear";
            clear.Text = "Clear";
            clear.CssClass = "CustomButton";
            clear.Width = 50;
            clear.Height = 30;

            TableCell searchCell = new TableCell();
            TableCell clearCell = new TableCell();

            searchCell.Controls.Add(search);
            clearCell.Controls.Add(clear);

            bottomRow.Cells.Add(searchCell);
            bottomRow.Cells.Add(clearCell);

            Filter.Rows.Add(bottomRow); 
        }

        // Generates partial database query for search
        private string generateSearchQuery(bool multi, string filter)
        {
            string tmp = " AND";
            if (!multi)
            {
                tmp = " WHERE";
            }
            string[] num = Request.QueryString[filter.ToLower()].Split(',');

            tmp += " " + filter + " = @" + filter + "1";
            for (int i = 1; i < num.Length; i++)
            {
                tmp += " OR " + filter + " = @" + filter + (i + 1);
            }
            return tmp;
        }

        // Gets names of items from a table
        private HashSet<string> convertToNames(HashSet<string> ids, string table, string id)
        {
            HashSet<string> names = new HashSet<string>();

            Database db = new Database();

            foreach (string n in ids)
            {
                db.NewCommand($"SELECT Name FROM {table} WHERE {id} = @{id}");
                db.Add(id, Convert.ToInt32(n));

                names.Add(db.GetString());
            }

            db.Close();
            return names;
        }

        // Generates part of the database for a filter
        private string generateIDQuery(bool multi, string filter, string id)
        {
            string tmp = " AND";
            if (!multi)
            {
                tmp = " WHERE";
            }

            tmp += $" {id} = @{id}1";
            string[] num = Request.QueryString[filter].Split(',');
            for (int i = 1; i < num.Length; i++)
            {
                tmp += $" OR {id} = @{id}{i + 1}";
            }
            return tmp;
        }

        // Adds the content for one filter
        private void addFilterRows(Table table, FilterCheckBoxManager manager, HashSet<string> set, string filter)
        {
            if (set.Count > 0)
            {
                TableRow nameRow = new TableRow();
                TableCell nameCell = new TableCell();

                nameCell.Text = filter;
                nameRow.Cells.Add(nameCell);
                table.Rows.Add(nameRow);
            }
            int limit = set.Count;
            if (limit > filterDisplayLimit)
            {
                limit = filterDisplayLimit;
            }
            int count = 0;
            foreach (string i in set)
            {
                if (count < limit) {
                    CheckBox checkbox = new CheckBox();
                    checkbox.ID = i;
                    manager.Add(checkbox);
                    TableRow r = new TableRow();
                    TableCell c = new TableCell();
                    TableCell name = new TableCell();
                    name.Text = i;
                    r.CssClass = "filtertable";
                    r.Width = 100;
                    c.Controls.Add(checkbox);
                    r.Cells.Add(c);
                    r.Cells.Add(name);
                    table.Rows.Add(r);
                }
                count++;
            }
        }

        // Adds arguments to the database query
        private void addCommandValues(Database db, string filter)
        {
            string[] num = Request.QueryString[filter.ToLower()].Split(',');
            for (int i = 0; i < num.Length; i++)
            {
                db.Add($"@{filter}{i + 1}", num[i]);
            }
        }

        // Adds arguments to the database query
        private void addIDCommandValues(Database db, string filter, string id)
        {
            string[] num = Request.QueryString[filter.ToLower()].Split(',');
            for (int i = 0; i < num.Length; i++)
            {
                db.Add($"@{id}{i + 1}", num[i]);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            createFilterButtons();
            string query = "SELECT * FROM Products";

            ArrayList ids = new ArrayList();
            ArrayList images = new ArrayList();
            ArrayList names = new ArrayList();
            ArrayList prices = new ArrayList();
            Database db = new Database(query);

            bool multi = false;

            bool price = false;
            bool search = false;

            price = Request.QueryString["price"] != null;

            if (Request.QueryString["search"] != null)
            {
                search = true;
                ItemTitle.Text = $"Search Results for \"{Request.QueryString["search"]}\"";
            }

            string[] filters = new string[] { "Brand", "Colour", "Size", "Type"};
            foreach (string filter in filters)
            {
                if (Request.QueryString[filter.ToLower()] != null)
                {
                    query += generateSearchQuery(multi, filter);
                    addCommandValues(db, filter);
                    multi = true;
                }
            }

            if (Request.QueryString["shop"] != null)
            {
                query += generateIDQuery(multi, "shop", "ShopID");
                addIDCommandValues(db, "shop", "ShopID");
                multi = true;
            }
            if (Request.QueryString["category"] != null)
            {
                query += generateIDQuery(multi, "category", "Category");
                addIDCommandValues(db, "category", "Category");
                multi = true;
            }

            db.ChangeQuery(query);

            HashSet<string> shopsSet = new HashSet<string> { };
            HashSet<string> sizesSet = new HashSet<string> { };
            HashSet<string> brandsSet = new HashSet<string> { };
            HashSet<string> coloursSet = new HashSet<string> { };
            HashSet<string> categoriesSet = new HashSet<string> { };
            HashSet<string> typesSet = new HashSet<string> { };

            while (db.HasNext())
            {
                bool check = true;
                if (price)
                {
                    string[] priceRange = Request.QueryString["price"].Split(',');
                    double lower = Convert.ToDouble(priceRange[0]);
                    double higher = Convert.ToDouble(priceRange[1]);
                    double money = Convert.ToDouble(db.Get("Price"));
                    if (money < lower || money > higher)
                    {
                        check = false;
                    }
                }
                if (search && check)
                {
                    string name = db.Get("Name");
                    string searchTerm = Request.QueryString["search"];
                    if (!name.ToLower().Contains(searchTerm.ToLower()) && !searchTerm.ToLower().Contains(name.ToLower()))
                    {
                        check = false;
                    }
                }
                if (check)
                {
                    ids.Add(Convert.ToInt32(db.Get("ProductID")));
                    images.Add(db.Get("URL"));
                    names.Add(db.Get("Name"));
                    prices.Add(Convert.ToDouble(db.Get("Price").ToString().Substring(0, db.Get("Price").ToString().Length - 2)));
                    sizesSet.Add(db.Get("Size"));
                    brandsSet.Add(db.Get("Brand"));
                    coloursSet.Add(db.Get("Colour"));
                    typesSet.Add(db.Get("Type"));
                    categoriesSet.Add(Convert.ToInt32(db.Get("Category")).ToString());
                    shopsSet.Add(Convert.ToInt32(db.Get("ShopID")).ToString());
                }
            }

            categoriesSet = convertToNames(categoriesSet, "Categories", "CategoryID");
            shopsSet = convertToNames(shopsSet, "Shops", "ShopID");

            shopListener = new FilterCheckBoxManager();
            addFilterRows(ShopList, shopListener, shopsSet, "Shop");

            categoryListener = new FilterCheckBoxManager();
            addFilterRows(CategoryList, categoryListener, categoriesSet, "Category");

            brandListener = new FilterCheckBoxManager();
            addFilterRows(BrandList, brandListener, brandsSet, "Brand");

            sizeListener = new FilterCheckBoxManager();
            addFilterRows(SizeList, sizeListener, sizesSet, "Size");

            colourListener = new FilterCheckBoxManager();
            addFilterRows(ColourList, colourListener, coloursSet, "Colour");

            typeListener = new FilterCheckBoxManager();
            addFilterRows(TypeList, typeListener, typesSet, "Type");

            db.Close();

            int columns = 3;

            TableRow row = new TableRow();
            for (int i = 0; i < names.Count; i++)
            {
                if (i % columns == 0 && i != 0)
                {
                    Display.Rows.Add(row);
                    row = new TableRow();
                }
                TableCell cell = new TableCell();
                cell.CssClass = "cell";
                
                ItemDisplayImageButton image = new ItemDisplayImageButton((int)ids[i], displayItem);
                image.ImageUrl = (string)images[i];
                ItemDisplayButton name = new ItemDisplayButton((int)ids[i], displayItem);

                cell.Controls.Add(image);
                cell.Controls.Add(name);

                name.Text = string.Format(names[i] + "\n£" + prices[i]);
                name.BorderColor = System.Drawing.Color.Transparent;
                name.BackColor = System.Drawing.Color.Transparent;
                name.CssClass = "nameText";
                row.Cells.Add(cell);
            }
            if (row.Cells.Count != 0)
            {
                Display.Rows.Add(row);
            }
        }

        protected void goBack(object sender, EventArgs e)
        {
           Response.Redirect("MainScreen.aspx");
        }
    }
}