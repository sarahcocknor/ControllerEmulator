using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ControllerEmulator.Scheldues
{
    class FullSendScheldue : IJob
    {

        public async Task Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.MergedJobDataMap;
            ControllerConnection controllerConnection = (ControllerConnection)dataMap.Get("controllerConnection");
            
                ControllerCommands.FullSend(controllerConnection);
        }
    }
}
