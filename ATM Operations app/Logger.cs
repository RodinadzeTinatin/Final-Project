using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace ATM_Operations_app
{
    public class Logger
    {
        private readonly string filePath;
        private List<string> logs = new List<string>();
        private JsonSerializerOptions options1 = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true
        };

        public Logger(string filePath)
        {
            this.filePath = filePath;
            LoadLogData(filePath);
        }

        public void LoadLogData(string filePath)
        {

            if (File.Exists(filePath))
            {
                try
                {
                    string json = File.ReadAllText(filePath);
                    logs = JsonSerializer.Deserialize<List<string>>(json);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading customer data: {ex.Message}");
                }
            }
            else
            {
                try
                {
                    File.WriteAllText(filePath, "[]");
                    logs = new List<string>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating customer data file: {ex.Message}");
                }

            }
        }

        public void Log(string logMessage)
        {
            string decodedLogMessage = Regex.Unescape(logMessage);

            logs.Add(decodedLogMessage);    
            
            try
            {
                string json = JsonSerializer.Serialize(logs, options1);

                File.WriteAllText(filePath, json);
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
