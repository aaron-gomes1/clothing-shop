<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shop.cs" Inherits="OnlineShop2.Shop" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Shop</title>
    <link rel="stylesheet" href="style.css"/>
    <style type="text/css">
        .spacing {
            width: 200px;
            padding-bottom: 10px;
        }

        .textLabel {
            text-align: center;
            color: white;
            font-weight: bold;
            font-family: 'Arial Rounded MT';
            font-size: 50px;
        }

        .shopform {
            vertical-align: middle;
            align-content: center;
            width:100%;
            display: block;
        }

        .table {
            vertical-align: middle;
            align-content: center;
            padding-left: 25%;
            padding-top: 5%;
            display: block;
        }

        td {
            padding-top: 10px;
        }

        tr {  
            font-size: larger;
            font-family: Arial;
            text-align: center;
            cursor: pointer;
        }

        .nameText {
            font-size: larger;
            font-family: Arial;
            text-align: center;
            cursor: pointer;
        }

        .cell {
            padding-top:10px;
            padding-left:10px;
            padding-right:10px;
        }

        .cell:hover {
            background-color: lightpink;
            cursor: pointer;
            align-content: center;
        }
    </style>
</head>
<body>
    <form class="shopform" id="ShopPage" runat="server">
         <!--#include file ="SearchBar.aspx"-->
            <table class="table">
                <tr>
                    <td colspan="2"  style="text-align: center">
                        <asp:Label ID="ShopNameLabel" class="textLabel" runat="server">&nbsp;</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr class="spacing">
                    <td class="spacing">Description</td>
                    <td>
                        <asp:Label ID="ShopDescriptionLabel" class="spacing" runat="server">&nbsp;</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Address</td>
                    <td>
                        <asp:Label ID="ShopAddressLabel" class="spacing" runat="server">&nbsp;</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Post Code</td>
                    <td>
                        <asp:Label ID="ShopPostCodeLabel" class="spacing" runat="server">&nbsp;</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Email</td>
                    <td>
                        <asp:Label ID="ShopEmailLabel" class="spacing" runat="server">&nbsp;</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>      
                    <td>
                        <asp:Button ID="ShopBackBtn" runat="server" class="CustomButton" OnClick="goBack" Text="Back" Width="100px" />
                    </td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <table style="align-content:center">
                <tr>
                    <td>
                        <asp:Table ID="ShopProductsTable" class="table" CellSpacing="30" runat="server"></asp:Table>
                    </td>
                </tr>
            </table>
        
    </form>
</body>
</html>


