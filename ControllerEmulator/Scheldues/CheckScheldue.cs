<<<<<<< HEAD:ControllerEmulator/Scheldues/CheckScheldue.cs
﻿using Quartz;
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
            ControllerCommands.Check(controllerConnection);
        }
    }
}
=======
﻿using Quartz;
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

            ControllerCommands.Check(controllerConnection);
        }
    }
}
>>>>>>> ebae88adec8ba85ce51d7ee12f8038ccf660d0c0:ControllerEmulator/ScheduleJob.cs
