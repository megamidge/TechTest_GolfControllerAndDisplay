using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace WPFPresenter.Panels
{
    /// <summary>
    /// Interaction logic for ShotsAndScore.xaml
    /// </summary>
    public partial class ShotsAndScore : UserControl, INotifyPropertyChanged
    {
        public static readonly RoutedEvent ShotsChangedAnimationRequested = EventManager.RegisterRoutedEvent("ShotsChangedAnimationRequested",RoutingStrategy.Bubble,typeof(RoutedEventHandler),typeof(ShotsAndScore));
        public static readonly RoutedEvent ScoreChangedAnimationRequested = EventManager.RegisterRoutedEvent("ScoreChangedAnimationRequested", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ShotsAndScore));

        public string ScoreText => $"{GolfManager.Instance.Score} POINTS";
        public string ShotsText => $"{GolfManager.Instance.Shots} SHOTS";

        public ShotsAndScore()
        {


            InitializeComponent();

            GolfManager.Instance.PropertyChanged += Instance_SomethingChanged;

        }

        private void Instance_SomethingChanged(object? sender, PropertyChangedEventArgs e)
        {
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ScoreText)));
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShotsText)));

            // NotifyPropertyChanged makes sense, but is inexplicably slow. This however seems to be quicker
            this.Dispatcher.Invoke(() =>
            {
                if (e.PropertyName == nameof(GolfManager.Instance.Score))
                    lblScore.Content = ScoreText;
                if (e.PropertyName == nameof(GolfManager.Instance.Shots))
                    lblShots.Content = ShotsText;
            switch (e.PropertyName) {
                case nameof(GolfManager.Instance.Score):
                    ScoreGrid.RaiseEvent(new RoutedEventArgs(ShotsAndScore.ScoreChangedAnimationRequested, this));
                    break;
                case nameof(GolfManager.Instance.Shots):
                    ShotsGrid.RaiseEvent(new RoutedEventArgs(ShotsAndScore.ShotsChangedAnimationRequested, this));
                    break;
            }
            });



        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
