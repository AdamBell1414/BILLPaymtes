using BillPaymentProject.objectControls;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BillPaymentProject.classobjects
{
    public class DatabaseHandler
    {
        private DbCommand command;
        private Database db;
        public DatabaseHandler()
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            db = factory.Create("BillPayments");

        }

        public int CreateUser(BillPaymnet createuser)
        {
            try
            {
                command = db.GetStoredProcCommand("CreateUser", new object[]
                {

                    createuser.UserID,
                     createuser.Username,
                     createuser.PasswordHash,
                    createuser.Email,
                    createuser.PhoneNumber



                });
                DataSet ds = db.ExecuteDataSet(command);
                int newId = Convert.ToInt32(ds.Tables[0].Rows[0]["newUserId"]);

                return newId;


            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int CreateUtyility(BillPaymnet createUtility)
        {
            try
            {
                command = db.GetStoredProcCommand("CreateUtility", new object[]
                {

                    createUtility.UtilityName,
                    createUtility.UtilityCode,
                    createUtility.CreatedBy

                });
                DataSet ds = db.ExecuteDataSet(command);
                int newId = Convert.ToInt32(ds.Tables[0].Rows[0]["UtilityId"]);

                return newId;



            }
            catch (Exception ex)
            {
                return 0;

            }
        }

        public int CreateCustomer(BillPaymnet customer)
        {
            try
            {
                command = db.GetStoredProcCommand("sp_CreateCustomer", new object[] {
                customer.ReferenceNumber,
                customer.CustomerName,
                 customer.Email,
                customer.PhoneNumber,
                customer.PasswordHash,
                customer.UtilityCode,
                customer.CreatedBy


                });
                DataSet ds = db.ExecuteDataSet(command);
                int newId = Convert.ToInt32(ds.Tables[0].Rows[0]["CustomerId"]);

                return newId;


            }
            catch (Exception ex)
            {

                return 0;

            }
        }

        public int CreateVendor(BillPaymnet vendor)
        {
            try
            {


                command = db.GetStoredProcCommand("sp_CreateVendor", new object[] {


            vendor.VendorCode,
            vendor.VendorName,
            vendor.ContactEmail,
            vendor.ContactPhone,
            vendor.PasswordHash,
            vendor.Balance,
            vendor.CreatedBy


                });
                DataSet ds = db.ExecuteDataSet(command);
                int newId = Convert.ToInt32(ds.Tables[0].Rows[0]["CustomerId"]);

                return newId;


            }
            catch (Exception ex)
            {

                return 0;

            }
        }


        public int LoginUser(BillPaymnet loginuser)
        {


            try
            {

                string usernameOrEmail = string.IsNullOrEmpty(loginuser.Email) ? loginuser.Username : loginuser.Email;

                command = db.GetStoredProcCommand("sp_LoginUser", new object[]
                {
            usernameOrEmail,
            loginuser.PasswordHash
                });

                DataSet ds = db.ExecuteDataSet(command);

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count == 0)
                    {

                        return -1;
                    }


                    if (dt.Columns.Contains("ResultCode"))
                    {
                        return Convert.ToInt32(dt.Rows[0]["ResultCode"]);
                    }
                    else
                    {

                        int roleId = Convert.ToInt32(dt.Rows[0]["RoleID"]);

                        return roleId;
                    }
                }

                return -1;
            }
            catch (Exception ex)
            {

                Console.WriteLine("Login error: " + ex.Message);
                return -99;
            }

        }

        public string ValidateUtilityReference(string vendorCode, string referenceNumber)
        {
            try
            {
                command = db.GetStoredProcCommand("sp_GetCustomerInfoByReference", new object[]
                {
            vendorCode,
            referenceNumber
                });

                DataSet ds = db.ExecuteDataSet(command);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var row = ds.Tables[0].Rows[0];
                    var customerInfo = new
                    {
                        CustomerName = row["CustomerName"].ToString(),
                        Email = row["Email"].ToString(),
                        Phone = row["Phone"].ToString(),
                        UtilityID = row["UtilityID"].ToString(),
                        UtilityCode = row["UtilityCode"].ToString(),
                    };
                    return JsonConvert.SerializeObject(customerInfo); // <== Easier to parse on front end
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string InitiateVendorPaymentTransaction(BillPaymnet bill)
        {
            try
            {
                command = db.GetStoredProcCommand("InitiateVendorPaymentTransaction", new object[]
                {
            bill.VendorCode,
            bill.ReferenceNumber,     // CustomerReference
            bill.UtilityCode,
            bill.Amount,
            bill.CreatedBy            // VendorUserID
                });

                DataSet ds = db.ExecuteDataSet(command);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string transactionId = ds.Tables[0].Rows[0]["TransactionID"].ToString();
                    return transactionId;
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public DataTable GetAllPendingTransaction()
        {
            command = db.GetStoredProcCommand("sp_GetPendingTransactionsForUtility");
            DataTable dt = db.ExecuteDataSet(command).Tables[0];

            return dt;
        }


        public void UpdateUtilityTransactionResult(Guid transactionID, string utilityToken, string utilityReceiptNo, string status)
        {
            try
            {
                command = db.GetStoredProcCommand(
                    "sp_UpdateUtilityTransactionResult",
                    transactionID,
                    utilityToken,
                    utilityReceiptNo,
                    status
                );

                db.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                // Log exception properly
                Console.WriteLine("Error updating transaction result: " + ex.Message);
            }
        }


        public BillPaymnet GetVendorDashboardSummary(int userId)
        {
            try
            {
                command = db.GetStoredProcCommand("sp_GetVendorDashboardSummaryByUser", userId);
                DataSet ds = db.ExecuteDataSet(command);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];

                    return new BillPaymnet
                    {
                        VendorName = row["VendorName"].ToString(),
                        AccountBalance = Convert.ToDecimal(row["AccountBalance"]),
                        CustomersWorked = Convert.ToInt32(row["CustomersWorked"]),
                        TotalPaymentsMade = Convert.ToDecimal(row["TotalPaymentsMade"]),
                        FailedPayments = Convert.ToInt32(row["FailedPayments"]),
                        TotalTransactions = Convert.ToInt32(row["TotalTransactions"])
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Dashboard fetch error: " + ex.Message);
                return null;
            }
        }



        public BillPaymnet GetCustomerandVendorInfor()
        {
            try
            {
                command = db.GetStoredProcCommand("sp_GetCompletedTransactionsForEmailNotification");
                DataSet ds = db.ExecuteDataSet(command);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];

                    return new BillPaymnet
                    {
                        TransactionID = row["TransactionID"] != DBNull.Value
                            ? (Guid?)Guid.Parse(row["TransactionID"].ToString())
                            : null,

                        VendorName = row["VendorName"]?.ToString(),
                        ContactEmail = row["VendorEmail"]?.ToString(), 

                        CustomerName = row["CustomerName"]?.ToString(),
                        Email = row["CustomerEmail"]?.ToString(),       

                        ReferenceNumber = row["ReferenceNumber"]?.ToString(),
                        Amount = row["Amount"] != DBNull.Value
                            ? Convert.ToDecimal(row["Amount"])
                            : 0,

                        UtilityToken = row["UtilityToken"]?.ToString(),
                        UtilityReceiptNo = row["UtilityReceiptNo"]?.ToString(),

                        ProcessedAt = row["ProcessedAt"] != DBNull.Value
                            ? Convert.ToDateTime(row["ProcessedAt"])
                            : (DateTime?)null
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching vendor/customer info: " + ex.Message);
                return null;
            }
        }






    }
}

