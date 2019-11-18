<<<<<<< HEAD
﻿using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ControllerEmulator.Scheldues
{
    class LampScheldue : IJob
    {

        public async Task Execute(IJobExecutionContext context)
        {
            //here hour gen

            JobDataMap dataMap = context.MergedJobDataMap;

            ControllerConnection controllerConnection = (ControllerConnection)dataMap.Get("controllerConnection");

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

namespace ControllerEmulator.Scheldues
{
    class LampScheldue : IJob
    {

        public async Task Execute(IJobExecutionContext context)
        {
            //here hour gen

            JobDataMap dataMap = context.MergedJobDataMap;

            ControllerConnection controllerConnection = (ControllerConnection)dataMap.Get("controllerConnection");

            ControllerCommands.Check(controllerConnection);
        }
    }
}
>>>>>>> ebae88adec8ba85ce51d7ee12f8038ccf660d0c0
