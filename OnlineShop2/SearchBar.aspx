<script src="https://kit.fontawesome.com/f716c05871.js" crossorigin="anonymous"></script>
<link rel="stylesheet" href="SearchBar.css"/>
<div>
    <ul style="align-content: center;">
        <li style="padding-left: 40px; padding-right:30px"><i class="fa-solid fa-shop fa-3x"></i></li>
        <li><a href="MainScreen.aspx">Home</a></li>
        <li><a href="ItemsDisplay.aspx">Items</a></li>
        <li><a href="Shops.aspx">Shops</a></li>
        <%  if (Session["UserId"] != null)
        {%>
            <% if (getManager())
                {%>
                <li><asp:Button ID="AddProduct" class="menuItem" Text="Add Product" OnClick="goToAddProduct" runat="server"/></li>
                <li><asp:Button ID="Orders" class="menuItem" Text="Orders" OnClick="goToOrders" runat="server"/></li>
            <% } %>
            <% if (getOwner())
                                                                                        {%>
                    <li><asp:Button ID="Managers" class="menuItem" Text="Managers" OnClick="goToManagers" runat="server"/></li>
            <% } %>
            <li style="float:right"><button onserverclick="logout" class="CustomButton" runat="server">Logout</button></li>
        <% } %>
         <% else { %>
             <li style="float:right"><button onserverclick="login" class="CustomButton" runat="server">Login</button></li>
         <% } %>

        <li style="float:right; padding-right: 20px"><button onserverclick="goToBasket" class="CustomButton" runat="server"><i class="fa-solid fa-basket-shopping fa-2x"></i></button></li>
        <li style="float:right;"><button onserverclick="goToWishList" class="CustomButton" runat="server"><i class="fa-solid fa-heart fa-2x"></i></button></li>
        <li style="float:right;"><button onserverclick="goToAccount" class="CustomButton" runat="server"><i class="fa-solid fa-user fa-2x"></i></button></li>
        <li style="float:right; padding-right: 20px">
            <asp:Panel runat="server" DefaultButton="searchButton">
                <asp:TextBox class="input_field" placeholder="Search Catalogue..." ID="NavBarSearch" runat="server" ></asp:TextBox>
                <asp:Button style="border-color:transparent; background-color:transparent" id="searchButton" OnClick="search" runat="server"/>
            </asp:Panel>
        </li>
    </ul>
</div>