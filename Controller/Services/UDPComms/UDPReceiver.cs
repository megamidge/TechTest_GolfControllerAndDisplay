using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace Controller.Services.UDPComms
{

    internal class MessageEventArgs : EventArgs
    {

        public dynamic Message { get; private set; }

        public MessageEventArgs(dynamic message)
        {
            Message = message;
        }
    }
    internal class UDPReceiver
    {
        private const int Port = 19016;
        private const string Address = "255.255.255.255";

        private UdpClient _receiver;
        private IPEndPoint _endPoint;

        private Thread? receiverThread = null;

        private bool _stop = false;

        public event EventHandler<MessageEventArgs> MessageReceived;

        public UDPReceiver()
        {
            _receiver = new UdpClient(Port);
            _endPoint = new IPEndPoint(IPAddress.Any, Port);
        }

        public void Start()
        {
            if (receiverThread is not null) return;
            receiverThread = new Thread(Receive);
            receiverThread.Start();
        }

        private void Receive()
        {
            try
            {
                while (!_stop)
                {
                    Console.WriteLine("Awaiting UDP Message.");
                    var bytes = _receiver.Receive(ref _endPoint);

                    Console.WriteLine("Message received.");

                    var messageString = Encoding.ASCII.GetString(bytes);
                    Console.WriteLine($"    {messageString}");
                    var packet = JsonSerializer.Deserialize<Packet>(messageString);
                    dynamic messageData;
                    switch (packet.ContentType) {
                        case "helloworld":
                        default:
                            messageData = JsonSerializer.Deserialize<string>(packet.Content);
                            break;
                    }

                    InvokeMessageReceived(messageData);
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                _receiver.Close();
            }
        }

        private void InvokeMessageReceived(dynamic message)
        {
            MessageReceived?.Invoke(this, new MessageEventArgs(message));
        }

        public void Stop()
        {
            try
            {
                Console.WriteLine("Stopped Receiving.");
                _stop = true;
                _receiver?.Close(); // Close the receiver first!
                receiverThread?.Join(); // Joins the thread with the current, thus waiting for thread to stop
                receiverThread = null;
            }
            catch
            {
                // Failed to close - perhaps look into strategies/what actually happens to see what can be done.
                // Most times it'll be that the socket is already closed, so we're all good.
            }
        }
    }
}
