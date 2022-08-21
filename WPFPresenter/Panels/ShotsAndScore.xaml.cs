using System.ComponentModel;
using System.Windows.Controls;

namespace WPFPresenter.Panels
{
    /// <summary>
    /// Interaction logic for ShotsAndScore.xaml
    /// </summary>
    public partial class ShotsAndScore : UserControl, INotifyPropertyChanged
    {
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
            });
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
