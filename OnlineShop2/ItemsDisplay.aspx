<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemsDisplay.cs" Inherits="OnlineShop2.ItemsDisplay" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="style.css"/>
    <title>Item Display</title>
    <style>
        .price {
            width: 50px;
            height: 30px;
            padding:5px 5px 5px 5px;
        }

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
            width: 80%;
            font-size: large;
            text-align:center;
            padding-left: 8%;
            display: block;
        }

        .itemdisplayform {
            vertical-align: middle;
            align-content:center;
            width:100%;
            display: block;
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
        }

        .filtertable {
            display: block;
            align-content: center;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="ItemDisplayForm" class="itemdisplayform" runat="server">
        <!--#include file ="SearchBar.aspx"-->
        <asp:Label CssClass="itemtitle" id="ItemTitle" Text="All Products" runat="server"></asp:Label>
        <asp:Button ID="BackBtn" runat="server" CssClass="CustomButton" OnClick="goBack" Text="Back" Width="80px" />
        <table>
            <tr>
                <td style="vertical-align: top">
                    <asp:Table style="background-color: lightpink; padding: 5px 5px 5px 5px" CssClass="filtertable" ID="Filter" CellPadding="5" runat ="server">
                        <asp:TableRow>
                            <asp:TableCell style="text-align: center" ColumnSpan="2">
                                <asp:Label  Text="Price" runat ="server"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:TextBox CssClass="price" ID="MinPrice" runat ="server"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox CssClass="price" ID="MaxPrice" runat ="server"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Table ID="CategoryList" CellSpacing="8" CssClass="filtertable" runat="server">
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Table ID="ShopList" CellSpacing="8" CssClass="filtertable" runat="server">
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Table ID="BrandList" CellSpacing="8" CssClass="filtertable" runat="server">
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Table ID="TypeList" CellSpacing="8" CssClass="filtertable" runat="server">
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Table ID="SizeList" CellSpacing="8" CssClass="filtertable" runat="server">
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Table ID="ColourList" CellSpacing="8" CssClass="filtertable" runat="server">
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </td>
                <td>
                    <asp:Table ID="Display" class="table" CellPadding="10" CellSpacing="50" runat ="server">
                    </asp:Table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
