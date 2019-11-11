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

        private void CreateConnectPropities()
        {
            JSON.CreateEmptyConnect(Path.Combine(path, "connect.json"));
            //do создание джесона по шаблону
            Console.WriteLine("Connection json was generated. Please enter valid data");
        }

        private ConnectionPropities ReadConnectPropities()
        {
            return JSON.ReadConnection(Path.Combine(path, "connect.json"));
        }



    }
}
