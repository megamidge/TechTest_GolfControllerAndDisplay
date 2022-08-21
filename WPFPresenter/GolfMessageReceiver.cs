using System;
using System.Text.Json;
using WPFPresenter.Messages;
using WPFPresenter.Services.UDPComms;

namespace WPFPresenter;
public class GolfMessageReceiver
{
    private UDPReceiver _receiver;

    public GolfMessageReceiver()
    {
        _receiver = new UDPReceiver();
        _receiver.Start();

        _receiver.MessageReceived += MessageReceived;
    }

    private void MessageReceived(object? sender, MessageEventArgs e)
    {
        switch (e.Packet.ContentType) {
            case MessageTypes.Status:
                var statusUpdate = JsonSerializer.Deserialize<StatusUpdate>(e.Packet.Content);
                GolfManager.Instance.UpdateCommon(statusUpdate);
                break;
            case MessageTypes.Feature:
                var featureActivated = JsonSerializer.Deserialize<FeatureActivated>(e.Packet.Content);
                GolfManager.Instance.UpdateCommon(featureActivated);
                GolfManager.Instance.HandleGolfAction(featureActivated);
                break;
            case MessageTypes.BallLocation:
                var ballLocation = JsonSerializer.Deserialize<BallLocation>(e.Packet.Content);
                GolfManager.Instance.UpdateCommon(ballLocation);
                GolfManager.Instance.HandleGolfAction(ballLocation);
                break;
            case MessageTypes.HelloWorld:
                GolfManager.Instance.HandleHelloWorld();
                break;
        }

    }
}
