using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerEmulator
{
    class ProjectorDevicePropities : GlobalPropities
    {

        [JsonProperty(Order = -1)]
        public int lamphours { get; set; }

        [JsonProperty(Order = 0)]
        public LampStatus lampStatus { get; set; }

        [JsonProperty(Order = 1)]
        public Input input { get; set; }

        [JsonProperty(Order = 2)]
        public Power power { get; set; }

        public enum LampStatus
        {
            None = 0,
            On = 1,
            Off = 2,
            Cooling = 3,
            Heating = 4
        }

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
        public int _lamphours
        {
            get
            {
                return lamphours;
            }
            set
            {
                if (value > 0)
                    lamphours = value;
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
                case "lamphours":
                    this.lamphours = Convert.ToInt32(value);
                    break;
                case "lampstatus":
                    LampStatus _lampStatus = (LampStatus)Enum.Parse(typeof(LampStatus), value);
                    this.lampStatus = _lampStatus;
                    break;
                case "input":
                    Input _input = (Input)Enum.Parse(typeof(Input), value);
                    this.input = _input;
                    break;
                case "power":
                    Power _power = (Power)Enum.Parse(typeof(Power), value);
                    this.power = _power;
                    break;
                default:
                    Console.WriteLine(DateTime.Now.ToShortTimeString() + " (WARNING): Wrong parameter recived from server");
                    break;
            }


        }

        public static ProjectorDevicePropities ConvertFromJObject(object? currentObject)
        {
            JObject jObject = (JObject)currentObject;
            ProjectorDevicePropities device = new ProjectorDevicePropities();

            device.deviceId = jObject.GetValue("deviceId").ToString();
            device.status = Convert.ToBoolean(jObject.GetValue("status").ToString());
            device.errorCode = (ErrorCode)Enum.Parse(typeof(ErrorCode), jObject.GetValue("errorCode").ToString());
            device.errorMessage = jObject.GetValue("errorMessage").ToString();
            device._lamphours = Convert.ToInt32(jObject.GetValue("lamphours").ToString());
            device.lampStatus = (LampStatus)Enum.Parse(typeof(LampStatus), jObject.GetValue("lampStatus").ToString());
            device.input = (Input)Enum.Parse(typeof(Input), jObject.GetValue("input").ToString());
            device.power = (Power)Enum.Parse(typeof(Power), jObject.GetValue("power").ToString());

            return device;
        }
    }
}
