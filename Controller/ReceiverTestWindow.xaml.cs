using Controller.Messages;
using Controller.Services.UDPComms;
using System.Text.Json;
using System.Windows;

namespace Controller
{
    /// <summary>
    /// Interaction logic for ReceiverTestWindow.xaml
    /// </summary>
    public partial class ReceiverTestWindow : Window
    {
        private UDPReceiver _receiver;
        private const string MessageSeparator = "- - - - - - - - - - - - - - - - - - - - - -\r\n";

        private int _score;
        private int _shots;

        public ReceiverTestWindow()
        {
            InitializeComponent();
            _receiver = new();
            _receiver.Start();
            _receiver.MessageReceived += GenericMessageReceived;
        }

        private void GenericMessageReceived(object? sender, MessageEventArgs e)
        {
            string logString = "";
            switch (e.Packet.ContentType)
            {
                case MessageTypes.Status:
                    var statusUpdate = JsonSerializer.Deserialize<StatusUpdate>(e.Packet.Content);
                    _score = statusUpdate.Score;
                    _shots = statusUpdate.Shots;
                    logString = $"Score: {_score}\r\nShots: {_shots}";
                    break;
                case MessageTypes.Feature:
                    var featureActivated = JsonSerializer.Deserialize<FeatureActivated>(e.Packet.Content);
                    logString = $"{featureActivated.Feature} activated.";
                    _score = featureActivated.Score;
                    _shots = featureActivated.Shots;
                    break;
                case MessageTypes.BallLocation:
                    var ballLocation = JsonSerializer.Deserialize<BallLocation>(e.Packet.Content);
                    logString = $"Ball is in/on {ballLocation.Location}";
                    if(ballLocation.Location == Messages.Enums.BallLocations.Hole)
                    {
                        logString += $"\r\nHole in {_shots}";
                    }
                    _score = ballLocation.Score;
                    _shots = ballLocation.Shots;
                    break;
                case MessageTypes.HelloWorld:
                    logString = JsonSerializer.Deserialize<string>(e.Packet.Content);
                    break;
                default:
                    logString = e.Packet.Content;
                    break;
            }
            // Have to dispatch the commands to the main thread!
            Application.Current.Dispatcher.Invoke(() =>
            {
                tbContent.Text += $"{MessageSeparator}{logString}\r\n";
                tbContent.ScrollToEnd();
            });
        }
    }
}
