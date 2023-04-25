<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemInfo.cs" Inherits="OnlineShop2.ItemInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Item Info</title>
    <link rel="stylesheet" href="style.css"/>
    <script src="https://kit.fontawesome.com/f716c05871.js" crossorigin="anonymous"></script>
    <style type="text/css">
        .spacing {
            width: 300px;
            padding-bottom: 10px;
        }

        .rating {
            background-color: transparent;
            border-width: 0px;
        }

        .textLabel {
            text-align: center;
            color: white;
            font-weight: bold;
            font-family: 'Arial Rounded MT';
        }

        .iteminfoform {
            vertical-align: middle;
            align-content: center;
            width:100%;
            display: block;
        }

        .table {
            vertical-align: middle;
            align-content: center;
            padding-left: 30%;
            padding-top: 5%;
            display: block;
        }

        td {
            padding-top: 10px;
        }

        tr {
            align-content: center;
        }

        .checked {
            color: orange; 
        }
    </style>
</head>
<body>
    <form class="iteminfoform" id="ItemInfoPage" runat="server">
         <!--#include file ="SearchBar.aspx"-->
            <table class="table">
                <tr>
                    <td colspan="2"  style="text-align: center"><h1 class="textLabel">Item Info</h1></td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr style="text-align: center">
                    <td colspan="2">
                        <asp:Image ID="ItemInfoImage" runat="server"></asp:Image>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="spacing" >Name</td>
                    <td>
                        <asp:Label ID="ItemInfoNameLabel" class="spacing" runat="server">&nbsp;</asp:Label>
                    </td>
                </tr>
                <tr class="spacing">
                    <td class="spacing">Price</td>
                    <td>
                        <asp:Label ID="ItemInfoPriceLabel" class="spacing" runat="server">&nbsp;</asp:Label>
                    </td>
                </tr>
                <tr class="spacing">
                    <td class="spacing">Description</td>
                    <td>
                        <asp:Label ID="ItemInfoDescriptionLabel" class="spacing" runat="server">&nbsp;</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Brand</td>
                    <td>
                        <asp:Label ID="ItemInfoBrandLabel" class="spacing" runat="server">&nbsp;</asp:Label>
                    </td>
                </tr>
                <tr class="spacing">
                    <td class="spacing">Type</td>
                    <td>
                        <asp:Label ID="ItemInfoTypeLabel" class="spacing" runat="server">&nbsp;</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Category</td>
                    <td>
                        <asp:Label ID="ItemInfoCategoryLabel" class="spacing" runat="server">&nbsp;</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Shop</td>
                    <td>
                        <asp:Label ID="ItemInfoShopLabel" class="spacing" runat="server">&nbsp;</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Materials</td>
                    <td>
                        <asp:Label ID="ItemInfoMaterialsLabel" class="spacing" runat="server">&nbsp;</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Size</td>
                    <td>
                        <asp:Label ID="ItemInfoSizeLabel" class="spacing" runat="server">&nbsp;</asp:Label>
                    </td>
                </tr>
                <tr class="spacing">
                    <td class="spacing">Colour</td>
                    <td>
                        <asp:Label ID="ItemInfoColourLabel" class="spacing" runat="server">&nbsp;</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="spacing">User Ratings</td>
                    <td class="spacing"><asp:Table ID="UserRatingTable" runat="server"></asp:Table></td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="spacing">Rate this Item</td>
                    <td class="spacing"><asp:Table ID="RatingTable" runat="server"></asp:Table></td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>      
                    <td>
                        <asp:Button ID="ItemInfoBackBtn" runat="server" class="CustomButton" OnClick="goBack" Text="Back" Width="100px" />
                    </td>
                    <td>
                        <asp:Button ID="WishListButton" runat="server" class="CustomButton" OnClick="favourite" Text="Add To Wish List" Width="200px" />
                    </td>
                    <td>
                        <asp:Button ID="BuyButton" runat="server" class="CustomButton" OnClick="buy" Text="Buy Item" Width="100px" />
                    </td>
                </tr>
                <% if (Session["UserId"] != null && getManager())
                    { %>
                    <tr>
                        <td class="spacing">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="spacing">&nbsp;</td>
                        <td>
                            <asp:Button ID="EditProduct" runat="server" class="CustomButton" OnClick="editProduct" Text="Edit Product" Width="100px" />
                        </td>
                    </tr>
                <% } %>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
    </form>
</body>
</html>


