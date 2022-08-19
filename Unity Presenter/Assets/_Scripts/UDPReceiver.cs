using Assets._Scripts;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class UDPReceiver
{
    private const int Port = 19016;

    private UdpClient _receiver;
    private IPEndPoint _endPoint;

    private Thread? _receiverThread;

    private bool _stop = false;

    private Queue<Packet> _receivedPackets;
    public Queue<Packet> ReceivedPackets => _receivedPackets;

    public UDPReceiver()
    {
        _receiver = new UdpClient(Port);
        _endPoint = new IPEndPoint(IPAddress.Any, Port);
        _receivedPackets = new Queue<Packet>();
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
                var bytes = _receiver.Receive(ref _endPoint);

                var messageString = Encoding.ASCII.GetString(bytes);
                Debug.Log(messageString);
                try
                {
                    var packet = JsonUtility.FromJson<Packet>(messageString);
                    EnqueuePacket(packet);
                }
                catch { }
            }
        }
        catch (SocketException ex)
        {
            Debug.LogError(ex.ToString());
        }
        finally
        {
            _receiver.Close();
        }
    }

    private void EnqueuePacket(Packet packet)
    {
        lock (_receivedPackets)
        {
            _receivedPackets.Enqueue(packet);
        }
    }

    public void Stop()
    {
        try
        {
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
