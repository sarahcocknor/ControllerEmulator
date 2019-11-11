using System;

namespace ControllerEmulator
{
    class Program
    {
        static void Main(string[] args)
        {
            ControllerConnection controllerConnection = new ControllerConnection();
            controllerConnection.Send("2db09b37-2f56-47ce-5722-08d761eef482");
            controllerConnection.Send("<{connection check}>");
            controllerConnection.Recive();

        }
    }
}
