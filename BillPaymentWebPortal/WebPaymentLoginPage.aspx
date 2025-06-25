<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebPaymentLoginPage.aspx.cs" Inherits="BillPaymentWebPortal.WebPaymentLoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <style>
     /* General styles */
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
    color: white;
}

/* Welcome Title Styling */
.welcome-title {
    text-align: center;
    font-size: 36px;
    font-weight: bold;
    margin-bottom: 20px;
    color: black; /* Make text black to ensure visibility */
}

/* Login card styling */
.login-container {
    background-color: rgba(255, 255, 255, 0.7); /* Reduced opacity for better readability */
    padding: 40px;
    border-radius: 10px;
    box-shadow: 0 10px 25px rgba(0, 0, 0, 0.1);
    width: 100%;
    max-width: 600px;
    backdrop-filter: blur(5px); /* Optional blur effect */
}

/* Form Elements */
h2 {
    text-align: center;
    margin-bottom: 30px;
    color: #333;
}

.form-group {
    margin-bottom: 20px;
}

.form-group label {
    display: block;
    font-weight: bold;
    margin-bottom: 5px;
    color: #444;
}

.form-group input[type="email"],
.form-group input[type="password"],
.form-group select,
.form-group input[type="text"] {
    width: 100%;
    padding: 10px;
    font-size: 14px;
    border: 1px solid #ccc;
    border-radius: 5px;
    margin-top: 5px;
}

/* Phone Container styling */
.form-group .phone-container {
    display: flex;
    align-items: center;
    justify-content: space-between;
}

.form-group .country-code {
    width: 30%;
    margin-right: 5%;
}

.form-group .phone-number {
    width: 65%;
}

/* Submit Button Styling */
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

/* Message */
.message {
    margin-top: 15px;
    text-align: center;
    font-weight: bold;
    color: red;
}

/* Register Link */
.register-link {
    text-align: center;
    margin-top: 20px;
}

.register-link a {
    color: #4facfe;
    text-decoration: none;
    font-weight: bold;
}

.register-link a:hover {
    text-decoration: underline;
}

/* Responsive Styles */
@media screen and (max-width: 768px) {
    /* Increase font size of welcome title for small screens */
    .welcome-title {
        font-size: 28px;
    }

    /* Adjust the login-container to be slightly more opaque for better readability */
    .login-container {
        background-color: rgba(255, 255, 255, 0.8); /* More opaque for better visibility */
        padding: 20px;
        width: 90%;
    }

    /* Stack phone number and country code on smaller screens */
    .form-group .phone-container {
        flex-direction: column;
        align-items: stretch;
    }

    .form-group .country-code,
    .form-group .phone-number {
        width: 100%;
        margin-right: 0;
        margin-bottom: 10px;
    }

    /* Ensure text color is visible */
    .form-group label,
    .form-group input,
    .btn-submit,
    .welcome-title {
        color: #333; /* Dark color for better readability */
    }

    .btn-submit {
        font-size: 14px;
        padding: 10px;
    }
}

@media screen and (max-width: 480px) {
    /* Further increase font size for smaller screens */
    .welcome-title {
        font-size: 24px;
    }

    /* Reduce padding on small screens */
    .login-container {
        padding: 15px;
    }

    .btn-submit {
        font-size: 12px;
        padding: 8px;
    }
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
      

        <div class="login-container">
              <!-- Welcome Title -->
  <div class="welcome-title">
      Welcome to Elgon Payment Portal
  </div>

            <!-- KM Dropdown (instead of Race) -->
            <div class="form-group">
                <label><i class="fas fa-envelope"></i> Distance(Km)</label>
                <asp:DropDownList ID="ddlKM" runat="server" CssClass="form-control" required>
                    <asp:ListItem Text="Select KM" Value="" />
                    <asp:ListItem Text="2 km" Value="2" />
                    <asp:ListItem Text="5 km" Value="5" />
                    <asp:ListItem Text="10 km" Value="10" />
                    <asp:ListItem Text="25 km" Value="25" />
                    <asp:ListItem Text="50 km" Value="50" />
                    <asp:ListItem Text="100 km" Value="100" />
                </asp:DropDownList>
            </div>

            <!-- Name Field -->
            <div class="form-group">
                <label><i class="fas fa-lock"></i> Name</label>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" required />
            </div>

            <!-- Phone Number with Country Code -->
            <div class="form-group">
                <label><i class="fas fa-phone"></i> Phone Number</label>
                <div class="phone-container">
                    <asp:DropDownList ID="ddlCountryCode" runat="server" CssClass="country-code" required>
                        <asp:ListItem Text="Select Country Code" Value="" />
                        <asp:ListItem Text="+256 (Uganda)" Value="+256" />
                        <asp:ListItem Text="+1 (USA)" Value="+1" />
                        <asp:ListItem Text="+44 (UK)" Value="+44" />
                    </asp:DropDownList>
                    <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="phone-number" required />
                </div>
            </div>

            <!-- Login Button -->
            <asp:Button ID="btnLogin" runat="server" Text="PayNow" CssClass="btn-submit" OnClick="btnLogin_Click" />

            <!-- Message Label -->
            <asp:Label ID="lblMessage" runat="server" CssClass="message" />
        </div>
    </form>
</body>
</html>
