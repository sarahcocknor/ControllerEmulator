using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ControllerEmulator
{
    static class ControllerCommands
    {

        public static void Device(ControllerConnection controllerConnection, string message)
        {
            //on load try
            Propities device = new Propities();

            //change
            string deviceType = ServerMessage.FindDevice(message);
            List<object> devicePropities = ServerMessage.ChangeParam(message, deviceType);

            //send
            string m = ServerMessage.ChangeParamSending(device.DeviceToServerMessage(devicePropities, deviceType), message);
            Console.WriteLine(DateTime.Now.ToShortTimeString() + " (SENDING): " + m);
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

        public static void StopLisen(ControllerConnection controllerConnection)
        {
            controllerConnection.CloseConnection();
        }

        public static void TokenAuth(ControllerConnection controllerConnection, bool reconect)
        {
            
            if (reconect)
            {
                Propities propities = new Propities();
                Thread.Sleep( (propities.GetControllerPropities().reconectTimeOut / 2) * 1000 );
            }
                
            Console.WriteLine(DateTime.Now.ToShortTimeString() + " (INFO): SENDING TOKEN");
            Propities controller = new Propities();
            ControllerPropities controllerPropities = controller.GetControllerPropities();
            controllerConnection.Send(controllerPropities.token);


        }

        public static void Check(ControllerConnection controllerConnection)
        {
            controllerConnection.Send("<{connection check}>");
        }

        public static void FullSend(ControllerConnection controllerConnection)
        {
            Console.WriteLine(DateTime.Now.ToShortTimeString() + " (INFO): Sending list of all devices");

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
            Console.WriteLine(DateTime.Now.ToShortTimeString() + " (INFO): Done");

        }


        public static void RandomError(ControllerConnection controllerConnection, Random random)
        {


            int type;
            string message = null ;
            type = random.Next(2);

            Console.WriteLine(DateTime.Now.ToShortTimeString() + " (INFO): Randomize error for type number " + type +"");

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

            Console.WriteLine(DateTime.Now.ToShortTimeString() + " (INFO): Error was sended");

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
            list.InsertRange(0, projectorDevicePropities.Cast<object>().ToList());

            controller.SaveDevices(list);

        }
        
    }
}
