using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using BankClient.BankOrder;
using System;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;

namespace BankClient.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class CustomerDetailsViewModel : ViewModelBase
    {
        public ICommand FindCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ListCommand { get; set; }

        private Guid Guid;

        private int userid;
        public int UserId
        {

            get
            {
                return userid;
            }
            set
            {
                userid = value;
                RaisePropertyChanged("UserId");
            }
        }

        private string username;
        public string UserName
        {

            get
            {
                return username;
            }
            set
            {
                username = value;
                RaisePropertyChanged("UserName");
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                RaisePropertyChanged("Password");
            }
        }

        private string emailaddress;
        public string EmailAddress
        {
            get
            {
                return emailaddress;
            }
            set
            {
                emailaddress = value;
                RaisePropertyChanged("EmailAddress");
            }
        }

        private string firstname;
        public string FirstName
        {
            get
            {
                return firstname;
            }
            set
            {
                firstname = value;
                RaisePropertyChanged("FirstName");
            }
        }

        private string lastname;
        public string LastName
        {
            get
            {
                return lastname;
            }
            set
            {
                lastname = value;
                RaisePropertyChanged("LastName");
            }
        }

        private string country;
        public string Country
        {
            get
            {
                return country;
            }
            set
            {
                country = value;
                RaisePropertyChanged("Country");
            }
        }


        private string region;
        public string Region
        {
            get
            {
                return region; 
            }
            set
            {
                region = value;
                RaisePropertyChanged("Region");
            }
        }

        private string city;
        public string City
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
                RaisePropertyChanged("City");
            }
        }

        private string address;
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
                RaisePropertyChanged("Address");
            }
        }



        public CustomerDetailsViewModel()
        {
            Guid = new Guid();

            FindCommand = new RelayCommand(FindUser);    
            RegisterCommand = new RelayCommand(RegisterUser);   
            UpdateCommand = new RelayCommand(UpdateUser); 
            DeleteCommand = new RelayCommand(DeleteUser);
            ListCommand = new RelayCommand(List);

            LogoutCommand = new RelayCommand(Logout);
        }


        private async void RegisterUser()
        {
            BankOrderClient client = new BankOrderClient();
            var success = await client.RegisterAsync(username, password, emailaddress, firstname, lastname, country, region, city, address);
            client.Close();

            if (success)
            {
                Messenger.Default.Send(new ViewModelMessage
                {
                    Message = ViewModelMessage.Message_OpenDialog,
                    Dialog = ViewModelMessage.Dialog_Success
                });
            }
            else
            {
                Messenger.Default.Send(new ViewModelMessage
                {
                    Message = ViewModelMessage.Message_OpenDialog,
                    Dialog = ViewModelMessage.Dialog_Failed
                });
            }
        }

        private async void UpdateUser()
        {
            BankOrderClient client = new BankOrderClient();
            var success = await client.UpdateAsync(username, password, emailaddress,  firstname, lastname, country, region, city, address);
            client.Close();

            if (success)
            {
                Messenger.Default.Send(new ViewModelMessage
                {
                    Message = ViewModelMessage.Message_OpenDialog,
                    Dialog = ViewModelMessage.Dialog_Success
                });
            }
            else
            {
                Messenger.Default.Send(new ViewModelMessage
                {
                    Message = ViewModelMessage.Message_OpenDialog,
                    Dialog = ViewModelMessage.Dialog_Failed
                });
            }
        }

        private async void DeleteUser()
        {
            BankOrderClient client = new BankOrderClient();
            var success = await client.DeleteAsync(username);
            client.Close();

            if (success)
            {
                Messenger.Default.Send(new ViewModelMessage
                {
                    Message = ViewModelMessage.Message_OpenDialog,
                    Dialog = ViewModelMessage.Dialog_Success
                });
            }
            else
            {
                Messenger.Default.Send(new ViewModelMessage
                {
                    Message = ViewModelMessage.Message_OpenDialog,
                    Dialog = ViewModelMessage.Dialog_Failed
                });
            }
        }

        private async void FindUser()
        {
            BankOrderClient client = new BankOrderClient();
            var user = await client.GetUserByIdAsync(userid);
            client.Close();

            if (user != null)
            {
                UserName = user.UserName;
                Password = user.Password;
                EmailAddress = user.EmailAddress;
                FirstName = user.Firstname;
                LastName = user.Lastname;
                Region = user.Region;
                City = user.City;
                Country = user.Country;
                Address = user.Address;
            }
            else
            {
                Messenger.Default.Send(new ViewModelMessage
                {
                    Message = ViewModelMessage.Message_OpenDialog,
                    Dialog = ViewModelMessage.Dialog_Failed
                });
            }
        }

        private async void List()
        {
            BankOrderClient client = new BankOrderClient();
            var accounts = await client.GetAccountsByUserAsync(this.username);
            client.Close();     
            if (accounts != null) { 
                Messenger.Default.Send(new ViewModelMessage
                {
                    Guid = Guid,
                    UserName = username,
                    Message = ViewModelMessage.Message_Navigate,
                    NavigateTo = ViewModelMessage.Navigation_AccountDetails
                });
            }
        }

        private async void Logout()
        {
            BankOrderClient client = new BankOrderClient();
            var success = await client.LogoutAsync(Guid);
            client.Close();
            if (success)
            {
                Messenger.Default.Send(new ViewModelMessage
                {
                    Message = ViewModelMessage.Message_Navigate,
                    NavigateTo = ViewModelMessage.Navigation_Login
                });
            }
        }
    }
}