<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ControllerEmulator
{
    static class JsonWorker
    {
        
        public static void CreateEmptyConnectionPropities(string path)
        {

            ConnectionPropities connectionPropities = new ConnectionPropities() { ip = "127.0.0.1", port = 8002 };
            string json = JsonConvert.SerializeObject(connectionPropities);
            Save(json, path);
            Console.WriteLine(json);
        }

        public static void CreateEmptyControllerPropities(string path)
        {
            ControllerPropities controllerPropities = new ControllerPropities() { token = "controller-token-here", deviceTvCount = 3 , deviceProjectorCount = 2};
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

        public static void CreateEmptyProjectorPropities(string path, ControllerPropities controllerPropities)
        {
            uint i = controllerPropities.deviceProjectorCount;
            List<ProjectorDevicePropities> list = new List<ProjectorDevicePropities>();
            do
            {
                list.Add(new ProjectorDevicePropities() { deviceId = "device-id-here", status = true, errorCode = GlobalPropities.ErrorCode.None, errorMessage = "ok", input = ProjectorDevicePropities.Input.DVI1, power = ProjectorDevicePropities.Power.True, lamphours = 50, lampStatus = ProjectorDevicePropities.LampStatus.On });
                i--;
            }
            while (i != 0);
            string json = JsonConvert.SerializeObject(list);
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
            message = message.Trim('<'); message = message.Trim('[');
            message = message.Trim('>'); message = message.Trim(']');
            message = message.Replace("}|{", ",");
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

        public static List<object> ReadDevices(string path)
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                string json = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<object>>(json);
            }
        }

        public static string FirstInObject(object obj)
        {
            Newtonsoft.Json.Linq.JObject jObject = (Newtonsoft.Json.Linq.JObject) obj;
            
            return (string)jObject.First.First;
        }

        public static string FirstInObject(List<object> obj)
        {
            object current = obj.First<object>();
            string sd = JsonConvert.SerializeObject(current);
            Newtonsoft.Json.Linq.JObject jObject = null;
            sd = sd.Remove(0, 13);
            sd = sd.Remove(36);

            return sd;
        }



    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ControllerEmulator
{
    static class JsonWorker
    {
        
        public static void CreateEmptyConnectionPropities(string path)
        {

            ConnectionPropities connectionPropities = new ConnectionPropities() { ip = "127.0.0.1", port = 8002 };
            string json = JsonConvert.SerializeObject(connectionPropities);
            Save(json, path);
            Console.WriteLine(json);
        }

        public static void CreateEmptyControllerPropities(string path)
        {
            ControllerPropities controllerPropities = new ControllerPropities() { token = "controller-token-here", deviceTvCount = 3 , deviceProjectorCount = 2};
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

        public static void CreateEmptyProjectorPropities(string path, ControllerPropities controllerPropities)
        {
            uint i = controllerPropities.deviceProjectorCount;
            List<ProjectorDevicePropities> list = new List<ProjectorDevicePropities>();
            do
            {
                list.Add(new ProjectorDevicePropities() { deviceId = "device-id-here", status = true, errorCode = GlobalPropities.ErrorCode.None, errorMessage = "ok", input = ProjectorDevicePropities.Input.DVI1, power = ProjectorDevicePropities.Power.True, lamphours = 50, lampStatus = ProjectorDevicePropities.LampStatus.On });
                i--;
            }
            while (i != 0);
            string json = JsonConvert.SerializeObject(list);
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
            message = message.Trim('<'); message = message.Trim('[');
            message = message.Trim('>'); message = message.Trim(']');
            message = message.Replace("}|{", ",");
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

        public static List<object> ReadDevices(string path)
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                string json = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<object>>(json);
            }
        }

        public static string FirstInObject(object obj)
        {
            Newtonsoft.Json.Linq.JObject jObject = (Newtonsoft.Json.Linq.JObject) obj;
            
            return (string)jObject.First.First;
        }

        public static string FirstInObject(List<object> obj)
        {
            object current = obj.First<object>();
            string sd = JsonConvert.SerializeObject(current);
            Newtonsoft.Json.Linq.JObject jObject = null;
            sd = sd.Remove(0, 13);
            sd = sd.Remove(36);

            return sd;
        }



    }
}
>>>>>>> ebae88adec8ba85ce51d7ee12f8038ccf660d0c0
