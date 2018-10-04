using BankClient;
using BankClient.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using System.Windows.Input;

namespace BankClient.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

        private ViewModelBase _currentViewModel;
        readonly static ViewModelLocator _locator = new ViewModelLocator();
        readonly static LoginViewModel _loginViewModel = _locator.LoginView;
        readonly static AccountDetailsViewModel _accountDetailsViewModel = _locator.AccountDetailsView;
        readonly static CustomerDetailsViewModel _customerViewModel = _locator.CustomerDetailsView;



        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                if (_currentViewModel == value)
                {
                    return;
                }
                _currentViewModel = value;
                RaisePropertyChanged("CurrentViewModel");
            }
        }

        public MainViewModel()
        {
            CurrentViewModel = MainViewModel._loginViewModel;
            Messenger.Default.Register<ViewModelMessage>(this, OnReceiveMessage);
        }

        private void OnReceiveMessage(ViewModelMessage msg)
        {
            if (msg.Message == ViewModelMessage.Message_Navigate)
            {
                if (msg.NavigateTo == ViewModelMessage.Navigation_AccountDetails)
                {
                    CurrentViewModel = MainViewModel._accountDetailsViewModel;
                    // En el mensaje pasa el parametro de usuario y de guid
                    Messenger.Default.Send(new ViewModelMessage
                    {
                        Message = ViewModelMessage.Message_LoadAccountList,
                        Guid = msg.Guid,
                        UserName = msg.UserName
                    });
                }
                else if (msg.NavigateTo == ViewModelMessage.Navigation_Login)
                {
                    CurrentViewModel = MainViewModel._loginViewModel;
                }
                else if (msg.NavigateTo == ViewModelMessage.Navigation_Customer)
                {
                    CurrentViewModel = MainViewModel._customerViewModel;
                }
            }
        }
    }
}