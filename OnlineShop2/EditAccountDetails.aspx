<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditAccountDetails.cs" Inherits="OnlineShop2.EditAccountDetailsForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit Account Details</title>
    <link rel="stylesheet" href="style.css"/>
    <style type="text/css">
        .EditLabelLabel {
            text-align: center;
            color: red;
            font-weight: bold;
        }

        .textLabel {
            text-align: center;
            color: white;
            font-weight: bold;
            font-family: 'Arial Rounded MT';
        }

        .spacing {
            width: 250px;
        }

        .editdetailsform {
            vertical-align: middle;
            width:100%;
        }

        .table {
            vertical-align: middle;
            width: 70%;
            padding-left: 7%;
            padding-top: 5%;
        }
    </style>
</head>
<body>
    <form class="editdetailsform" id="EditAccountDetailsForm" runat="server">
         <!--#include file ="SearchBar.aspx"-->
        <div>
            <table class="table">
                <tr>
                    <td colspan="2"  style="text-align: center"><h1 class="textLabel">Edit Account Details</h1></td>
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
                    <td class="spacing">Edit Name</td>
                    <td>
                        <asp:TextBox class="input_field" ID="NewNameTB" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Edit Address</td>
                    <td>
                        <asp:TextBox class="input_field" ID="NewAddressTB" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Edit Postcode</td>
                    <td>
                        <asp:TextBox class="input_field" ID="NewPostCodeTB" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Edit Email Address</td>
                    <td>
                        <asp:TextBox class="input_field" ID="NewEmailAddressTB" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Change Password</td>
                    <td class="spacing">
                        <asp:TextBox class="input_field" ID="NewPasswordTB" type="password" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Repeat Password</td>
                    <td>
                        <asp:TextBox class="input_field" ID="NewRPTPasswordTB" type="password" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="EditAccountLabel" colspan="2">
                        <asp:Label ID="editMsgLabel" runat="server">&nbsp;</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="spacing">
                        <asp:Button ID="NewBackBtn" runat="server" class="CustomButton" OnClick="Back" Text="Back" Width="80px" />
                        
                    </td>
                    <td class="spacing">
                        <asp:Button ID="NewSaveBtn" runat="server" class="CustomButton" OnClick="Save_User" Text="Save" Width="80px" />
                    </td>
                </tr>
                
            </table>
        </div>
    </form>
</body>
</html>
