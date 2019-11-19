# ControllerEmulator

All settings contains in /propities folder:

That contains:
  * connect.json;
  * controller.json;
  * projector.json;
  * tv.json;

### /propities/connect.json:
  **ip**: ip address for server;
  
  **port**: port;
  
  Example: 
    ```[{
    "ip":"172.27.69.140",
    "port":8002
    }]```
  
/propities/controller.json:
  token: controller token;
  deviceTvCount: count of TV devices;
  deviceProjectorCount: count of Projector devices;
  fludInterval: value, which means the periodic time in seconds, 
                which sets the interval for sending the entire list of devices to the server. 
                Аt zero value, no sending;
  checkInterval: same that fludInterval for <check status> messages;
  errorRate: random error rate. Value in seconds. At zero value, no sending;
  projectorHourRate: value, which means time multiplay for projectors lamps. 
                     At 1 value, hour = hour. At 3600 value, hour = second, etc;
  reconnectTimeOut: Wait time after disconect in seconds. Tested with value 40.
  
  Example: 
    ```[{
    "token":"2db09b37-2f56-47ce-5722-08d761eef482",
    "deviceTvCount":3,
    "deviceProjectorCount":2,
    "fludInterval":10,
    "checkInterval":0,
    "errorRate":5,
    "projectorHourRate":360,
    "reconectTimeOut":40
    }]```
    
/propities/projector.json:
  deviceId: deviceId. When file is generated it first start equals "device-id-here".
            If file contains "device-id-here" value. In console log will arise warnings.
            For emulator works at least one deviceId must be valid.
  
  The rest of the properties are completely repeated by the WIKI.
  
  Example:
    ```[{
    "deviceId":"780c8y08-a997-466e-b0d9-08d76c0d8a69",
    "status":true,
    "errorCode":0,
    "errorMessage":"ok",
    "lamphours":50,
    "lampStatus":1,
    "input":6,
    "power":1
    }]```
    
/propities/tv.json:
  Same that projector.json, but for TVs.
  
  Example:
    ```[{
    "deviceId":"258d607d-beaf-4c74-19da-08d32391270f",
    "status":true,
    "errorCode":0,
    "errorMessage":"ok",
    "input":6,
    "power":1,
    "volume":50,
    "volumeMute":false
    }]```
