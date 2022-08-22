using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace WPFPresenter.Panels
{
    /// <summary>
    /// Interaction logic for BallOnTee.xaml
    /// </summary>
    public partial class BallInHole : Page
    {
        public string HoleInX => $"HOLE IN {GolfManager.Instance.Shots}";

        public string ShotsMessage
        {
            get
            {
                bool valueExists = _shotsMessages.TryGetValue(GolfManager.Instance.Shots, out string message);
                return valueExists ? message : _shotsMessages.Last().Value;
            }
        }


        private Dictionary<int, string> _shotsMessages;


        public BallInHole()
        {
            _shotsMessages = new Dictionary<int, string>();
            _shotsMessages.Add(0, "Straight outta Hogwarts!");
            _shotsMessages.Add(1, "Babsolute Belter!");
            _shotsMessages.Add(2, "Nicely done!");
            _shotsMessages.Add(3, "That'll do nicely!");
            _shotsMessages.Add(4, "Respectable.");
            _shotsMessages.Add(5, "Number 5 is alive!");
            _shotsMessages.Add(6, "Took your time...");
            _shotsMessages.Add(7, "It's about the journey, not the destination...");

            InitializeComponent();
        }
    }
}
