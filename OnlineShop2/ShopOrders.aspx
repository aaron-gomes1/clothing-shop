<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopOrders.cs" Inherits="OnlineShop2.ShopOrders" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Shop Orders</title>
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

        .nameText {
            font-size: larger;
            font-family: Arial;
            text-align: center;
            cursor: pointer;
        }

        .orderLabel {
            text-align: center;
            color: white;
            font-weight: bold;
            font-family: 'Arial Rounded MT';
            font-size: 20px;
        }

        .shopordersform {
            vertical-align: middle;
            align-content: center;
            width:100%;
            display: block;
        }

        .table {
            vertical-align: middle;
            align-content: center;
            padding-left: 40%;
            padding-top: 5%;
            display: block;
        }
    </style>
</head>
<body>
    <form class="shopordersform" id="ShopOrdersPage" runat="server">
         <!--#include file ="SearchBar.aspx"-->
            <table class="table">
                <tr>
                    <td colspan="2"  style="text-align: center"><h1 class="textLabel">Shop Orders</h1></td>
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
                    <td colspan="2"  style="text-align: center"><h3 class="orderLabel">Active Orders</h3></td>
                </tr>
                <tr>
                    <td>
                        <asp:Table ID="ActiveShopOrdersTable" class="spacing" CellSpacing="10" runat="server"></asp:Table>
                    </td>
                </tr>
		        <tr>
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2"  style="text-align: center"><h3 class="orderLabel">Fufilled Orders</h3></td>
                </tr>
                <tr>
                    <td>
                        <asp:Table ID="FufilledShopOrdersTable" class="spacing" CellSpacing="10" runat="server"></asp:Table>
                    </td>
                </tr>
		        <tr>
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2"  style="text-align: center"><h3 class="orderLabel">Delivered Orders</h3></td>
                </tr>
                <tr>
                    <td>
                        <asp:Table ID="DeliveredShopOrdersTable" class="spacing" runat="server"></asp:Table>
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
                        <asp:Button ID="ShopOrdersBackBtn" runat="server" class="CustomButton" OnClick="goBack" Text="Back" Width="100px" />
                    </td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <table style="align-content:center" class="table">
                <tr>
                    <td>
                        <asp:Table ID="ShopProductsTable" class="table" CellSpacing="30" runat="server"></asp:Table>
                    </td>
                </tr>
            </table>
        
    </form>
</body>
</html>


