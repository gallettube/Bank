using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using BankClient.BankOrder;
using System;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;

namespace BankClient.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class LoginViewModel : ViewModelBase
    {
        private User user;
        private Guid guid;

        public ICommand LoginCommand { get; set; }
        public ICommand IsRegisterCommand { get; set; }

        public string LoginLabel { get; set; }
        public string UserName
        {
            get
            {
                return user.UserName;
            }
            set
            {
                user.UserName = value;
                RaisePropertyChanged("UserName");
            }
        }


        public string Password;

        public ICommand PasswordChangedCommand
        {
            get
            {
                return new RelayCommand<object>(ExecChangePassword);
            }
        }

        private void ExecChangePassword(object obj)
        {
            Password = ((System.Windows.Controls.PasswordBox)obj).Password;
            user.Password = Password;
        }


        public LoginViewModel()
        {
            this.user = new User();
            LoginCommand = new RelayCommand(LoginUser, () => { return LoginLabel == "Login"; });
            IsRegisterCommand = new RelayCommand(IsRegisteredUser);
            LoginLabel = "Login";
        }


        
        private async void IsRegisteredUser()
        {
            

            LoginLabel = "Logging in...";
            RaisePropertyChanged("LoginLabel");
            ((RelayCommand)IsRegisterCommand).RaiseCanExecuteChanged();

            BankOrderClient client = new BankOrderClient();
            var loginGuid = await client.LoginAsync(user);
            guid = loginGuid;
            client.Close();

            LoginLabel = "Login";
            RaisePropertyChanged("LoginLabel");
            ((RelayCommand)IsRegisterCommand).RaiseCanExecuteChanged();

            // if login succeeded and we got a valid guid
            if (guid != new Guid())
            {
                Messenger.Default.Send(new ViewModelMessage
                {
                    Guid = guid,
                    UserName = UserName,
                    Message = ViewModelMessage.Message_Navigate,
                    NavigateTo = ViewModelMessage.Navigation_Customer
                });

                UserName = "";
                Password = "";
            }
            else
            {
                Messenger.Default.Send(new ViewModelMessage
                {
                    Message = ViewModelMessage.Message_OpenDialog,
                    Dialog = ViewModelMessage.Dialog_LoginFailed
                });
            }
        }
        

        /*
        private async void RegisterUser()
        {
            BankOrderClient client = new BankOrderClient();
            var success = await client.RegisterAsync(UserName, Password, "NYI");
            client.Close();

            if (success)
            {
                Messenger.Default.Send(new ViewModelMessage
                {
                    Message = ViewModelMessage.Message_OpenDialog,
                    Dialog = ViewModelMessage.Dialog_RegistrationSuccess
                });
            }
            else
            {
                Messenger.Default.Send(new ViewModelMessage
                {
                    Message = ViewModelMessage.Message_OpenDialog,
                    Dialog = ViewModelMessage.Dialog_RegistrationFailed
                });
            }
        }*/

        private async void LoginUser()
        {
            LoginLabel = "Logging in...";
            RaisePropertyChanged("LoginLabel");
            ((RelayCommand)LoginCommand).RaiseCanExecuteChanged();

            BankOrderClient client = new BankOrderClient();
            var loginGuid = await client.LoginAsync(user);
            guid = loginGuid;
            client.Close();

            LoginLabel = "Login";
            RaisePropertyChanged("LoginLabel");
            ((RelayCommand)LoginCommand).RaiseCanExecuteChanged();

            // if login succeeded and we got a valid guid
            if (guid != new Guid())
            {
                Messenger.Default.Send(new ViewModelMessage
                {
                    Guid = guid,
                    UserName = UserName,
                    Message = ViewModelMessage.Message_Navigate,
                    NavigateTo = ViewModelMessage.Navigation_AccountDetails
                });
                UserName = "";
                Password = "";
            }
            else
            {
                Messenger.Default.Send(new ViewModelMessage
                {
                    Message = ViewModelMessage.Message_OpenDialog,
                    Dialog = ViewModelMessage.Dialog_LoginFailed
                });
            }
        }
    }
}