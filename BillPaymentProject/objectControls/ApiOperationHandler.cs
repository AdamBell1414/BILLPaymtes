using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using BillPaymentProject.objectControls;

namespace BillPaymentProject.classobjects
{
    public static class ApiOperationHandler
    {
        public static ApiResponse<int> Handle(Func<int> operation, string successMessage)
        {
            try
            {
                int resultId = operation();

                if (resultId > 0)
                    return ApiResponse<int>.Ok(resultId, successMessage);
                else
                    return ApiResponse<int>.Fail("Operation failed or returned 0.");
            }
            catch (Exception ex)
            {
                return ApiResponse<int>.Fail("Error: " + ex.Message);
            }
        }
    }
}
