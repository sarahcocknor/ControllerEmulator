using ControllerEmulator.Scheldues;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

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
            GetAllDevices();
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

        public void GetAllDevices()
        {
            GetTVDevicePropities();
            GetProjectorDevicePropities();
        }
        public List<TVDevicePropities> GetTVDevicePropities()
        {
            if (!System.IO.File.Exists(Path.Combine(path, "tv.json")))
                CreateTVDevicePropities();
            return ReadTVDevicePropities();
        }

        public List<ProjectorDevicePropities> GetProjectorDevicePropities()
        {
            if (!System.IO.File.Exists(Path.Combine(path, "projector.json")))
                CreateProjectorPropities();
            return ReadProjectorDevicePropities();
        }

        private void CreateConnectPropities()
        {
            JsonWorker.CreateEmptyConnectionPropities(Path.Combine(path, "connect.json"));
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

        private void CreateProjectorPropities()
        {
            ControllerPropities controllerPropities;
            controllerPropities = JsonWorker.ReadControllerPropities(Path.Combine(path, "controller.json"));
            JsonWorker.CreateEmptyProjectorPropities(Path.Combine(path, "projector.json"), controllerPropities);
            Console.WriteLine("Projector json was generated. Please enter valid data");
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
            List<TVDevicePropities> tVDevices = new List<TVDevicePropities>();
            List<object> list = JsonWorker.ReadDevices(Path.Combine(path, "tv.json"));
            foreach (object current in list)
            {
                tVDevices.Add( TVDevicePropities.ConvertFromJObject(current) );
            }

            return tVDevices;
        }

        private List<ProjectorDevicePropities> ReadProjectorDevicePropities()
        {
            List<ProjectorDevicePropities> projectorDevicePropities = new List<ProjectorDevicePropities>();
            List<object> list = JsonWorker.ReadDevices(Path.Combine(path, "projector.json"));
            foreach (object current in list)
            {
                projectorDevicePropities.Add( ProjectorDevicePropities.ConvertFromJObject(current) );
            }
            return projectorDevicePropities;
        }


        

        public void SaveDevices(List<object?> device)
        {

            string firstDeviceInObject = JsonWorker.FirstInObject(device);
            JsonWorker.DeviceSave(JsonWorker.SerializeDevice(device), Path.Combine(path, GetTypeOfDevice(firstDeviceInObject)));
        }

        public string DeviceToServerMessage(object? device, string deviceType) 
        {
            string json = JsonWorker.SerializeDevice(device);
            int indexOfEndFirstJson;
            switch (deviceType)
            {
                case "tv.json":
                    indexOfEndFirstJson = json.IndexOf(",\"input");
                    break;
                case "projector.json":
                    indexOfEndFirstJson = json.IndexOf(",\"lamphours");
                    break;
                default:
                    indexOfEndFirstJson = 0;
                    break;
            }

            json = json.Remove(indexOfEndFirstJson, 1);
            json = json.Insert(indexOfEndFirstJson, "}|{");
            json = json.Insert(0, "<");
            json = json.Insert(json.Length, ">");
            return json;


        }



        public string GetTypeOfDevice(string deviceid)
        {
            List<string> devices = new List<string>();
            devices.Add("tv.json");
            devices.Add("projector.json");

            string deviceType = null;
            foreach (string cur in devices)
            {
                List<object> listOfCurrentDevices = JsonWorker.ReadDevices(Path.Combine(path, cur));
                if (Search(listOfCurrentDevices, deviceid))
                    deviceType = cur;
            }


            return deviceType;
        }

        private bool Search(List<object> searchIn, string search)
        {
            foreach (object cur in searchIn)
            {
                if ( JsonWorker.FirstInObject(cur) == search)
                    return true;
            }
            return false;
        }

        public void StartScheldue(object controllerConnection)
        {
            StartCheckScheldue(controllerConnection);
            StartFullSendScheldue(controllerConnection);
            StartRandomScheldue(controllerConnection);
            LampHoursScheldue(controllerConnection);
        }

        private async Task StartFullSendScheldue(object controllerConnection)
        {
            
            ControllerPropities controllerPropities = GetControllerPropities();
            var controller = controllerConnection;

            NameValueCollection props = new NameValueCollection { { "quartz.serializer.type", "binary" } };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();

            JobDataMap keyValuePairs = new JobDataMap();
            keyValuePairs.Add("controllerConnection", controller);


            IJobDetail job = JobBuilder.Create<FullSendScheldue>()
                .UsingJobData(keyValuePairs)
                .Build();


            ITrigger trigger = TriggerBuilder.Create()
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(controllerPropities.fludInterval)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        private async Task StartCheckScheldue(object controllerConnection)
        {

            
            ControllerPropities controllerPropities = GetControllerPropities();
            if (controllerPropities.checkInterval != 0)
            {
                var controller = controllerConnection;

                NameValueCollection props = new NameValueCollection { { "quartz.serializer.type", "binary" } };
                StdSchedulerFactory factory = new StdSchedulerFactory(props);
                IScheduler scheduler = await factory.GetScheduler();
                await scheduler.Start();

                JobDataMap keyValuePairs = new JobDataMap();
                keyValuePairs.Add("controllerConnection", controller);


                IJobDetail job = JobBuilder.Create<CheckScheldue>()
                    .UsingJobData(keyValuePairs)
                    .Build();


                ITrigger trigger = TriggerBuilder.Create()
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(controllerPropities.checkInterval)
                        .RepeatForever())
                    .Build();

                await scheduler.ScheduleJob(job, trigger);

            }
        }

        private async Task StartRandomScheldue(object controllerConnection)
        {

            ControllerPropities controllerPropities = GetControllerPropities();
            if (controllerPropities.errorRate != 0)
            {
                Random random = new Random();
                var controller = controllerConnection;

                NameValueCollection props = new NameValueCollection { { "quartz.serializer.type", "binary" } };
                StdSchedulerFactory factory = new StdSchedulerFactory(props);
                IScheduler scheduler = await factory.GetScheduler();
                await scheduler.Start();

                JobDataMap keyValuePairs = new JobDataMap();
                keyValuePairs.Add("controllerConnection", controller);
                keyValuePairs.Add("random", random);


                IJobDetail job = JobBuilder.Create<ErrorSimulateScheldue>()
                    .UsingJobData(keyValuePairs)
                    .Build();


                ITrigger trigger = TriggerBuilder.Create()
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(controllerPropities.errorRate)
                        .RepeatForever())
                    .Build();

                await scheduler.ScheduleJob(job, trigger);

            }
        }

        private async Task LampHoursScheldue(object controllerConnection)
        {

            ControllerPropities controllerPropities = GetControllerPropities();
            if (controllerPropities.projectroHourRate != 0)
            {
                Random random = new Random();
                var controller = controllerConnection;

                NameValueCollection props = new NameValueCollection { { "quartz.serializer.type", "binary" } };
                StdSchedulerFactory factory = new StdSchedulerFactory(props);
                IScheduler scheduler = await factory.GetScheduler();
                await scheduler.Start();

                JobDataMap keyValuePairs = new JobDataMap();
                keyValuePairs.Add("controllerConnection", controller);
                keyValuePairs.Add("random", random);


                IJobDetail job = JobBuilder.Create<LampScheldue>()
                    .UsingJobData(keyValuePairs)
                    .Build();


                ITrigger trigger = TriggerBuilder.Create()
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(3600 / controllerPropities.projectroHourRate)
                        .RepeatForever())
                    .Build();

                await scheduler.ScheduleJob(job, trigger);

            }
        }


    }
}
