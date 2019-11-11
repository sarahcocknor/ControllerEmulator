using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ControllerEmulator
{
    class ControllerConnection
    {

        NetworkStream networkStream;
        TcpClient client;
        public ControllerConnection()
        {
            client = Connect();
        }

        private TcpClient Connect()
        {
            Propities connection = new Propities();
            ConnectionPropities connectionPropities = connection.GetConnect();

            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect(connectionPropities.ip, connectionPropities.port);
            return tcpClient;
        }

        public void CloseConnection(ControllerConnection controllerConnection)
        {
            controllerConnection.client.Close();
        }

        public async void Send(string message)
        {
            var controllerConnection = this;
            if (networkStream == null)
                GetNetworkStream(controllerConnection);
            byte[] byteMessage = MessageToByte(message);
            await networkStream.WriteAsync(byteMessage, 0, byteMessage.Length);

        }

        public async void Recive()
        {
            NetworkStream networkStream = this.networkStream;
            byte[] byteRecive = new byte[256];
            int bytes = await networkStream.ReadAsync(byteRecive, 0, byteRecive.Length);
            Console.WriteLine(ASCIIEncoding.ASCII.GetString(byteRecive, 0, bytes));
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
