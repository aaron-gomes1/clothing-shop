<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyOrders.cs" Inherits="OnlineShop2.MyOrders" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="style.css"/>
    <title>My Orders</title>
    <style>
        .itemtitle {
            display: block;
            font-size: xx-large;
            text-align: center;
            color: white;
            font-weight: bold;
            font-family: 'Arial Rounded MT';
            padding-top: 40px;
            padding-bottom: 40px;
            align-content: center;
        }

        .table {
            vertical-align: middle;
            width: 50%;
            font-size: large;
            text-align:center;
            padding-left: 33%;
            display: block;
        }

        .myorderform {
            vertical-align: middle;
            align-content: center;
            width:100%;
            display: block;
        }

        .nameText {
            font-size: larger;
            font-family: Arial;
            text-align: center;
            cursor: pointer;
        }

        td {
            padding-top:10px;
            padding-left:10px;
            padding-right:10px;
        }
    </style>
</head>
<body>
    <form id="MyOrdersForm" class="myordersform" runat="server">
        <!--#include file ="SearchBar.aspx"-->
        <asp:Label CssClass="itemtitle" id="ItemTitle" Text="My Orders" runat="server"></asp:Label>
        <div style="padding-left: 47%">
            <asp:Button ID="BackBtn" runat="server" style="align-content:center; vertical-align: central" CssClass="CustomButton" OnClick="goBack" Text="Back" Width="80px" />
        </div>
        <asp:Table ID="MyOrdersTable" class="table" CellPadding="10" CellSpacing="10" runat ="server">
        </asp:Table>
    </form>
</body>
</html>
