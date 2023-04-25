<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayScreen.cs" Inherits="OnlineShop2.PayScreen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="style.css"/>
    <style type="text/css">
        .spacing {
            width: 200px;
        }

        .errorLabel {
            text-align: center;
            color: red;
            font-weight: bold;
        }

        .textLabel {
            text-align: center;
            font-size: larger;
            color: white;
            font-weight: bold;
            font-family: 'Arial Rounded MT';
        }

        .payform {
            vertical-align: middle;
            width:100%;
        }

        .table {
            vertical-align: middle;
            width: 70%;
            padding-left:35%;
            padding-top: 3%;
        }
    </style>
</head>
<body>
    <form class="payform" id="payform" runat="server">
        <!--#include file ="SearchBar.aspx"-->
        <div>
            <table class="table">
                <tr>
                    <td colspan="2"  style="text-align: center"><h1 class="textLabel">Enter Card Details</h1></td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" style="align-content:center; text-align: center;">
                        <asp:Label ID="PriceLabel" runat ="server"></asp:Label>
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
                    <td class="spacing">Name on Card</td>
                    <td>
                        <asp:TextBox ID="NameTB" class="input_field" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="spacing">Card Number</td>
                    <td  colspan="2">
                        <asp:TextBox ID="CardNumTB" class="input_field" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Expiry Date</td>
                    <td>
                        <asp:TextBox ID="ExpiryTB" class="input_field" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">CVV</td>
                    <td>
                        <asp:TextBox ID="CVVTB" class="input_field" runat="server"></asp:TextBox>
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
                    <td class="errorLabel" colspan="2">
                        <asp:Label ID="msgLabel" runat="server">&nbsp;</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="spacing">
                        <asp:Button ID="BackBtn" runat="server" CssClass="CustomButton" OnClick="Back" Text="Back" Width="80px" />
                    </td>
                    <td class="spacing">
                        <asp:Button ID="PayBtn" runat="server" CssClass="CustomButton" OnClick="Pay" Text="Pay" Width="80px" />
                    </td>
                </tr>
                
            </table>
        </div>
    </form>
</body>
</html>
