using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ControllerEmulator.Scheldues
{
    class ErrorSimulateScheldue : IJob
    {

        public async Task Execute(IJobExecutionContext context)
        {
            

            JobDataMap dataMap = context.MergedJobDataMap;

            ControllerConnection controllerConnection = (ControllerConnection)dataMap.Get("controllerConnection");
            Random random = (Random) dataMap.Get("random");


                ControllerCommands.RandomError(controllerConnection, random);
        }
    }
}
