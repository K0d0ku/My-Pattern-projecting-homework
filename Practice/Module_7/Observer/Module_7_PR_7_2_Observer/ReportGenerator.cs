using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_7_PR_7_2_Observer
{
    public class ReportGenerator
    {
        private string _reportFilePath;
        public ReportGenerator(string reportFilePath)
        {
            _reportFilePath = reportFilePath;
        }
        public void GenerateReport(Dictionary<IObserver, int> notificationCounts, List<IObserver> observers)
        {
            try
            {
                File.WriteAllText(_reportFilePath, "subs report:\n----------------------------\n");
                foreach (var observer in observers)
                {
                    string observerName = observer.GetType().Name;
                    string reportEntry = $"{observerName}: notified {notificationCounts[observer]} times\n";
                    File.AppendAllText(_reportFilePath, reportEntry);
                }
                Console.WriteLine($"report created at: {_reportFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"failed to create report: {ex.Message}");
            }
        }
    }
}
