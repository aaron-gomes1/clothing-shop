<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Manager.cs" Inherits="OnlineShop2.Manager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manager</title>
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

        .managerform {
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
    <form class="managerform" id="ManagerPage" runat="server">
         <!--#include file ="SearchBar.aspx"-->
            <table class="table">
                <tr>
                    <td colspan="2"  style="text-align: center"><h1 class="textLabel">Manager Details</h1></td>
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
                    <td class="spacing" >Username</td>
                    <td>
                        <asp:Label ID="ManagerUserNameLabel" class="spacing" runat="server">&nbsp;</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="spacing" >Name</td>
                    <td>
                        <asp:Label ID="ManagerNameLabel" class="spacing" runat="server">&nbsp;</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Email</td>
                    <td>
                        <asp:Label ID="ManagerEmailLabel" class="spacing" runat="server">&nbsp;</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Address</td>
                    <td>
                        <asp:Label ID="ManagerAddressLabel" class="spacing" runat="server">&nbsp;</asp:Label>
                    </td>
                </tr>
                <tr class="spacing">
                    <td class="spacing">Post Code</td>
                    <td>
                        <asp:Label ID="ManagerPostCodeLabel" class="spacing" runat="server">&nbsp;</asp:Label>
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
    </form>
</body>
</html>


