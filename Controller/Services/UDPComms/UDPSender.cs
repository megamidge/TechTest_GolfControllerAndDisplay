using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Controller.Services.UDPComms
{
    internal class UDPSender
    {
        private const int Port = 19016;
        private const string Address = "255.255.255.255";

        private UdpClient _sender;
        private IPEndPoint _endPoint;


        public UDPSender()
        {
            // Setup a sender 
            _sender = new UdpClient(AddressFamily.InterNetwork);
            var broadcastAddress = IPAddress.Parse(Address);
            _endPoint = new IPEndPoint(broadcastAddress, Port);
        }

        public void SendMessage<TMessage>(string messageType, TMessage message)
        {
            if (message is null) throw new ArgumentNullException("Cannot send a null message.");
            var messageJson = JsonSerializer.Serialize(message); // Serialize the message
            var packet = new Packet(messageType, messageJson); // Bang the json into a packet
            var packetJson = JsonSerializer.Serialize(packet); // Serialise the packet
            var bytes = Encoding.ASCII.GetBytes(packetJson); // Get the packet bytes
            _sender.Send(bytes, bytes.Length, _endPoint); // Send the packet to the _endPoint
        }
    }
}
