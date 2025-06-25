using BillPaymentProject.classobjects;
using BillPaymentProject.objectControls;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BillPaymentWebPortal
{
    public partial class WebPaymentLoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var loginUser = new BillPaymnet
                {
                    Email = txtUsernameOrEmail.Text.Trim(),  // Could be email or username
                    PasswordHash = txtPassword.Text.Trim()
                };

                var loginService = new BillPaymentBusinessLogic();
                int result = loginService.LoginUsers(loginUser);

                switch (result)
                {
                    case 1:
                        Response.Redirect("~/AdminDashboard.aspx");
                        break;
                    case 2:
                        Response.Redirect("~/UserDashboard.aspx");
                        break;
                    case 3:
                        Response.Redirect("~/Vendor.aspx");
                        break;

                    case 4:
                        Response.Redirect("~/Customer.aspx");
                        break;
                    default:
                        lblMessage.Text = "Unknown role. Contact support.";
                        break;
                }
            }
            catch (ArgumentException ex)
            {
                lblMessage.Text = $"Input Error: {ex.Message}";
            }
            catch (UnauthorizedAccessException ex)
            {
                lblMessage.Text = $"Login Denied: {ex.Message}";
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"An error occurred: {ex.Message}";
            }
        }

    }
}
