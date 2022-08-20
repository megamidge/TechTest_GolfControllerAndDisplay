using Assets._Scripts;
using Assets._Scripts.Messages;
using Newtonsoft.Json;
using UnityEngine;

/// <summary>
/// Handle receiving golf messages and handling them, updating the game state as required.
/// </summary>
public class GolfMessageReceiver : MonoBehaviour
{
    private UDPReceiver _receiver;

    // Start is called before the first frame update
    void Start()
    {
        _receiver = new UDPReceiver();
        _receiver.Start();
    }

    private void OnDestroy()
    {
        _receiver.Stop();
        _receiver = null;
    }

    // Update is called once per frame
    void Update()
    {
        lock (_receiver.ReceivedPackets)
        {

            while (_receiver.ReceivedPackets.Count > 0)
            {
                Packet packet = _receiver.ReceivedPackets.Dequeue();
                Debug.Log("PACKET!" + packet.ContentType);
                switch (packet.ContentType) {
                    case MessageTypes.Status:
                        var statusUpdate = JsonConvert.DeserializeObject<StatusUpdate>(packet.Content);
                        GolfManager.Instance.UpdateCommon(statusUpdate);
                        break;
                    case MessageTypes.Feature:
                        var featureActivated = JsonConvert.DeserializeObject<FeatureActivated>(packet.Content);
                        GolfManager.Instance.UpdateCommon(featureActivated);
                        GolfManager.Instance.HandleGolfAction(featureActivated);
                        break;
                    case MessageTypes.BallLocation:
                        var ballLocation = JsonConvert.DeserializeObject<BallLocation>(packet.Content);
                        
                        GolfManager.Instance.UpdateCommon(ballLocation);
                        GolfManager.Instance.HandleGolfAction(ballLocation);
                        break;
                    case MessageTypes.HelloWorld:
                        //Debug.Log($"{JsonUtility.FromJson<string>(packet.Content)}");
                        GolfManager.Instance.HandleHelloWorld();
                        break;
                    default:
                        Debug.Log($"Unrecognised packet: {packet.ContentType} - {packet.Content}");
                        break;
                }

            }
        }
    }
}
