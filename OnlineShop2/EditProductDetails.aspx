<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditProductDetails.cs" Inherits="OnlineShop2.EditProductDetailsForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit Product Details</title>
    <link rel="stylesheet" href="style.css"/>
    <style type="text/css">
        .EditProductLabel {
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
    <form class="editdetailsform" id="EditProductDetailsForm" runat="server">
         <!--#include file ="SearchBar.aspx"-->
        <div>
            <table class="table">
                <tr>
                    <td colspan="2"  style="text-align: center"><h1 class="textLabel">Edit Product Details</h1></td>
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
                    <td class="spacing">Edit Description</td>
                    <td>
                        <asp:TextBox class="input_field" ID="NewDescriptionTB" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Edit Type</td>
                    <td>
                        <asp:DropDownList class="input_field" ID="NewTypeTB" runat="server">
                            <asp:ListItem Text= "Womens" Value= "Womens"></asp:ListItem>
                            <asp:ListItem Text= "Mens" Value= "Mens"></asp:ListItem>
                            <asp:ListItem Text= "Boys" Value= "Boys"></asp:ListItem>
                            <asp:ListItem Text= "Girls" Value= "Girls"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Edit Category</td>
                    <td>
                        <asp:DropDownList class="input_field" ID="NewCategoryTB" runat="server" >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Edit Brand</td>
                    <td>
                        <asp:TextBox class="input_field" ID="NewBrandTB" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Edit Price</td>
                    <td>
                        <asp:TextBox class="input_field" ID="NewPriceTB" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Edit Materials</td>
                    <td>
                        <asp:TextBox class="input_field" ID="NewMaterialsTB" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Edit Size</td>
                    <td>
                        <asp:TextBox class="input_field" ID="NewSizeTB" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Change Colour</td>
                    <td class="spacing">
                        <asp:TextBox class="input_field" ID="NewColourTB" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Edit URL</td>
                    <td>
                        <asp:TextBox class="input_field" ID="NewURLTB" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="EditProductLabel" colspan="2">
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
                        <asp:Button ID="NewSaveBtn" runat="server" class="CustomButton" OnClick="SaveProduct" Text="Save" Width="80px" />
                    </td>
                </tr>
                
            </table>
        </div>
    </form>
</body>
</html>
