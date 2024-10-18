using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_7_PR_7_2_Observer
{
    public class Logger
    {
        private string _logFilePath;
        public Logger(string logFilePath)
        {
            _logFilePath = logFilePath;
        }
        public void Log(string message)
        {
            try
            {
                File.AppendAllText(_logFilePath, $"{DateTime.Now}: {message}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"failed to write log: {ex.Message}");
            }
        }
    }
}
