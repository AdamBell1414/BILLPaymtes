using System;
using System.Data;
using System.Web.UI.WebControls;

namespace BillPaymentWebPortal
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadVendors(); // Load vendors on initial load
            }
        }

        protected void btnCreateVendor_Click(object sender, EventArgs e)
        {
            string vendorCode = txtVendorCode.Text.Trim();
            string vendorName = txtVendorName.Text.Trim();
            string contactEmail = txtVendorEmail.Text.Trim();
            string contactPhone = txtVendorPhone.Text.Trim();
            decimal balance = string.IsNullOrEmpty(txtVendorBalance.Text) ? 0 : Convert.ToDecimal(txtVendorBalance.Text);
            string password = txtVendorPassword.Text.Trim();
            int createdBy = Convert.ToInt32(Session["UserID"]);

            // Call the API via HttpClient or WebClient
            using (var client = new System.Net.WebClient())
            {
                client.Headers[System.Net.HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

                var data = $"vendorCode={vendorCode}&vendorName={vendorName}&conatactEmail={contactEmail}&conatatPhone={contactPhone}&password={password}&balance={balance}&createdBy={createdBy}";

                string apiUrl = "https://localhost:44313/BillPaymentApiEndPoint.asmx?op=CreateVendor";

                try
                {
                    string response = client.UploadString(apiUrl, data); 
                    ClearVendorForm(); 
                }
                catch (Exception ex)
                {
                    // Log or show error
                }
            }
        }


        // Create User
        protected void btnCreateUser_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string email = txtUserEmail.Text.Trim();
            string password = txtUserPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return;

            //DatabaseHelper.CreateUser(username, email, password);
            txtUserName.Text = txtUserEmail.Text = txtUserPassword.Text = "";
        }

        // Create Utility
        protected void btnCreateUtility_Click(object sender, EventArgs e)
        {
            string utilityName = txtUtilityName.Text.Trim();
            if (string.IsNullOrEmpty(utilityName))
                return;

            //DatabaseHelper.CreateUtility(utilityName);
            txtUtilityName.Text = "";
        }

        // Search Vendors
        protected void txtSearchVendor_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearchVendor.Text.Trim();
            LoadVendors(keyword);
        }

        // Load Vendors (with optional search)
        private void LoadVendors(string keyword = "")
        {
            //DataTable dt = DatabaseHelper.GetVendors(keyword);
            //gvVendors.DataSource = dt;
            gvVendors.DataBind();
        }


        private void ClearVendorForm()
        {
            txtVendorCode.Text = "";
            txtVendorName.Text = "";
            txtVendorEmail.Text = "";
            txtVendorPhone.Text = "";
            txtVendorBalance.Text = "";
        }

    }
}
