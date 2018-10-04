using GalaSoft.MvvmLight.Messaging;
using BankClient.BankOrder;
using System;
using System.Collections.Generic;

namespace BankClient
{
    public class ViewModelMessage : MessageBase
    {
        public static string Message_Navigate = "Navigate";
        public static string Message_LoadAccountList = "LoadAccountList";
        public static string Message_OpenDialog = "OpenDialog";


        public static string Navigation_Login = "Login";
        public static string Navigation_AccountDetails = "AccountDetails";
        public static string Navigation_Customer = "Customer";

        public static string Dialog_AccountDetailsView = "AccountDetailsView";
        public static string Dialog_LoginFailed = "LoginFailed";
        public static string Dialog_Failed = "Failed";
        public static string Dialog_Success = "Success";


        public string UserName { get; set; }
        public Guid Guid { get; set; }
        public string Message { get; set; }
        public Account Account { get; set; }
        public string NavigateTo { get; set; }
        public string Dialog { get; set; }

    }
}
