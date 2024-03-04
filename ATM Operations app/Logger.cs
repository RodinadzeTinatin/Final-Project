using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ATM_Operations_app
{
    public class Logger
    {
        private readonly string filePath;

        public Logger(string filePath)
        {
            this.filePath = filePath;
        }

        public void Log(string logMessage)
        {
            try
            {

                var json = File.ReadAllLines(filePath).ToList();

                json.Add(logMessage);

                File.WriteAllLines(filePath, json);
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error logging message: {ex.Message}");
            }

        }

        public void ClearLog()
        {
            try
            {
                File.WriteAllText(filePath, "[]");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing log file: {ex.Message}");
            }
        }
    }
}
