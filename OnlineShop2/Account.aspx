<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Account.cs" Inherits="OnlineShop2.Account" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Account Details</title>
    <link rel="stylesheet" href="style.css"/>
    <style type="text/css">
        .spacing {
            width: 250px;
            padding-bottom: 10px;
        }

        .textLabel {
            text-align: center;
            color: white;
            font-weight: bold;
            font-family: 'Arial Rounded MT';
        }

        .accountform {
            vertical-align: middle;
            width:100%;
            
        }

        .table {
            vertical-align: middle;
            width: 80%;
            padding-left: 20%;
            padding-top: 6%;
        }

    </style>
</head>
<body>
    <form class="accountform" id="AccountPage" runat="server">
         <!--#include file ="SearchBar.aspx"-->
        <div>
            <table class="table">
                <tr>
                    <td colspan="2"  style="text-align: center"><h1 class="textLabel">Account Details</h1></td>
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
                <tr>
                    <td class="spacing" >Username</td>
                    <td>
                        <asp:Label ID="AccountUserNameLabel" class="spacing" runat="server">&nbsp;</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="spacing" >Name</td>
                    <td>
                        <asp:Label ID="AccountNameLabel" class="spacing" runat="server">&nbsp;</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Email</td>
                    <td>
                        <asp:Label ID="AccountEmailLabel" class="spacing" runat="server">&nbsp;</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Address</td>
                    <td>
                        <asp:Label ID="AccountAddressLabel" class="spacing" runat="server">&nbsp;</asp:Label>
                    </td>
                </tr>
                <tr class="spacing">
                    <td class="spacing">Post Code</td>
                    <td>
                        <asp:Label ID="AccountPostCodeLabel" class="spacing" runat="server">&nbsp;</asp:Label>
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
                    <td style="text-align: center">
                        <asp:Button ID="EditAccountDetailsBtn" class="CustomButton" runat="server" OnClick="goToEditAccountDetails" Text="Edit Details" Width="100px" />
                        
                    </td>
                    <td>
                        <asp:Button ID="MyOrdersBtn" runat="server" class="CustomButton" OnClick="goToMyOrders" Text="My Orders" Width="100px" />
                    </td>
                    <td>
                    <asp:Button ID="AccountBackBtn" runat="server" class="CustomButton" OnClick="goBack" Text="Back" Width="100px" />
                    </td>
                </tr>
                
            </table>
        </div>
    </form>
</body>
</html>


