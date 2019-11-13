using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ControllerEmulator
{
    class ScheduleJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
  
            JobDataMap dataMap = context.MergedJobDataMap;

            ControllerConnection controllerConnection = (ControllerConnection) dataMap.Get("controllerConnection");

            ControllerCommands.Check(controllerConnection);
        }
    }
}
