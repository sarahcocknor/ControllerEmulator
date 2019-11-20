using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ControllerEmulator
{
    static class Console
    {

        static readonly string _logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.log");

        public static void WriteLine()
        {
            File.AppendAllText(_logPath, Environment.NewLine);
            System.Console.WriteLine();
        }

        public static void WriteLine(string line)
        {
            File.AppendAllText(_logPath, line + Environment.NewLine);
            System.Console.WriteLine(line);
        }

        public static void Write(string line)
        {
            File.AppendAllText(_logPath, line);
            System.Console.Write(line);
        }

        public static void Write(int n)
        {
            File.AppendAllText(_logPath, n.ToString());
            System.Console.Write(n);
        }
    }
}
