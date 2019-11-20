<<<<<<< HEAD
﻿using System;
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
=======
﻿using System;
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
>>>>>>> bd10fdd39769e200c51b67b5372bdf62c2bf3ab4
