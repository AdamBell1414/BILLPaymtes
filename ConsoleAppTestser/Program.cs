using BillPaymentProject.classobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTestser
{
    internal class Program
    {
        static void Main(string[] args)
        {

            DatabaseHandler dh = new DatabaseHandler();

            dh.GetAllPendingTransaction();
        }
    }
}
