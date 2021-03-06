﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ControllerEmulator
{
    class GlobalPropities
    {

        [JsonProperty(Order = -3)]
        public ErrorCode errorCode { get; set; }

        [JsonProperty(Order = -5)]
        public string deviceId { get; set; }

        [JsonProperty(Order = -4)]
        public bool status { get; set; }

        [JsonProperty(Order = -2)]
        public string errorMessage { get; set; }


        public enum ErrorCode
        {
            None = 0,
            OK = 200,
            Info = 300,
            Warning = 400,
            Critical = 500
        }

    }
}
