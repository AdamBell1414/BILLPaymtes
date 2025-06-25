<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="BillPaymentWebPortal.AdminDashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Dashboard</title>
    <style>
        * {
            box-sizing: border-box;
        }

        body, html {
            margin: 0;
            padding: 0;
            height: 100%;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f1f4f7;
        }

        .layout {
            display: flex;
            height: 100vh;
            overflow: hidden;
        }

        /* Sidebar */
        .sidebar {
            width: 240px;
            background-color: #2c3e50;
            color: white;
            padding-top: 30px;
            flex-shrink: 0;
            display: flex;
            flex-direction: column;
        }

        .sidebar h2 {
            text-align: center;
            margin-bottom: 30px;
            font-size: 20px;
        }

        .sidebar a {
            display: block;
            color: white;
            padding: 12px 20px;
            text-decoration: none;
            font-size: 16px;
            transition: background 0.3s;
        }

        .sidebar a:hover {
            background-color: #34495e;
        }

        /* Main Content */
        .main {
            flex-grow: 1;
            padding: 30px;
            overflow-y: auto;
        }

        .section {
            background-color: white;
            padding: 25px;
            margin-bottom: 30px;
            border-radius: 10px;
            box-shadow: 0 3px 10px rgba(0, 0, 0, 0.05);
        }

        h1 {
            color: #333;
            margin-top: 0;
        }

        .section h2 {
            color: #4facfe;
            margin-bottom: 20px;
        }

        .form-group {
            margin-bottom: 15px;
        }

        label {
            display: block;
            font-weight: bold;
            margin-bottom: 5px;
        }

        input[type="text"],
        input[type="email"],
        input[type="password"] {
            width: 100%;
            padding: 8px 10px;
            border-radius: 5px;
            border: 1px solid #ccc;
        }

        .btn {
            padding: 10px 20px;
            background-color: #4facfe;
            color: white;
            font-weight: bold;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

        .btn:hover {
            background-color: #00c6fb;
        }

        .search-bar {
            margin-bottom: 10px;
        }

        .search-bar input {
            padding: 8px;
            width: 100%;
            border-radius: 5px;
            border: 1px solid #ccc;
        }

        .grid-container {
            margin-top: 20px;
        }

        .grid-container table {
            width: 100%;
            border-collapse: collapse;
        }

        .grid-container th,
        .grid-container td {
            padding: 12px;
            border-bottom: 1px solid #ddd;
        }
    </style>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="d-flex vh-100">
        <!-- Sidebar -->
        <div class="bg-dark text-white p-3" style="width: 240px;">
            <h4 class="text-center mb-4">Admin Menu</h4>
            <ul class="nav flex-column">
                <li class="nav-item"><a class="nav-link text-white" href="#" data-bs-toggle="modal" data-bs-target="#vendorModal">Create Vendor</a></li>
                <li class="nav-item"><a class="nav-link text-white" href="#" data-bs-toggle="modal" data-bs-target="#userModal">Create User</a></li>
                <li class="nav-item"><a class="nav-link text-white" href="#" data-bs-toggle="modal" data-bs-target="#utilityModal">Create Utility</a></li>
                <li class="nav-item"><a class="nav-link text-white" href="#list">Vendor Table</a></li>
            </ul>
        </div>

        <!-- Main Content -->
        <div class="flex-grow-1 p-4 bg-light overflow-auto">
            <h2 class="mb-4">Welcome, Admin</h2>

            <!-- Dashboard Cards -->
            <div class="row g-3 mb-4">
                <div class="col-md-4">
                    <div class="card text-white bg-primary">
                        <div class="card-body">
                            <h5 class="card-title">Total Vendors</h5>
                            <p class="card-text">120</p>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card text-white bg-success">
                        <div class="card-body">
                            <h5 class="card-title">Total Users</h5>
                            <p class="card-text">45</p>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card text-white bg-warning">
                        <div class="card-body">
                            <h5 class="card-title">Utilities</h5>
                            <p class="card-text">30</p>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Vendor Table -->
            <div id="list" class="card p-3">
                <h4 class="mb-3">Vendor Table (with Search)</h4>

                <asp:TextBox ID="txtSearchVendor" runat="server" CssClass="form-control mb-3" AutoPostBack="true" OnTextChanged="txtSearchVendor_TextChanged" Placeholder="Search Vendor..." />

                <asp:GridView ID="gvVendors" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                    <Columns>
                        <asp:BoundField DataField="VendorCode" HeaderText="Vendor Code" />
                        <asp:BoundField DataField="VendorName" HeaderText="Vendor Name" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

          vendor.VendorCode,
  vendor.VendorName,
  vendor.ContactEmail,
  vendor.ContactPhone,
  vendor.PasswordHash,
  vendor.Balance,
  vendor.CreatedBy

      <!-- Vendor Modal -->
    <div class="modal fade" id="vendorModal" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary text-white">
                    <h5 class="modal-title">Create Vendor</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">

                    <label>Vendor Code</label>
                        <asp:TextBox ID="txtVendorCode" runat="server" CssClass="form-control mb-2" />


                    <label>Vendor Name</label>

                    <asp:TextBox ID="txtVendorName" runat="server" CssClass="form-control mb-2" />

                    

                    <label>Contact Email</label>
                    <asp:TextBox ID="txtVendorEmail" runat="server" CssClass="form-control mb-2" TextMode="Email" />

                    <label>Contact Phone</label>
                    <asp:TextBox ID="txtVendorPhone" runat="server" CssClass="form-control mb-2" />

                    <label>Password</label>
                    <asp:TextBox ID="txtVendorPassword" runat="server" CssClass="form-control mb-2" TextMode="Password" />


                    <label>Initial Balance</label>
                    <asp:TextBox ID="txtVendorBalance" runat="server" CssClass="form-control mb-2" TextMode="Number" />
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnCreateVendor" runat="server" Text="Create Vendor" CssClass="btn btn-primary" OnClick="btnCreateVendor_Click" />
                </div>
            </div>
        </div>
    </div>

           <!-- Customer Model -->
        <div class="modal fade" id="userModal" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header bg-success text-white">
                        <h5 class="modal-title">Create User</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                    </div>
                    <div class="modal-body">
                        <label>Username</label>
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control mb-2" />

                        <label>Email</label>
                        <asp:TextBox ID="txtUserEmail" runat="server" TextMode="Email" CssClass="form-control mb-2" />

                        <label>Phone Number</label>
                        <asp:TextBox ID="txtUserPhone" runat="server" CssClass="form-control mb-2" />

                        <label>Password</label>
                        <asp:TextBox ID="txtUserPassword" runat="server" TextMode="Password" CssClass="form-control mb-2" />
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnCreateUser" runat="server" Text="Create User" CssClass="btn btn-success" OnClick="btnCreateUser_Click" />
                    </div>
                </div>
            </div>
        </div>


            <!-- Utility Modal -->
        <div class="modal fade" id="utilityModal" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header bg-warning text-dark">
                        <h5 class="modal-title">Create Utility</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                    </div>
                    <div class="modal-body">
                        <label>Utility Name</label>
                        <asp:TextBox ID="txtUtilityName" runat="server" CssClass="form-control mb-2" />

                        <label>Utility Code</label>
                        <asp:TextBox ID="txtUtilityCode" runat="server" CssClass="form-control mb-2" />
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnCreateUtility" runat="server" Text="Create Utility" CssClass="btn btn-warning text-dark" OnClick="btnCreateUtility_Click" />
                    </div>
                </div>
            </div>
        </div>

</form>

</body>
</html>
