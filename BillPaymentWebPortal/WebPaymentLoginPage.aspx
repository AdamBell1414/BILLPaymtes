<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebPaymentLoginPage.aspx.cs" Inherits="BillPaymentWebPortal.WebPaymentLoginPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login - Elgon Payment Portal</title>
    <style>
        body {
            margin: 0;
            padding: 0;
            font-family: Arial, sans-serif;
            background: url('/images/mountains.jpg') no-repeat center center fixed;
            background-size: cover;
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .login-container {
            background-color: rgba(255, 255, 255, 0.9);
            padding: 40px;
            border-radius: 10px;
            box-shadow: 0 10px 25px rgba(0, 0, 0, 0.2);
            width: 100%;
            max-width: 400px;
            backdrop-filter: blur(5px);
        }

        .login-title {
            text-align: center;
            font-size: 28px;
            font-weight: bold;
            margin-bottom: 25px;
            color: #333;
        }

        .form-group {
            margin-bottom: 20px;
        }

        .form-group label {
            display: block;
            font-weight: bold;
            margin-bottom: 5px;
            color: #333;
        }

        .form-group input {
            width: 100%;
            padding: 10px;
            font-size: 14px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .btn-submit {
            width: 100%;
            padding: 12px;
            background-color: #4facfe;
            border: none;
            color: white;
            font-weight: bold;
            font-size: 16px;
            border-radius: 5px;
            cursor: pointer;
            transition: 0.3s;
        }

        .btn-submit:hover {
            background-color: #00c6fb;
        }

        .message {
            margin-top: 15px;
            text-align: center;
            font-weight: bold;
            color: red;
        }

        .remember-me {
            display: flex;
            align-items: center;
            margin-top: 10px;
        }

        .remember-me input {
            margin-right: 5px;
        }

        .footer {
            text-align: center;
            margin-top: 20px;
            font-size: 12px;
            color: #666;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <div class="login-title">
                Welcome To Bill Paymets
            </div>

            <!-- Email/Username -->
            <div class="form-group">
                <label for="txtUsernameOrEmail">Email or Username</label>
                <asp:TextBox ID="txtUsernameOrEmail" runat="server" CssClass="form-control" placeholder="Enter Email or Username" />
            </div>

            <!-- Password -->
            <div class="form-group">
                <label for="txtPassword">Password</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Enter Password" />
            </div>

            <!-- Remember Me -->
            <div class="remember-me">
                <asp:CheckBox ID="chkRememberMe" runat="server" />
                <label for="chkRememberMe">Remember Me</label>
            </div>

            <!-- Submit Button -->
            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn-submit" OnClick="btnLogin_Click" />

            <!-- Message -->
            <asp:Label ID="lblMessage" runat="server" CssClass="message" />

            <!-- Optional Footer -->
            <div class="footer">
                &copy; 2025 Elgon Portal. All rights reserved.
            </div>
        </div>
    </form>
</body>
</html>
