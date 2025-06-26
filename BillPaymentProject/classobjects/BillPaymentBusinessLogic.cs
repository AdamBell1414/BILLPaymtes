using BillPaymentProject.objectControls;
using System;
using System.Collections.Generic;
using System.Data;

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

        public string InitiateVendorPaymentTransaction(BillPaymnet transaction)
        {
            if (string.IsNullOrWhiteSpace(transaction.VendorCode) || string.IsNullOrWhiteSpace(transaction.ReferenceNumber))
                throw new ArgumentException("Vendor code and reference are required.");

            if (transaction.Amount <= 0)
                throw new ArgumentException("Amount must be greater than 0.");

            return _databaseHandler.InitiateVendorPaymentTransaction(transaction);
        }
        public string ValidateUtilityReference(BillPaymnet referenceInfo)
        {
            if (string.IsNullOrWhiteSpace(referenceInfo.VendorCode))
                throw new ArgumentException("Vendor Code is required.");

            if (string.IsNullOrWhiteSpace(referenceInfo.ReferenceNumber))
                throw new ArgumentException("Reference Number is required.");

            var result = _databaseHandler.ValidateUtilityReference(referenceInfo.VendorCode, referenceInfo.ReferenceNumber);

            if (string.IsNullOrEmpty(result))
                throw new KeyNotFoundException("Customer not found for the given reference.");

            return result;
        }


        public  List<BillPaymnet> GetPendingTransactions()
        {
            

            
            DataTable dt = _databaseHandler.GetAllPendingTransaction();

            List<BillPaymnet> transactionList = new List<BillPaymnet>();

            
            foreach (DataRow dr in dt.Rows)
            {
                BillPaymnet transaction = new BillPaymnet
                {
                    TransactionID = Guid.Parse(dr["TransactionID"].ToString()), 
                    VendorID = Convert.ToInt32(dr["VendorID"]),
                    CustomerID = Convert.ToInt32(dr["CustomerID"]),
                    UtilityID = Convert.ToInt32(dr["UtilityID"]),
                    ReferenceNumber = dr["ReferenceNumber"].ToString(),
                    Amount = Convert.ToDecimal(dr["Amount"]),
                    UtilityToken = dr["UtilityToken"].ToString(),
                    UtilityReceiptNo = dr["UtilityReceiptNo"].ToString(),
                 
                };

                transactionList.Add(transaction);
            }

            return transactionList;
        }
        public ApiResponse<string> UpdateUtilityTransactionResult(BillPaymnet bill)
        {
            try
            {
                _databaseHandler.UpdateUtilityTransactionResult(
                    (Guid)bill.TransactionID,
                    bill.UtilityToken,
                    bill.UtilityReceiptNo,
                    bill.Status
                );

                return new ApiResponse<string> { Success = true, Data = bill.TransactionID.ToString() };
            }
            catch (Exception ex)
            {
                return new ApiResponse<string> { Success = false, Message = ex.Message };
            }
        }


        public void UpdateUtilityTransactionResult(Guid? transactionID, string utilityToken, string receiptNo, string status)
        {
            throw new NotImplementedException();
        }




    }
}
