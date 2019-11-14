using System;
using System.Collections.Generic;
using System.Threading;

namespace ControllerEmulator
{
    static class ControllerCommands
    {
        public static void TVDevice(ControllerConnection controllerConnection, string messege)
        {
            //on load try
            Propities tvdevice = new Propities();

            //change
            List<TVDevicePropities> tVDevicePropities = ServerMessage.ChangeParam(messege);
            //send
            string m = ServerMessage.ChangeParamSending(tVDevicePropities, messege);
            Console.WriteLine("SENDING: " + m);
            controllerConnection.Send(m);

            //save
            tvdevice.SaveTVDevices(tVDevicePropities);

        }

        public static void StartLisen(ControllerConnection controllerConnection)
        {
            Thread lisenThread = new Thread(new ParameterizedThreadStart(controllerConnection.LisenServer));
            lisenThread.Start(controllerConnection);
        }

        public static void StopLisen()
        {
            //do
        }

        public static void TokenAuth(ControllerConnection controllerConnection)
        {
            Propities controller = new Propities();
            ControllerPropities controllerPropities = controller.GetControllerPropities();
            controllerConnection.Send(controllerPropities.token);
           
        }

        public static void TokenAuth(ControllerConnection controllerConnection, out bool status)
        {
            Propities controller = new Propities();
            ControllerPropities controllerPropities = controller.GetControllerPropities();
            controllerConnection.Send(controllerPropities.token);
            status = TokenAuthStatus(controllerConnection);
        }

        private static bool TokenAuthStatus(ControllerConnection controllerConnection)
        {
            if (controllerConnection.ReadRecive() == "authorization completed\n")
            {
                Console.WriteLine("auth success");
                return true;
            }
            else
            {
                Console.WriteLine("auth failed");
                return false;
            }

        }

        public static void Check(ControllerConnection controllerConnection)
        {
            controllerConnection.Send("<{connection check}>");
        }
        
        public static void Check(ControllerConnection controllerConnection, out bool status)
        {
            controllerConnection.Send("<{connection check}>");
            status = CheckStatus(controllerConnection);
        }

        private static bool CheckStatus(ControllerConnection controllerConnection)
        {
            if (controllerConnection.ReadRecive() == "check successful\n")
            {
                Console.WriteLine("check success");
                return true;
            }
            else
            {
                Console.WriteLine("check failed");
                return false;
            }

        }

        public static void MessageHandler(string m)
        { 
        
        }

        
    }
}
