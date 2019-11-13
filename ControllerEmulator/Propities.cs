using System;
using System.Collections.Generic;
using System.IO;


namespace ControllerEmulator
{
    class Propities 
    {
        public string path;


        public Propities()
        {
            path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);                
            Directory.SetCurrentDirectory(path);
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

        public List<TVDevicePropities> GetTVDevicePropities()
        {
            if (!System.IO.File.Exists(Path.Combine(path, "tv.json")))
                CreateTVDevicePropities();
            return ReadTVDevicePropities();
        }

        private void CreateConnectPropities()
        {
            JsonWorker.CreateEmptyConnectionPripities(Path.Combine(path, "connect.json"));
            Console.WriteLine("Connection json was generated. Please enter valid data");
        }

        private void CreateControllerPropities()
        {
            JsonWorker.CreateEmptyControllerPropities(Path.Combine(path, "controller.json"));
            Console.WriteLine("Controller json was generated. Please enter valid token");
        }

        private void CreateTVDevicePropities()
        {
            ControllerPropities controllerPropities;
            controllerPropities = JsonWorker.ReadControllerPropities(Path.Combine(path, "controller.json"));
            JsonWorker.CreateEmptyTVDevicePropities(Path.Combine(path, "tv.json"), controllerPropities);
            Console.WriteLine("TV json was generated. Please enter valid data");
        }

        private ConnectionPropities ReadConnectPropities()
        {
            return JsonWorker.ReadConnection(Path.Combine(path, "connect.json"));
        }

        private ControllerPropities ReadControllerPropities()
        { 
            return JsonWorker.ReadControllerPropities(Path.Combine(path, "controller.json"));
        }

        private List<TVDevicePropities> ReadTVDevicePropities()
        {
            return JsonWorker.ReadTVDevicePropities(Path.Combine(path, "tv.json"));
        }

        public void SaveTVDevices(List<TVDevicePropities> tVs)
        {
            JsonWorker.DeviceSave(JsonWorker.SerializeTVDevicePropities(tVs), Path.Combine(path, "tv.json"));
        }

    }
}
