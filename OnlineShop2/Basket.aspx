<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Basket.cs" Inherits="OnlineShop2.Basket" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="style.css"/>
    <title>Basket</title>
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

        .basketform {
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

        .textLabel {
            font-size: larger;
            font-family: Arial;
            text-align: center;
            color:white;
            padding-top:20px;
            align-content:center;
            vertical-align: central
        }

        td {
            padding-top:10px;
            padding-left:10px;
            padding-right:10px;
        }
    </style>
</head>
<body>
    <form id="BasketForm" class="basketform" runat="server">
        <!--#include file ="SearchBar.aspx"-->
        <asp:Label CssClass="itemtitle" ID="ItemTitle" Text="Basket" runat="server"></asp:Label>
        <div style="padding-left: 47%">
            <asp:Button ID="BackBtn" runat="server" style="align-content:center; vertical-align: central" CssClass="CustomButton" OnClick="goBack" Text="Back" Width="80px" />
        </div>
        <div style="padding-left: 47%">
            <asp:Label ID="PriceLabel" CssClass="textLabel" runat ="server"></asp:Label>
        </div>
        <asp:Table ID="BasketTable" class="table" CellPadding="10" OnClick="goBack" CellSpacing="10" runat ="server">
        </asp:Table>
        <div style="padding-left: 47%">
            <asp:Button ID="PayButton" runat="server" style="align-content:center; vertical-align: central" CssClass="CustomButton" OnClick="Pay" Text="Pay" Width="80px" />
        </div>
    </form>
</body>
</html>
