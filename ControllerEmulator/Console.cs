using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ControllerEmulator
{
    static class Console
    {

        static readonly string _logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.log");

        public static void WriteLine(string line)
        {
            try
            { 
            File.AppendAllText(_logPath, line + Environment.NewLine);
            System.Console.WriteLine(line);
            }
            catch (Exception e)
            {
            System.Console.WriteLine(line);
            }
        }

    }
}
