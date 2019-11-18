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
            System.Timers.Timer timer = new System.Timers.Timer(30000);
            timer.AutoReset = false;
            timer.Enabled = true;
            timer.Elapsed += Timer_Elapsed;

        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ControllerCommands.Reconect();
        }

        public static void OnMessage(object sender, string m)
        {
            Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + m);
            
            if (m.StartsWith('{'))
                ControllerCommands.Device(controller, m);

        }
    }
}
