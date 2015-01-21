using System.Collections.Generic;
// using System.ServiceProcess;
using System.Text;

namespace FastLoad
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            // main version
            Watcher wa = new Watcher();
            // target 1/19/2015
            // process();
        }
        static void process()
        {
            MockUpReader rdr = new MockUpReader();
            TestSalesOrder tester = new TestSalesOrder();
            SalesOrder order = rdr.FillInOrder();
            tester.ProcessOrder(order);

        }
    }
}
