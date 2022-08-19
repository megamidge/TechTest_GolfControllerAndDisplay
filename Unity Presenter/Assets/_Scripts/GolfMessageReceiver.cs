using Assets._Scripts;
using Assets._Scripts.Messages;
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
                        var statusUpdate = JsonUtility.FromJson<StatusUpdate>(packet.Content);
                        GolfManager.Instance.UpdateCommon(statusUpdate);
                        break;
                    case MessageTypes.Feature:
                        var featureActivated = JsonUtility.FromJson<FeatureActivated>(packet.Content);
                        GolfManager.Instance.UpdateCommon(featureActivated);
                        break;
                    case MessageTypes.BallLocation:
                        var ballLocation = JsonUtility.FromJson<BallLocation>(packet.Content);
                        GolfManager.Instance.UpdateCommon(ballLocation);
                        //logString = $"Ball is in/on {ballLocation.Location}";
                        //if (ballLocation.Location == Messages.Enums.BallLocations.Hole)
                        //{
                        //    logString += $"\r\nHole in {_shots}";
                        //}
                        //_score = ballLocation.Score;
                        //_shots = ballLocation.Shots;
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
