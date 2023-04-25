<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shops.cs" Inherits="OnlineShop2.Shops" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Shops</title>
    <link rel="stylesheet" href="style.css"/>
    <style type="text/css">
        .spacing {
            width: 100px;
            padding-bottom: 10px;
        }

        .textLabel {
            text-align: center;
            color: white;
            font-weight: bold;
            font-family: 'Arial Rounded MT';
        }

        .shopsform {
            vertical-align: middle;
            align-content: center;
            width:100%;
            display: block;
        }

        .table {
            vertical-align: middle;
            align-content: center;
            padding-left: 37%;
            padding-top: 5%;
            display: block;
        }

        td {
            padding-top: 10px;
        }

        tr {
            align-content: center;
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
    <form class="shopsform" id="ShopPage" runat="server">
         <!--#include file ="SearchBar.aspx"-->
         	<table class="table">
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td colspan="1"  style="text-align: center"><h1 class="textLabel">Shops</h1></td>
                    <td class="spacing">&nbsp;</td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td class="spacing">&nbsp;</td>
                    <td class="spacing">&nbsp;</td>
                </tr>
		        <tr>
                    <td colspan="2" style="align-content: center">
                        <asp:Table ID="ShopsTable" class="table" CellSpacing="10" runat="server"></asp:Table>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td class="spacing">&nbsp;</td>
                    <td class="spacing">&nbsp;</td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td colspan="1" style="text-align: center">
                        <asp:Button ID="ShopsBackBtn" runat="server" class="CustomButton" OnClick="goBack" Text="Back" Width="100px" />
                    </td>
                    <td class="spacing">&nbsp;</td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td class="spacing">&nbsp;</td>
                    <td class="spacing">&nbsp;</td>
                </tr>
            </table>
        
    </form>
</body>
</html>


