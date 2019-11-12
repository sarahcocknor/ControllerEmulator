using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace ControllerEmulator
{
    class JSON
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
            ControllerPropities controllerPropities = new ControllerPropities() { token = "controller-token-here"};
            string json = JsonConvert.SerializeObject(controllerPropities);
            Save(json, path);
            Console.WriteLine(json);
        }

        public static void CreateEmptyTVDevicePropities(string path)
        {
            TVDevicePropities tv = new TVDevicePropities() { deviceId = "device-id-here", status = true, errorCode = GlobalPropities.ErrorCode.None, errorMessage = "ok" , input = TVDevicePropities.Input.DVI1, power = TVDevicePropities.Power.True, _volume = 50, volumeMute = false };
            string json = JsonConvert.SerializeObject(tv);
            Save(json, path);
            Console.WriteLine(json);
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

        public static TVDevicePropities ReadTVDevicePropities(string path)
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                string json = streamReader.ReadToEnd();
                TVDevicePropities tVDevicePropities = JsonConvert.DeserializeObject<TVDevicePropities>(json);
                return tVDevicePropities;
            }
        }



    }
}
