using BillPaymentProject.classobjects;
using BillPaymentProject.objectControls;
using System.ComponentModel.DataAnnotations;
using System.Web.Services;

namespace BillPaymentApi
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class BillPaymentApiEndPoint : WebService
    {
        private readonly BillPaymentBusinessLogic _logic;

        public BillPaymentApiEndPoint()
        {
            _logic = new BillPaymentBusinessLogic();
        }

        [WebMethod]
        public ApiResponse<int> CreateUser(string  ReferenceNumber, string CustomerName, string phoneNumer, string UtilityCodse, int CreatedBy)
        {
            BillPaymnet createUser = new BillPaymnet
            {
                ReferenceNumber = ReferenceNumber,
                CustomerName = CustomerName,
                ContactPhone = phoneNumer,
                UtilityCode = UtilityCodse,
                CreatedBy = CreatedBy


            };




            return ApiOperationHandler.Handle(() => _logic.CreateUser(createUser), "User created successfully.");
        }

        [WebMethod]
        public ApiResponse<int> CreateCustomer(string ReferenceNumber, string CustomerName,string email, string phoneNumer, string password, string UtilityCodse, int CreatedBy)
        {

            BillPaymnet createCustomer = new BillPaymnet 
            {
                ReferenceNumber = ReferenceNumber,
                CustomerName = CustomerName,
                Email = email,
                PhoneNumber = phoneNumer,
                PasswordHash = password,
                UtilityCode = UtilityCodse,
                CreatedBy = CreatedBy


            };
            return ApiOperationHandler.Handle(() => _logic.CreateCustomer(createCustomer), "Customer created successfully.");
        }

        [WebMethod]
        public ApiResponse<int> CreateUtility(string UtilityName, string UtilityCode, int CreatedBy)
        {

            BillPaymnet utility = new BillPaymnet {
            UtilityCode = UtilityCode,
            UtilityName = UtilityName,
            CreatedBy = CreatedBy
            };
            
            return ApiOperationHandler.Handle(() => _logic.CreateUtility(utility), "Utility created successfully.");
        }

        [WebMethod]
        public ApiResponse<int> CreateVendor(string vendorCode, string vendorName, string conatactEmail, string conatatPhone, string password, decimal balance, int createdBy)
        {

            BillPaymnet vendor = new BillPaymnet {
                VendorCode = vendorCode,
                VendorName = vendorName,
                ContactEmail = conatactEmail,
                ContactPhone = conatatPhone,
                PasswordHash = password,
                Balance = balance,
                CreatedBy = createdBy
            };

         
            return ApiOperationHandler.Handle(() => _logic.CreateVendor(vendor), "Vendor created successfully.");
        }

        [WebMethod]
        public ApiResponse<int> LoginUser(string username, string password) {

            BillPaymnet loginUser = new BillPaymnet {
            Username = username,
            PasswordHash = password
            };

            return ApiOperationHandler.Handle(()=> _logic.LoginUsers(loginUser), "Success Login");
        }
    }
}
