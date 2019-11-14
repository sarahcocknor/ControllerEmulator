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


        public static List<TVDevicePropities> ChangeParam(string message)
        {
            Propities propities = new Propities();
            List<TVDevicePropities> tVDevicePropities = propities.GetTVDevicePropities();

            ServerMessage m = ServerMessageConvert(message);

            TVDevicePropities curent = tVDevicePropities.Find(tVDevicePropities => tVDevicePropities.deviceId == m.deviceid);


            if (curent != null)
                curent.ParamChange(m.param, m.val);
            else
                Console.WriteLine("Device not found in tv.json");
            return tVDevicePropities;

        }

        public static string ChangeParamSending(List<TVDevicePropities> tVDevicePropities, string message)
        {
            string json;
            Propities propities = new Propities();
            ServerMessage m = ServerMessageConvert(message);
            TVDevicePropities curent = tVDevicePropities.Find(tVDevicePropities => tVDevicePropities.deviceId == m.deviceid);
            
            if (curent != null)
                curent.ParamChange(m.param, m.val);
            else
                Console.WriteLine("Device not found in tv.json");
            
            //нужен метод свойств принимающий устройство и преобразующий его в строку джейсона воспринимаюмую сервером обязательно учеть что это object. вернуть его

            return propities.DeviceToServerMessage(curent);
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
