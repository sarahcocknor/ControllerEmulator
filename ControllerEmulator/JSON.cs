using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace ControllerEmulator
{
    class JSON
    {

        public static void CreateEmptyConnect(string path)
        {
            ConnectionPropities connectionPropities = new ConnectionPropities() { ip = "127.0.0.1", port = 8002 };
            string json = JsonSerializer.Serialize<ConnectionPropities>(connectionPropities);
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
                connectionPropities = JsonSerializer.Deserialize<ConnectionPropities>(json);
                return connectionPropities;
            }
        }

    }
}
