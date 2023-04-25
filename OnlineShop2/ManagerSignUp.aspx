<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagerSignUp.cs" Inherits="OnlineShop2.ManagerSignUpForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manager Sign Up</title>
    <link rel="stylesheet" href="style.css"/>
    <style type="text/css">
        .SignUpLabel {
            text-align: center;
            color: red;
            font-weight: bold;
        }

        .spacing {
            width: 250px;
        }

        .textLabel {
            text-align: center;
            color: white;
            font-weight: bold;
            font-family: 'Arial Rounded MT';
        }

        .managersignupform {
            vertical-align: middle;
            padding-left: 12%;
            padding-top: 5%;
        }

        .table {
            vertical-align: middle;
            width: 80%;
        }
    </style>
</head>
<body>
    <form class="managersignupform" id="managersignupform" runat="server">
        <div>
            <table class="table">
                <tr>
                    <td colspan="2"  style="text-align: center"><h1 class="textLabel">Manager Sign Up</h1></td>
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
                    <td class="spacing">Username</td>
                    <td>
                        <asp:TextBox ID="UsernameTB" class="input_field" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Name</td>
                    <td>
                        <asp:TextBox ID="NameTB" class="input_field" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Address</td>
                    <td>
                        <asp:TextBox ID="AddressTB" class="input_field" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Postcode</td>
                    <td>
                        <asp:TextBox ID="PostCodeTB" class="input_field" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Email Address</td>
                    <td>
                        <asp:TextBox ID="EmailAddressTB" class="input_field" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Password</td>
                    <td>
                        <asp:TextBox ID="PasswordTB" class="input_field" type="password" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Repeat Password</td>
                    <td>
                        <asp:TextBox ID="RPTPasswordTB" class="input_field" type="password" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr">
                    <td class="spacing"><asp:Label ID="ShopText" Text="Shop" runat="server"></asp:Label></td>
                    <td>
                        <asp:DropDownList class="input_field" Width="250px" ID="ShopTB" runat="server" >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="SignUpLabel" colspan="2">
                        <asp:Label ID="msgLabel" runat="server">&nbsp;</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Button ID="BackBtn" runat="server" CssClass="CustomButton" OnClick="Back" Text="Back" Width="80px" />
                    </td>
                    <td class="auto-style1">
                        <asp:Button ID="SaveBtn" runat="server" CssClass="CustomButton" OnClick="Save_User" Text="Save" Width="80px" />
                    </td>
                </tr>
                
            </table>
        </div>
    </form>
</body>
</html>
