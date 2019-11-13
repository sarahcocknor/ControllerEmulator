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

            ServerMessage m = JsonWorker.ReadServerMessage(message);
            m.deviceid = m.deviceid.Trim('"');
            m.param = m.param.Trim('"');
            m.val = m.val.Trim('"');
            TVDevicePropities curent = tVDevicePropities.Find(tVDevicePropities => tVDevicePropities.deviceId == m.deviceid);


            if (curent != null)
                curent.ParamChange(m.param, m.val);
            else
                Console.WriteLine("Device not foind in tv.json");
            return tVDevicePropities;

            //поиск девайс айди и присваивание типа соответственно джейсону для этого нужно хранить лист устройств в одном списке нужно реализовать несколько устройств и читать их
        }
    }
}
