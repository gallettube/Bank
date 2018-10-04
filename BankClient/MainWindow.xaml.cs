using System.Windows;
using BankClient.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using BankClient;
using BankClient.View;

namespace BankClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
            Messenger.Default.Register<ViewModelMessage>(this, OnReceiveMessage);
        }

        private void OnReceiveMessage(ViewModelMessage msg)
        {
            if (msg.Message == ViewModelMessage.Message_OpenDialog)
            {
              
                if (msg.Dialog == ViewModelMessage.Dialog_LoginFailed)
                {
                    MessageBox.Show("Login failed. Invalid Username or Password.");
                }
                else if (msg.Dialog == ViewModelMessage.Dialog_Success)
                {
                    MessageBox.Show("Operation completed, you may now login.");
                }
                else if (msg.Dialog == ViewModelMessage.Dialog_Failed)
                {
                    MessageBox.Show("Operation failed!");
                }
            }
        }
    }
}