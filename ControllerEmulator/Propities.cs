using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace ControllerEmulator
{
    class Propities 
    {
        public string path;

        public Propities()
        {
            path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);                
            Directory.SetCurrentDirectory(path);
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            path = Path.Combine(path, "propities");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public ConnectionPropities GetConnect()
        {
            if (!System.IO.File.Exists(Path.Combine(path, "connect.json")))
                CreateConnectPropities();
            return ReadConnectPropities();
        }

        public ControllerPropities GetControllerPropities()
        {
            if (!System.IO.File.Exists(Path.Combine(path, "controller.json")))
                CreateControllerPropities();
            return ReadControllerPropities();
        }

        public TVDevicePropities GetTVDevicePropities()
        {
            if (!System.IO.File.Exists(Path.Combine(path, "tv.json")))
                CreateTVDevicePropities();
            return ReadTVDevicePropities();
        }

        private void CreateConnectPropities()
        {
            JSON.CreateEmptyConnectionPripities(Path.Combine(path, "connect.json"));
            Console.WriteLine("Connection json was generated. Please enter valid data");
        }

        private void CreateControllerPropities()
        {
            JSON.CreateEmptyControllerPropities(Path.Combine(path, "controller.json"));
            Console.WriteLine("Controller json was generated. Please enter valid token");
        }

        private void CreateTVDevicePropities()
        {
            JSON.CreateEmptyTVDevicePropities(Path.Combine(path, "tv.json"));
            Console.WriteLine("TV json was generated. Please enter valid data");
        }

        private ConnectionPropities ReadConnectPropities()
        {
            return JSON.ReadConnection(Path.Combine(path, "connect.json"));
        }

        private ControllerPropities ReadControllerPropities()
        { 
            return JSON.ReadControllerPropities(Path.Combine(path, "controller.json"));
        }

        private TVDevicePropities ReadTVDevicePropities()
        {
            return JSON.ReadTVDevicePropities(Path.Combine(path, "tv.json"));
        }


    }
}
