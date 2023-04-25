<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.cs" Inherits="OnlineShop2.LoginForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Log In</title>
    <link rel="stylesheet" href="style.css"/>
    <style type="text/css">
        .loginLabel {
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
            width: 200px;
        }

        .loginform {
            vertical-align: middle;
            padding-left: 2%;
            padding-top: 8%;
        }

        .table {
            vertical-align: middle;
            width: 80%;
        }
    </style>
</head>
<body>
    <form id="form1" class="loginform" runat="server">
        <div>
            <table class="table">
                <tr>
                    <td colspan="2"  style="text-align: center"><h1 class="textLabel">Login</h1></td>
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

                        <asp:TextBox ID="LoginUserNameTB" class="input_field" Width="160px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">Password</td>
                    <td>
                        <asp:TextBox ID="LoginPasswordTB" class="input_field" Width="160px" type="password" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="loginLabel" colspan="2">
                        <asp:Label ID="loginLabel" class="btn btn-primary" runat="server">&nbsp;</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="spacing">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="LoginBtn" runat="server" class="CustomButton" Text="Log in" Width="150px" OnClick="Loginbtn_Click" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="SignUpBtn" runat="server" class="CustomButton" Text="Sign Up" Width="150px" OnClick="Signupbtn_Click" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="ManagerSignUpBtn" runat="server" class="CustomButton" Text="Manager Sign Up" Width="150px" OnClick="goToManagerSignUp" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

