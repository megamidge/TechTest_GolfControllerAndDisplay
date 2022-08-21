using WPFPresenter.Services.UDPComms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFPresenter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GolfMessageReceiver _receiver;

        private Dictionary<GolfManager.Animations, Uri> _pages;

        public MainWindow()
        {
            _receiver = new();
            InitializeComponent();
            _pages = new Dictionary<GolfManager.Animations, Uri>();
            AddAnimationToDictionary(GolfManager.Animations.HelloWorld, "Panels/HelloWorld.xaml");
            AddAnimationToDictionary(GolfManager.Animations.BallOnTee, "Panels/BallOnTee.xaml");
            AddAnimationToDictionary(GolfManager.Animations.BallInHole, "Panels/BallInHole.xaml");
            AddAnimationToDictionary(GolfManager.Animations.SuperTubeActivated, "Panels/SuperTubeActivated.xaml");
            AddAnimationToDictionary(GolfManager.Animations.HazardActivated, "Panels/HazardActivated.xaml");
            GolfManager.Instance.AnimationTriggered += AnimationTriggered;
        }

        private void AddAnimationToDictionary(GolfManager.Animations animation, string path)
        {
            _pages.Add(animation, new Uri(path, UriKind.Relative));
        }

        private void AnimationTriggered(object? sender, GolfManager.AnimationTriggeredEventArgs e)
        {
            // Set the source to null to remove whatever is already there - means the same animation can be re-triggered
            this.Dispatcher.Invoke(() =>
            {
                frameView.Source = null;
            });
            // Sleep for a short moment
            System.Threading.Thread.Sleep(10);
            // Set the new source
            Uri? pageUri;
            _pages.TryGetValue(e.Animation, out pageUri);
            this.Dispatcher.Invoke(() =>
            {
                frameView.Source = pageUri;
            });
        }
    }
}
