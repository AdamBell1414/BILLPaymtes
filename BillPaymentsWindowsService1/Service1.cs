// File: TransactionQueueReceiverService.cs
using System;

using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using BillPaymentProject.classobjects;
using BillPaymentProject.objectControls;

using System.Message
namespace BillPaymentServices
{
    public partial class TransactionQueueReceiverService : ServiceBase
    {
        private readonly string queuePath = @".\private$\VendorTransactions";
        private static readonly ILog log = LogManager.GetLogger(typeof(TransactionQueueReceiverService));

        public TransactionQueueReceiverService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            log.Info("TransactionQueueReceiverService started.");
            Task.Run(() => ProcessQueue());
        }

        private void ProcessQueue()
        {
            try
            {
                if (!MessageQueue.Exists(queuePath))
                {
                    MessageQueue.Create(queuePath);
                    log.Info("Queue created: " + queuePath);
                }

                using (MessageQueue queue = new MessageQueue(queuePath))
                {
                    queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(BillPaymnet) });

                    while (true)
                    {
                        try
                        {
                            var message = queue.Receive();
                            var bill = (BillPaymnet)message.Body;

                            log.Info($"Received transaction for reference: {bill.ReferenceNumber}");

                            DatabaseHandler handler = new DatabaseHandler();
                            string transactionId = handler.InitiateVendorPaymentTransaction(bill);

                            if (!string.IsNullOrEmpty(transactionId))
                            {
                                log.Info($"Transaction inserted with ID: {transactionId}");
                            }
                            else
                            {
                                log.Warn("Transaction insertion failed.");
                            }
                        }
                        catch (Exception ex)
                        {
                            log.Error("Queue Processing Error: " + ex.Message);
                        }

                        Thread.Sleep(500); // Slight delay
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Fatal error in queue processor: " + ex.Message);
            }
        }

        protected override void OnStop()
        {
            log.Info("TransactionQueueReceiverService stopped.");
        }
    }
}
