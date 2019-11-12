using System;

namespace ControllerEmulator
{
    class Program
    {
        static void Main(string[] args)
        {
            ControllerConnection controllerConnection = new ControllerConnection();
            bool status = false;
            
            ControllerCommands.TokenAuth(controllerConnection, out status);
            ControllerCommands.StartLisen(controllerConnection);

            ControllerCommands.Check(controllerConnection, out status);
            ControllerCommands.TVDevice(controllerConnection);

            
            Console.WriteLine(controllerConnection.ReadRecive());
            

        }
    }
}
