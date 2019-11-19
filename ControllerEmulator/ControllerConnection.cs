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

        private Propities propities = new Propities();
        NetworkStream networkStream;
        readonly TcpClient client;
        public ControllerConnection()
        {
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

                Console.WriteLine(e.Message);
                return null;
            }
        }

        public void CloseConnection(ControllerConnection controllerConnection)
        {
            controllerConnection.client.Close();
        }

        public async Task Send(string message)
        {
            var controllerConnection = this;
            if (networkStream == null)
                GetNetworkStream(controllerConnection);
            byte[] byteMessage = MessageToByte(message);
            await networkStream.WriteAsync(byteMessage, 0, byteMessage.Length);
        }

        public void LisenServer(object controllerConnection)
        {
            //try
            //{ 
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
            //}
            /*catch (Exception e)
            {
                EventArgs eventArgs = new EventArgs();
                On_Exception.Invoke(e, eventArgs);
                Console.WriteLine(e.Message);
            }*/

        }

        public string ReadRecive()
        {
            NetworkStream networkStream = this.networkStream;
            byte[] byteRecive = new byte[256];
            int bytes = networkStream.Read(byteRecive, 0, byteRecive.Length);
            return ASCIIEncoding.ASCII.GetString(byteRecive, 0, bytes);
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
