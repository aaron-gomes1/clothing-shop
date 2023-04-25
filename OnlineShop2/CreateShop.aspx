<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateShop.cs" Inherits="OnlineShop2.CreateShopForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Shop</title>
    <link rel="stylesheet" href="style.css"/>
    <style type="text/css">
        .CreateShopLabel {
            text-align: center;
            color: red;
            font-weight: bold;
        }
        .center {
            text-align: center;
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

        .createshopform {
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
    <form class="createshopform" id="form2" runat="server">
        <div>
            <table class="table">
                <tr>
                    <td colspan="2"  style="text-align: center"><h1 class="textLabel">Create Shop</h1></td>
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
                    <td class="spacing">Shop Name</td>
                    <td>
                        <asp:TextBox ID="NameTB" class="input_field" runat="server"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td class="spacing">Description</td>
                    <td>
                        <asp:TextBox ID="DescriptionTB" class="input_field" runat="server"></asp:TextBox>
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
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="CreateShopLabel" colspan="2">
                        <asp:Label ID="msgLabel" runat="server">&nbsp;</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="center">
                        <asp:Button ID="BackBtn" runat="server" CssClass="CustomButton" OnClick="Back" Text="Back" Width="80px" />
                    </td>
                    <td class="center">
                        <asp:Button ID="SaveBtn" runat="server" CssClass="CustomButton" OnClick="SaveShop" Text="Save" Width="80px" />
                    </td>
                </tr>
                
            </table>
        </div>
    </form>
</body>
</html>
