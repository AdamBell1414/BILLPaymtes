using BillPaymentProject.objectControls;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
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
            db = factory.Create("BillPayment");

        }

        public int CreateUser(BillPaymnet createuser)
        {
            try
            {
                command = db.GetStoredProcCommand("sp_CreateUser", new object[]
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
                command = db.GetStoredProcCommand("sp_createUtility", new object[]
                {

                    createUtility.UtilityName,
                    createUtility.UtilityCode,

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

        //public int Customer(BillPaymnet customer)
        //{
        //    try {
        //        command = db.GetStoredProcCommand("SP_CreateCustomer", new object[] { 
        //        customer.
                
        //        });
            
        //    }
        //    catch (Exception ex) { 
            
        //    }
        //}
    }
}

