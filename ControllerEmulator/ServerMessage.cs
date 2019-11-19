using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerEmulator
{
    class ServerMessage
    {
        public string deviceid { get; set; }
        public string param { get; set; }
        public string val { get; set; }


        public static string FindDevice(string message)
        {
            Propities propities = new Propities();
            ServerMessage m = ServerMessageConvert(message);

            return propities.GetTypeOfDevice(m.deviceid);
        }

        public static List<object> ChangeParam(string message, string deviceType)
        {
            Propities propities = new Propities();
            ServerMessage m = ServerMessageConvert(message);

            switch (deviceType)
            { 
                case "tv.json":
                    List<TVDevicePropities> tVDevicePropities = propities.GetTVDevicePropities();
                    TVDevicePropities currentTv = tVDevicePropities.Find(tVDevicePropities => tVDevicePropities.deviceId == m.deviceid);



                    if (currentTv != null)
                        currentTv.ParamChange(m.param, m.val);
                    else
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " (WARNING): Device not found in tv.json-----------------------");


                    List<object> listTv = new List<object>();
                    foreach (TVDevicePropities cur in tVDevicePropities)
                    {
                        listTv.Add((object)cur);
                    }
                    return listTv;

                case "projector.json":
                    List<ProjectorDevicePropities> projectorDevicePropities = propities.GetProjectorDevicePropities();
                    ProjectorDevicePropities currentProj = projectorDevicePropities.Find(projectorDevicePropities => projectorDevicePropities.deviceId == m.deviceid);
                    //for projector here

                    if (currentProj != null)
                        currentProj.ParamChange(m.param, m.val);
                    else
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " (WARNING): Device not found in projector.json");

                    List<object> listProj = new List<object>();
                    foreach (ProjectorDevicePropities cur in projectorDevicePropities)
                    {
                        listProj.Add((object)cur);
                    }
                    return listProj;

                default:
                    Console.WriteLine(DateTime.Now.ToShortTimeString() + " (WARNING): Device with recived token not found in devices");
                    return null;
            }
            
        }

        public static string ChangeParamSending(string message, string deviceType)
        {
            string json;
            Propities propities = new Propities();
            ServerMessage m = ServerMessageConvert(deviceType);
            string currentDeviceType = FindDevice(deviceType);
            //change param analog here
            switch (currentDeviceType)
            {
                case "tv.json":
                    List<TVDevicePropities> tVDevicePropities = propities.GetTVDevicePropities();
                    TVDevicePropities curentTv = tVDevicePropities.Find(tVDevicePropities => tVDevicePropities.deviceId == m.deviceid);

                    if (curentTv != null)
                        curentTv.ParamChange(m.param, m.val);
                    else
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " (WARNING): Device not found in tv.json");

                    return propities.DeviceToServerMessage(curentTv, currentDeviceType);
                case "projector.json":

                    List<ProjectorDevicePropities> projectorDevicePropities = propities.GetProjectorDevicePropities();
                    ProjectorDevicePropities currentProj = projectorDevicePropities.Find(projectorDevicePropities => projectorDevicePropities.deviceId == m.deviceid);  

                    if (currentProj != null)
                        currentProj.ParamChange(m.param, m.val);
                    else
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " (WARNING): Device not found in projector.json");

                    return propities.DeviceToServerMessage(currentProj, currentDeviceType);
                default:
                    return null;
            }

            
        }

        private static ServerMessage ServerMessageConvert(string message)
        {
            ServerMessage m = JsonWorker.ReadServerMessage(message);
            m.deviceid = m.deviceid.Trim('"');
            m.param = m.param.Trim('"');
            m.val = m.val.Trim('"');
            return m;
        }

    }
}
