using Controller.Services.UDPComms;
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
using System.Windows.Shapes;

namespace Controller
{
    /// <summary>
    /// Interaction logic for ControllerWindow.xaml
    /// </summary>
    public partial class ControllerWindow : Window
    {
        private UDPSender _sender;
        public ControllerWindow()
        {
            InitializeComponent();
            _sender = new();
            btnHelloWorld.Click += BtnHelloWorld_Click;
        }

        private void BtnHelloWorld_Click(object sender, RoutedEventArgs e)
        {
            _sender.SendMessage("helloworld","Hello World!");
        }
    }
}
