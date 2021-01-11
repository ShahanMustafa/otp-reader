using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTPReader
{
    static class Logger
    {
        static private string filePath { get; set; }
        static private string fileName { get; set; }
        static private StreamWriter sw{ get; set; }

        public static void InitializeLogger()
        {
            fileName = "Log.txt";
            filePath = Directory.GetCurrentDirectory() + "/" + fileName;
            OpenTheFile();
        }
        private static void OpenTheFile()
        {
            if (!String.IsNullOrEmpty(fileName) || !String.IsNullOrEmpty(filePath))
            {
               sw = new StreamWriter(filePath, false);
            }
        }

        public static void CloseTheFile()
        {
            if (sw!=null)
            sw.Close();
        } 

        private static void Log(string logLevel , string message)
        {
            string time = DateTime.Now.ToString("t");
            string log = time + " [" + logLevel + "] " + message ;

            Console.WriteLine(log);
            sw.WriteLine(log);
        }
        public static void LogError(string message)
        {
            Log("ERROR", message);
        }

        public static void LogDebug(string message)
        {
            Log("DEBUG", message);
        }

        public static void LogWarning(string message)
        {
            Log("WARNING", message);
        }

    }
}
