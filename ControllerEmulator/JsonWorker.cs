using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace ControllerEmulator
{
    static class JsonWorker
    {
        
        public static void CreateEmptyConnectionPripities(string path)
        {

            ConnectionPropities connectionPropities = new ConnectionPropities() { ip = "127.0.0.1", port = 8002 };
            string json = JsonConvert.SerializeObject(connectionPropities);
            Save(json, path);
            Console.WriteLine(json);
        }

        public static void CreateEmptyControllerPropities(string path)
        {
            ControllerPropities controllerPropities = new ControllerPropities() { token = "controller-token-here", deviceTvCount = 3};
            string json = JsonConvert.SerializeObject(controllerPropities);
            Save(json, path);
            Console.WriteLine(json);
        }

        public static void CreateEmptyTVDevicePropities(string path, ControllerPropities controllerPropities)
        {
            uint i = controllerPropities.deviceTvCount;
            List<TVDevicePropities> tvList = new List<TVDevicePropities>();
            do
            {
                tvList.Add(new TVDevicePropities() { deviceId = "device-id-here", status = true, errorCode = GlobalPropities.ErrorCode.None, errorMessage = "ok", input = TVDevicePropities.Input.DVI1, power = TVDevicePropities.Power.True, _volume = 50, volumeMute = false });
                i--;
            }
            while (i != 0);
            string json = JsonConvert.SerializeObject(tvList);
            Save(json, path);
            Console.WriteLine(json);
        }

        public static string SerializeTVDevicePropities(List<TVDevicePropities> tVs)
        {
            return JsonConvert.SerializeObject(tVs);
        }

        public static void Save(string json, string path)
        {
            UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
            byte[] result = unicodeEncoding.GetBytes(json);
            if (!System.IO.File.Exists(path))
                System.IO.File.Create(path).Close();
                
            using (FileStream fileStream = new FileStream(path, FileMode.Truncate))
            using (StreamWriter streamWriter = new StreamWriter(fileStream))
            {
                streamWriter.Write(json);
            }
        }

        public static ConnectionPropities ReadConnection(string path)
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                string json = streamReader.ReadToEnd();
                ConnectionPropities connectionPropities;
                connectionPropities = JsonConvert.DeserializeObject<ConnectionPropities>(json);
                return connectionPropities;
            }
        }

        public static ControllerPropities ReadControllerPropities(string path)
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                string json = streamReader.ReadToEnd();
                ControllerPropities controllerPropities;
                controllerPropities = JsonConvert.DeserializeObject<ControllerPropities>(json);
                return controllerPropities;
            }
        }

        public static List<TVDevicePropities> ReadTVDevicePropities(string path)
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                string json = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<TVDevicePropities>>(json);
            }
        }

        public static ServerMessage ReadServerMessage(string message)
        { 
            return JsonConvert.DeserializeObject<ServerMessage>(message);
        }

        public static void DeviceSave(string json, string path )
        {
            Save(json, path);
        }

        public static string SerializeDevice(object? device)
        {
            return JsonConvert.SerializeObject(device);
        }



    }
}
