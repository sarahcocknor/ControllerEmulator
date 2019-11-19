using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerEmulator
{
    class ControllerPropities
    {
        public string token { get; set; }

        public uint deviceTvCount { get; set; }

        public uint deviceProjectorCount { get; set; }

        public int fludInterval { get; set; }

        public int checkInterval { get; set; }

        public int errorRate { get; set; }

        public int projectorHourRate { get; set; }

        public int reconectTimeOut { get; set; }

    }
}
