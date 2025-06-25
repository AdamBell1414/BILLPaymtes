using BillPaymentProject.objectControls;
using Microsoft.Practices.EnterpriseLibrary.Data;
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


    }
}

