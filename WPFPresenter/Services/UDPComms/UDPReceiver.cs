using WPFPresenter.Messages;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace WPFPresenter.Services.UDPComms
{

    internal class MessageEventArgs : EventArgs
    {

        public Packet Packet { get; init; }

        public MessageEventArgs(Packet packet)
        {
            Packet = packet;
        }
    }
    internal class UDPReceiver
    {
        private const int Port = 19016;

        private UdpClient _receiver;
        private IPEndPoint _endPoint;

        private Thread? _receiverThread = null;

        private bool _stop = false;

        /// <summary>
        /// Invoked for all messages, but spits out a 'dynamic' type.
        /// </summary>
        public event EventHandler<MessageEventArgs> MessageReceived;

        public UDPReceiver()
        {
            _receiver = new UdpClient(Port);
            _endPoint = new IPEndPoint(IPAddress.Any, Port);
        }

        public void Start()
        {
            if (_receiverThread is not null) return;
            _receiverThread = new Thread(Receive);
            _receiverThread.Start();
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
                    try
                    {

                        var packet = JsonSerializer.Deserialize<Packet>(messageString);
                        InvokeMessageReceived(packet);
                    }
                    catch (JsonException)
                    {
                        // Json Exception happened, ignore and carry on
                    }
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

        private void InvokeMessageReceived(Packet packet)
        {
            MessageReceived?.Invoke(this, new MessageEventArgs(packet));
        }

        public void Stop()
        {
            try
            {
                Console.WriteLine("Stopped Receiving.");
                _stop = true;
                _receiver?.Close(); // Close the receiver first!
                _receiverThread?.Join(); // Joins the thread with the current, thus waiting for thread to stop
                _receiverThread = null;
            }
            catch
            {
                // Failed to close - perhaps look into strategies/what actually happens to see what can be done.
                // Most times it'll be that the socket is already closed, so we're all good.
            }
        }
    }
}
