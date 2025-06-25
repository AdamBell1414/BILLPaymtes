using BillPaymentProject.objectControls;
using System;

namespace BillPaymentProject.classobjects
{
    public class BillPaymentBusinessLogic
    {
        private readonly DatabaseHandler _databaseHandler;

        public BillPaymentBusinessLogic()
        {
            _databaseHandler = new DatabaseHandler();
        }

        public int CreateUser(BillPaymnet createUser)
        {
            if (string.IsNullOrWhiteSpace(createUser.Username) || string.IsNullOrWhiteSpace(createUser.PasswordHash))
            {
                throw new ArgumentException("Username and Password are required.");
            }

            return _databaseHandler.CreateUser(createUser);
        }

        public int CreateUtility(BillPaymnet utility)
        {
            if (string.IsNullOrWhiteSpace(utility.UtilityName) || string.IsNullOrWhiteSpace(utility.UtilityCode))
            {
                throw new ArgumentException("Utility Name and Code are required.");
            }

            return _databaseHandler.CreateUtyility(utility);
        }

        public int CreateCustomer(BillPaymnet customer)
        {
            if (string.IsNullOrWhiteSpace(customer.CustomerName) || string.IsNullOrWhiteSpace(customer.PhoneNumber))
            {
                throw new ArgumentException("Customer name and phone number are required.");
            }

            return _databaseHandler.CreateCustomer(customer);
        }

        public int CreateVendor(BillPaymnet vendor)
        {
            if (string.IsNullOrWhiteSpace(vendor.VendorCode) || string.IsNullOrWhiteSpace(vendor.VendorName))
            {
                throw new ArgumentException("Vendor Code and Name are required.");
            }

            return _databaseHandler.CreateVendor(vendor);
        }

        public int LoginUsers(BillPaymnet users)
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(users.Email) && string.IsNullOrWhiteSpace(users.Username))
            {
                throw new ArgumentException("Username or Email is required.");
            }

            if (string.IsNullOrWhiteSpace(users.PasswordHash))
            {
                throw new ArgumentException("Password is required.");
            }

            // Call DB handler to perform login
            int loginResult = _databaseHandler.LoginUser(users);


            // Handle the result
            switch (loginResult)
            {
                case -1:
                    throw new UnauthorizedAccessException("Invalid username/email or password.");
                case -2:
                    throw new UnauthorizedAccessException("Your account is locked due to multiple failed login attempts.");
                case -99:
                    throw new Exception("An internal error occurred while attempting login.");
                default:
                    
                    return loginResult; // This is the RoleID
            }
        }

    }
}
