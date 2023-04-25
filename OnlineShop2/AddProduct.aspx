<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddProduct.cs" Inherits="OnlineShop2.AddProduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="style.css"/>
    <style type="text/css">
        .spacing {
            width: 400px;
        }

        .errorLabel {
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

        .addproductform {
            vertical-align: middle;
            width:100%;
        }

        .table {
            vertical-align: middle;
            width: 80%;
            padding-left: 5%;
            padding-top: 3%;
        }

        .description {
            width: 500px;
            height: 100px;
        }
    </style>
</head>
<body>
    <form class="addproductform" id="addproductform" runat="server">
        <!--#include file ="SearchBar.aspx"-->
        <div>
            <table class="table">
                <tr>
                    <td colspan="2"  style="text-align: center"><h1 class="textLabel">Add Product</h1></td>
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
                    <td class="spacing">Name</td>
                    <td>
                        <asp:TextBox ID="NameTB" class="input_field" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="spacing">Description</td>
                    <td  colspan="2">
                        <asp:TextBox ID="DescriptionTB" class="description" TextMode="MultiLine" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Type</td>
                    <td>
                        <asp:DropDownList class="input_field" ID="TypeTB" runat="server">
                            <asp:ListItem Text= "Womens" Value= "Womens"></asp:ListItem>
                            <asp:ListItem Text= "Mens" Value= "Mens"></asp:ListItem>
                            <asp:ListItem Text= "Boys" Value= "Boys"></asp:ListItem>
                            <asp:ListItem Text= "Girls" Value= "Girls"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Category</td>
                    <td>
                        <asp:DropDownList class="input_field" ID="CategoryTB" runat="server" >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Brand</td>
                    <td>
                        <asp:TextBox ID="BrandTB" class="input_field" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Price</td>
                    <td>
                        <asp:TextBox ID="PriceTB" class="input_field" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Material</td>
                    <td>
                        <asp:TextBox ID="MaterialsTB" class="input_field" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Size</td>
                    <td>
                        <asp:TextBox ID="SizeTB" class="input_field" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Colour</td>
                    <td>
                        <asp:TextBox ID="ColourTB" class="input_field" runat="server"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td class="spacing">URL</td>
                    <td>
                        <asp:TextBox ID="URLTB" class="input_field" runat="server"></asp:TextBox>
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
                        <asp:Button ID="SaveBtn" runat="server" CssClass="CustomButton" OnClick="SaveProduct" Text="Save" Width="80px" />
                    </td>
                </tr>
                
            </table>
        </div>
    </form>
</body>
</html>
