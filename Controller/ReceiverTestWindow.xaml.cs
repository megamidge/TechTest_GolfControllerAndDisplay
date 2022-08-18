using Controller.Services.UDPComms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Controller
{
    /// <summary>
    /// Interaction logic for ReceiverTestWindow.xaml
    /// </summary>
    public partial class ReceiverTestWindow : Window
    {
        private UDPReceiver _receiver;
        public ReceiverTestWindow()
        {
            InitializeComponent();
            _receiver = new();
            _receiver.Start();
            _receiver.MessageReceived += MessageReceived;
        }

        private void MessageReceived(object? sender, MessageEventArgs e)
        {
            // Have to dispatch the commands to the main thread!
            
            Application.Current.Dispatcher.Invoke(() =>
            {
                tbContent.Text += e.Message + "\r\n";
            });
        }
    }
}
