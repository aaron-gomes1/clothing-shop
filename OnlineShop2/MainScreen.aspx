<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainScreen.cs" Inherits="OnlineShop2.MainScreen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Shop to Love</title>
    <link rel="stylesheet" href="style.css"/>
</head>
<body>
    <form id="mainscreenform"  style="width:100%;display:block" runat="server">
        <!--#include file ="SearchBar.aspx"-->
        <asp:Image style="padding-top:20px; align-content:center; padding-left:5%" ImageURL="https://matalan-content.imgix.net/uploads/asset_file/asset_file/474003/1679914730.4938056-secondary.jpg?ixlib=rails-4.2.0&w=1319&ar=1600%3A504&fm=pjpg&auto=format%2Ccompress&q=30&cs=tinysrgb&s=2de230c77926f04bded6c1a433eecae2" runat="server" />
        <table style="padding-top:50px; padding-bottom:30px">
            <tr>
                <td>
                    <table style="width:500px">
                        <tr style="align-content:center; display:block; width:100%">
                            <td style="align-content:center; display:block; width:100%; padding-left:20%">
                                <asp:ImageButton ImageURL="https://matalan-content.imgix.net/uploads/asset_file/asset_file/469279/1678116945.8428173-S2952401_C438_Alt1.jpg?ixlib=rails-4.2.0&cs=tinysrgb&auto=compress%2Cformat&fm=pjpg&w=300&fit=crop&ar=300%3A0&s=a311876ebf02f05cb8128b5ff8400bfb" OnClick="goToDresses" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center">
                                Dresses
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table style="width:500px">
                        <tr style="align-content:center; display:block; width:100%">
                            <td style="align-content:center; display:block; width:100%; padding-left:20%">
                                <asp:ImageButton ImageURL="https://matalan-content.imgix.net/uploads/asset_file/asset_file/461986/1675422485.3409398-S2938144_C228_Alt1.jpg?ixlib=rails-4.2.0&cs=tinysrgb&auto=compress%2Cformat&fm=pjpg&w=300&fit=crop&ar=300%3A0&s=6baa5a397a10a8c81d3fce4e11feb9be" OnClick="goToFormal" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center">
                                Formal
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table style="width:500px">
                        <tr style="align-content:center; display:block; width:100%">
                            <td style="align-content:center; display:block; width:100%; padding-left:20%">
                                <asp:ImageButton ImageURL="https://matalan-content.imgix.net/uploads/asset_file/asset_file/450996/1669982812.9250512-S2940008_C101_Alt1.jpg?ixlib=rails-4.2.0&cs=tinysrgb&auto=compress%2Cformat&fm=pjpg&w=300&fit=crop&ar=300%3A0&s=8775fbf86fce491b8d75f346c13ce715%20300w" OnClick="goToShoes" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center">
                                Shoes
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </form>
    </body>
</html>