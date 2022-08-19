using System.Windows;

namespace Controller
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // Start required windows (not using startup uri so as to better control startup windows.

            var controllerWindow = new ControllerWindow();
            controllerWindow.Show();

            //var receiverTestWindow = new ReceiverTestWindow();
            //receiverTestWindow.Show();
        }
    }
}
