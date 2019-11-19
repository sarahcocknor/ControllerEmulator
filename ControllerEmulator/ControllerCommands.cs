using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ControllerEmulator
{
    static class ControllerCommands
    {

        public static void Reconect()
        {
            ControllerConnection controller = new ControllerConnection();

            ControllerCommands.TokenAuth(controller);
            ControllerCommands.StartLisen(controller);
        }

        public static void Device(ControllerConnection controllerConnection, string message)
        {
            //on load try
            Propities device = new Propities();

            //change
            string deviceType = ServerMessage.FindDevice(message);
            List<object> devicePropities = ServerMessage.ChangeParam(message, deviceType);

            //send
            string m = ServerMessage.ChangeParamSending(device.DeviceToServerMessage(devicePropities, deviceType), message);
            Console.WriteLine("SENDING: " + m);
            controllerConnection.Send(m);

            //save
            if (devicePropities != null)
                device.SaveDevices(devicePropities);

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

        public static void FullSend(ControllerConnection controllerConnection)
        {
            Console.Write("Sending list of all devices...");

            string deviceType;
            Propities controller = new Propities();

            List<TVDevicePropities> tVDevicePropities = controller.GetTVDevicePropities();          
            deviceType = "tv.json";
            foreach (TVDevicePropities tVDevice in tVDevicePropities)
            {
                controllerConnection.Send(controller.DeviceToServerMessage(tVDevice, deviceType));
            }

            List<ProjectorDevicePropities> projectorDevicePropities = controller.GetProjectorDevicePropities();
            deviceType = "projector.json";
            foreach (ProjectorDevicePropities projectorDevice in projectorDevicePropities)
            {
                controllerConnection.Send(controller.DeviceToServerMessage(projectorDevice, deviceType));
            }

            Console.WriteLine(" Done.");

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

        public static void RandomError(ControllerConnection controllerConnection, Random random)
        {


            int type;
            string message = null ;
            type = random.Next(2);

            Console.WriteLine("Randomize error for type number " + type);

            string deviceType;
            Propities controller = new Propities();
            int deviceIndex;

            switch (type)
            {
                case 0:
                    List<TVDevicePropities> tVDevicePropities = controller.GetTVDevicePropities();
                    deviceType = "tv.json";

                    deviceIndex = random.Next(0, tVDevicePropities.Count - 1);
                    TVDevicePropities.ErrorCode errorCodeT = (TVDevicePropities.ErrorCode)(random.Next(0, 5) * 100);

                    tVDevicePropities[deviceIndex].errorCode = errorCodeT;
                    message = controller.DeviceToServerMessage(tVDevicePropities[deviceIndex], deviceType);
                    break;

                case 1:
                    List<ProjectorDevicePropities> projectorDevicePropities = controller.GetProjectorDevicePropities();
                    deviceType = "projector.json";

                    deviceIndex = random.Next(0, projectorDevicePropities.Count - 1);
                    ProjectorDevicePropities.ErrorCode errorCodeP = (ProjectorDevicePropities.ErrorCode)(random.Next(0, 5) * 100);

                    projectorDevicePropities[deviceIndex].errorCode = errorCodeP;
                    message = controller.DeviceToServerMessage(projectorDevicePropities[deviceIndex], deviceType);
                    break;
            }
            controllerConnection.Send(message);

            Console.WriteLine("Error sended: " + message);


        }

        public static void ProjectorLamp(ControllerConnection controllerConnection)
        {
            Propities controller = new Propities();
            List<ProjectorDevicePropities> projectorDevicePropities = controller.GetProjectorDevicePropities();
            


            foreach (ProjectorDevicePropities projectorDevice in projectorDevicePropities)
            {


                projectorDevice.lamphours += 1;
                controllerConnection.Send(controller.DeviceToServerMessage(projectorDevice, "projector.json"));
                

            }


            List<object> list = new List<ProjectorDevicePropities>().Cast<object>().ToList();

            controller.SaveDevices(list);

        }
        
    }
}
