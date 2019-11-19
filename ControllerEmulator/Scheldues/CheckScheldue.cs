using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ControllerEmulator
{
    class CheckScheldue : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        { 
            JobDataMap dataMap = context.MergedJobDataMap;
            ControllerConnection controllerConnection = (ControllerConnection) dataMap.Get("controllerConnection");

            /*
            if (controllerConnection.reconect)
            {
                IScheduler schelduler = (IScheduler)dataMap.Get("schelduler");
                Propities.PauseScheldue(schelduler);
            } else*/
                ControllerCommands.Check(controllerConnection);


        }
    }
}
