using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using BankClient.BankOrder;

namespace BankClient.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class AccountDetailsViewModel : ViewModelBase
    {
        private ObservableCollection<AccountViewModel> accountList;

        public ICommand LogoutCommand { get; set;}
        public ICommand BackCommand { get; set; }

        public Guid Guid
        {
            get
            {
                return this.guid;
            }
            set
            {
                this.guid = value;
                RaisePropertyChanged("Guid");
            }
        }

        private Guid guid;


        public string UserName
        {
            get
            {
                return this.username;
            }
            set
            {
                this.username = value;
                RaisePropertyChanged("UserName");
            }
        }

        private string username;


        public ObservableCollection<AccountViewModel> AccountList
        {
            get
            {
                return accountList;
            }
        }
        

        public AccountDetailsViewModel()
        {
            accountList = new ObservableCollection<AccountViewModel>();
            Guid = new Guid();
            Messenger.Default.Register<ViewModelMessage>(this, OnReceiveMessage);
            BackCommand = new RelayCommand(Back);      
            LogoutCommand = new RelayCommand(Logout);      
        }


        private async void Back()
        {   
            Messenger.Default.Send(new ViewModelMessage
            {
                Message = ViewModelMessage.Message_Navigate,
                NavigateTo = ViewModelMessage.Navigation_Customer
            });
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

        private void OnReceiveMessage(ViewModelMessage msg)
        {
            if (msg.Message == ViewModelMessage.Message_LoadAccountList)
            {
                this.Guid = msg.Guid;        
                this.UserName = msg.UserName;
                LoadaccountList();
           }
         
        
        }

        public async void LoadaccountList()
        {
            // do not load list if we already have it
            if (accountList.Count == 0)
            {
                BankOrderClient client = new BankOrderClient();
                //var accounts = await client.GetAllAccountsAsync(this.guid);
                var accounts = await client.GetAccountsByUserAsync(this.username);
                client.Close();
                foreach (var item in accounts)
                {
                    accountList.Add(new AccountViewModel(item));
                }

              
            }
        }
    }
}