using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using BankClient.BankOrder;
using System.Collections.Generic;
using System.Windows.Input;

namespace BankClient.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class AccountViewModel : ViewModelBase
    {
        private Account account;

        public string Description
        {
            get
            {
                return account.Description;
            }
        }

     

        public string AccountNumber
        {
            get
            {
                return account.AccountNumber;
            }
        }

        public AccountViewModel(Account a)
        {
            account = a;
        }


        public Account ToAccount()
        {
            return account;
        }
    }
}