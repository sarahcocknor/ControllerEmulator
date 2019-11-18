using System;
using System.Threading;

namespace ControllerEmulator
{
    class Program
    {
        private static ControllerConnection controller;


        private static bool status = false;
        static void Main(string[] args)
        {
            controller = new ControllerConnection();
            CheckScheldue scheduleJob = new CheckScheldue();

            ControllerCommands.TokenAuth(controller);
            ControllerCommands.StartLisen(controller);
            
            controller.On_Messege += OnMessage;
            controller.On_Exception += Controller_On_Exception;

            Console.ReadLine();

        }

        private static void Controller_On_Exception(object sender, EventArgs e)
        {
            //timer to reconect
            controller = new ControllerConnection();
            CheckScheldue scheduleJob = new CheckScheldue();

            ControllerCommands.TokenAuth(controller);
            ControllerCommands.StartLisen(controller);
        }

        public static void OnMessage(object sender, string m)
        {
            Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + m);
            
            if (m.StartsWith('{'))
                ControllerCommands.Device(controller, m);

        }
    }
}
