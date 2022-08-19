using Controller.Messages;
using Controller.Messages.Enums;
using Controller.Services.UDPComms;
using System.Windows;

namespace Controller
{
    /// <summary>
    /// Interaction logic for ControllerWindow.xaml
    /// </summary>
    public partial class ControllerWindow : Window
    {
        private UDPSender _sender;

        private int _score = 0;
        private int _shots = 0;

        public ControllerWindow()
        {
            InitializeComponent();
            _sender = new();

            // Set up click handlers. Using Commands would also work for sake of being able to bind actions. Would be a better solution for something going into production so as to decouple the UI from the actions that can be taken.
            // For sake of this task, this is fine. No need to over-engineer.
            btnHelloWorld.Click += BtnHelloWorld_Click;

            btnBallInHole.Click += BtnBallInHole_Click;
            btnBallOnTee.Click += BtnBallOnTee_Click;

            btnSuperTubeActivated.Click += BtnSuperTubeActivated_Click;
            btnHazardActivated.Click += BtnHazardActivated_Click;

            btnIncrementScore.Click += BtnIncrementScore_Click;
            btnDecrementScore.Click += BtnDecrementScore_Click;
            btnIncrementShots.Click += BtnIncrementShots_Click;
            btnResetShots.Click += BtnResetShots_Click;
        }

        private void BtnResetShots_Click(object sender, RoutedEventArgs e)
        {
            _shots = 0;
            SendStatusUpdateMessage();
        }

        private void BtnIncrementShots_Click(object sender, RoutedEventArgs e)
        {
            _shots++;
            SendStatusUpdateMessage();
        }

        private void BtnDecrementScore_Click(object sender, RoutedEventArgs e)
        {
            _score--;
            SendStatusUpdateMessage();

        }

        private void BtnIncrementScore_Click(object sender, RoutedEventArgs e)
        {
            _score++;
            SendStatusUpdateMessage();
        }

        private void BtnHazardActivated_Click(object sender, RoutedEventArgs e)
        {
            SendFeatureMessage(Feature.Hazard);
        }

        private void BtnSuperTubeActivated_Click(object sender, RoutedEventArgs e)
        {
            SendFeatureMessage(Feature.SuperTube);
        }

        private void BtnBallOnTee_Click(object sender, RoutedEventArgs e)
        {
            SendLocationMessage(BallLocations.Tee);
        }

        private void BtnBallInHole_Click(object sender, RoutedEventArgs e)
        {
            SendLocationMessage(BallLocations.Hole);
        }

        private void BtnHelloWorld_Click(object sender, RoutedEventArgs e)
        {
            _sender.SendMessage(MessageTypes.HelloWorld, "Hello World!");
        }

        private void SendLocationMessage(BallLocations ballLocation)
        {
            _sender.SendMessage(MessageTypes.BallLocation, new BallLocation
            {
                Location = ballLocation,
                Score = _score,
                Shots = _shots
            });
        }

        private void SendFeatureMessage(Feature feature)
        {
            _sender.SendMessage(MessageTypes.Feature, new FeatureActivated
            {
                Feature = feature,
                Score = _score,
                Shots = _shots
            });
        }

        private void SendStatusUpdateMessage()
        {
            _sender.SendMessage(MessageTypes.Status, new StatusUpdate
            {
                Score = _score,
                Shots = _shots
            });
        }
    }
}
