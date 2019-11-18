<<<<<<< HEAD:ControllerEmulator/Devices/TVDevicePropities.cs
﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ControllerEmulator
{

    [JsonObject]
    class TVDevicePropities : GlobalPropities
    {
        [JsonProperty(Order = 1)]
        public int volume { get; set; }

        [JsonProperty(Order = 2)]
        public bool volumeMute { get; set; }

        [JsonProperty(Order = -1)]
        public Input input { get; set; }

        [JsonProperty(Order = 0)]
        public Power power { get; set; }

        public enum Input
        {
            None = 0,
            HDMI1 = 1,
            HDMI2 = 2,
            HDMI3 = 3,
            HDMI4 = 4,
            HDMI5 = 5,
            DVI1 = 6,
            DVI2 = 7,
            DVI3 = 8,
            DVI4 = 9,
            DVI5 = 10,
            VGA1 = 11,
            VGA2 = 12,
            RCA1 = 13,
            RCA2 = 14,
            SVIDEO1 = 15,
            SVIDEO2 = 16,
            HDBT1 = 17,
            HDBT2 = 18
        }

        public enum Power
        {
            False = 0,
            True = 1

        }

        [JsonIgnore]
        public int _volume
        {
            get
            {
                return volume;
            }
            set
            {
                if (value >= 0 && value <= 100)
                    volume = value;
            }
        }

        public void ParamChange(string param, string value)
        {
            switch (param)
            {
                case "status":
                    this.status = Convert.ToBoolean(value);
                    break;
                case "errorcode":
                    ErrorCode _errorCode = (ErrorCode)Enum.Parse(typeof(ErrorCode), value);
                    this.errorCode = _errorCode;
                    break;
                case "errormessage":
                    this.errorMessage = value;
                    break;
                case "input":
                    Input _input = (Input)Enum.Parse(typeof(Input), value);
                    this.input = _input;
                    break;
                case "power":
                    Power _power = (Power)Enum.Parse(typeof(Power), value);
                    this.power = _power;
                    break;
                case "volume":
                    if (value == "up")
                        this.volume += 1;
                    if (value == "down")
                        this.volume -= 1;
                    break;
                case "volumemute":
                    this.volumeMute = Convert.ToBoolean(value);
                    break;
                default:
                    Console.WriteLine("wrong parameter recived");
                    break;
            }
                 

        }

        public static TVDevicePropities ConvertFromJObject(object? currentObject)
        {
            JObject jObject = (JObject)currentObject;
            TVDevicePropities device = new TVDevicePropities();

            device.deviceId = jObject.GetValue("deviceId").ToString();
            device.status = Convert.ToBoolean( jObject.GetValue("status").ToString() );
            device.errorCode = (ErrorCode)Enum.Parse(typeof(ErrorCode), jObject.GetValue("errorCode").ToString());
            device.errorMessage = jObject.GetValue("errorMessage").ToString();
            device.input = (Input)Enum.Parse(typeof(Input), jObject.GetValue("input").ToString());
            device.power = (Power)Enum.Parse(typeof(Power), jObject.GetValue("power").ToString());
            device.volume = Convert.ToInt32(jObject.GetValue("volume").ToString());
            device.volumeMute = Convert.ToBoolean(jObject.GetValue("volumeMute").ToString());

            return device;
        }

    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ControllerEmulator
{

    [JsonObject]
    class TVDevicePropities : GlobalPropities
    {
        [JsonProperty(Order = 1)]
        public int volume { get; set; }

        [JsonProperty(Order = 2)]
        public bool volumeMute { get; set; }

        [JsonProperty(Order = -1)]
        public Input input { get; set; }

        [JsonProperty(Order = 0)]
        public Power power { get; set; }

        public enum Input
        {
            None = 0,
            HDMI1 = 1,
            HDMI2 = 2,
            HDMI3 = 3,
            HDMI4 = 4,
            HDMI5 = 5,
            DVI1 = 6,
            DVI2 = 7,
            DVI3 = 8,
            DVI4 = 9,
            DVI5 = 10,
            VGA1 = 11,
            VGA2 = 12,
            RCA1 = 13,
            RCA2 = 14,
            SVIDEO1 = 15,
            SVIDEO2 = 16,
            HDBT1 = 17,
            HDBT2 = 18
        }

        public enum Power
        {
            False = 0,
            True = 1

        }

        [JsonIgnore]
        public int _volume
        {
            get
            {
                return volume;
            }
            set
            {
                if (value >= 0 && value <= 100)
                    volume = value;
            }
        }

        public void ParamChange(string param, string value)
        {
            switch (param)
            {
                case "status":
                    this.status = Convert.ToBoolean(value);
                    break;
                case "errorcode":
                    ErrorCode _errorCode = (ErrorCode)Enum.Parse(typeof(ErrorCode), value);
                    this.errorCode = _errorCode;
                    break;
                case "errormessage":
                    this.errorMessage = value;
                    break;
                case "input":
                    Input _input = (Input)Enum.Parse(typeof(Input), value);
                    this.input = _input;
                    break;
                case "power":
                    Power _power = (Power)Enum.Parse(typeof(Power), value);
                    this.power = _power;
                    break;
                case "volume":
                    if (value == "up")
                        this.volume += 1;
                    if (value == "down")
                        this.volume -= 1;
                    break;
                case "volumemute":
                    this.volumeMute = Convert.ToBoolean(value);
                    break;
                default:
                    Console.WriteLine("wrong parameter recived");
                    break;
            }
                 

        }

        public static TVDevicePropities ConvertFromJObject(object? currentObject)
        {
            JObject jObject = (JObject)currentObject;
            TVDevicePropities device = new TVDevicePropities();

            device.deviceId = jObject.GetValue("deviceId").ToString();
            device.status = Convert.ToBoolean( jObject.GetValue("status").ToString() );
            device.errorCode = (ErrorCode)Enum.Parse(typeof(ErrorCode), jObject.GetValue("errorCode").ToString());
            device.errorMessage = jObject.GetValue("errorMessage").ToString();
            device.input = (Input)Enum.Parse(typeof(Input), jObject.GetValue("input").ToString());
            device.power = (Power)Enum.Parse(typeof(Power), jObject.GetValue("power").ToString());
            device.volume = Convert.ToInt32(jObject.GetValue("volume").ToString());
            device.volumeMute = Convert.ToBoolean(jObject.GetValue("volumeMute").ToString());

            return device;
        }

    }
}
>>>>>>> ebae88adec8ba85ce51d7ee12f8038ccf660d0c0:ControllerEmulator/TVDevicePropities.cs
