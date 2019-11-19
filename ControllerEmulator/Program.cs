using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using Quartz;
using Quartz.Impl;

namespace ControllerEmulator
{
    class Program
    {
        private static ControllerConnection controller;
        private static bool reconect = false;
        static void Main()
        {
            controller = new ControllerConnection();

            ControllerCommands.TokenAuth(controller, reconect);
            ControllerCommands.StartLisen(controller);
            
            controller.On_Messege += OnMessage;
            controller.On_Exception += Controller_On_Exception;

        }

        private static void Controller_On_Exception(object sender, EventArgs e)
        {
            Propities propities = new Propities();

            reconect = true;
            controller.reconect = true;

            ControllerCommands.StopLisen(controller);

            Thread.Sleep( (propities.GetControllerPropities().reconectTimeOut/2) * 1000);
            Main();
        }

        public static void OnMessage(object sender, string m)
        {
            string[] marray = m.Split("\n");
            if (marray[marray.Length - 1] == "")
                marray = marray.Take(marray.Count() - 1).ToArray();
            foreach (string current in marray)
                Console.WriteLine(DateTime.Now.ToShortTimeString() + " (SERVER): " + current);
            
            if (m.StartsWith('{'))
                ControllerCommands.Device(controller, m);

            if (m == "authorization token not found\n")
                ControllerCommands.TokenAuth(controller, reconect);

        }
    }
}
