using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Quartz;
using System.Collections.Specialized;
using Quartz.Impl;
using ControllerEmulator.Scheldues;

namespace ControllerEmulator
{
    class ControllerConnection
    {
 

        public event EventHandler<string> On_Messege;
        public event EventHandler On_Exception;

        public bool reconect = false;
        private Propities propities = new Propities();
        public NetworkStream networkStream;
        readonly TcpClient client = null;
        public ControllerConnection()
        {
            if (client != null)
                client.Close();


            client = Connect();
        }

        private TcpClient Connect()
        {

            try
            {
                Propities propities = new Propities();
                ConnectionPropities connectionPropities = propities.GetConnect();

                TcpClient tcpClient = new TcpClient();
                tcpClient.Connect(connectionPropities.ip, connectionPropities.port);

                return tcpClient;
            }
            catch (Exception e)
            {
                EventArgs eventArgs = new EventArgs();
                On_Exception.Invoke(e, eventArgs);
                //Console.WriteLine(e.Message);
                return null;
            }
        }

        public void CloseConnection()
        {
            if (client != null)
                this.client.Close();
            if (networkStream != null)
                networkStream.Close();
        }

        public async Task Send(string message)
        {
            var controllerConnection = this;
            

            if (networkStream == null)
                GetNetworkStream(controllerConnection);
            byte[] byteMessage = MessageToByte(message);
            //debug
            Console.WriteLine(DateTime.Now.ToShortTimeString() + " (CLIENT): " + message);
            
            //debug
            await networkStream.WriteAsync(byteMessage, 0, byteMessage.Length);

        }

        public void LisenServer(object controllerConnection)
        {
            try
            { 
            propities.StartScheldue(controllerConnection);

            var controller = controllerConnection;
            while (true)
            {
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                byte[] data = new byte[265];

                do
                {
                    bytes = networkStream.Read(data, 0, data.Length);
                    builder.Append(Encoding.ASCII.GetString(data, 0, bytes));
                }
                while (networkStream.DataAvailable);

                On_Messege.Invoke(controller, builder.ToString());
                
            }
            }
            catch (Exception e)
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString() + " (CRITYCAL): Connection was aborted. Reconect in " + propities.GetControllerPropities().reconectTimeOut + "s"); 
                EventArgs eventArgs = new EventArgs();
                On_Exception.Invoke(e, eventArgs);
                //Console.WriteLine(e.Message);
            }

        }

        private void GetNetworkStream(ControllerConnection controllerConnection)
        {
            networkStream = controllerConnection.client.GetStream();
        }

        private byte[] MessageToByte(string message)
        {
            return System.Text.ASCIIEncoding.ASCII.GetBytes(message);
        }

        

    }
}
