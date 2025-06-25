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
            string name = txtName.Text.Trim();
            string phoneNumber = txtPhoneNumber.Text.Trim();
            string countryCode = ddlCountryCode.SelectedValue;
            string km = ddlKM.SelectedValue;


            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(countryCode) || string.IsNullOrEmpty(km))
            {
                lblMessage.Text = "Please fill out all fields.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            else
            {

                string message = $"Thank you {name}! You have selected {km} for the payment. " +
                                 $"We will contact you at {countryCode} {phoneNumber} shortly.";


                lblMessage.Text = message;
                lblMessage.ForeColor = System.Drawing.Color.Green;


                txtName.Text = "";
                txtPhoneNumber.Text = "";
                ddlCountryCode.SelectedIndex = 0;
                ddlKM.SelectedIndex = 0;
            }
        }
    }
}
