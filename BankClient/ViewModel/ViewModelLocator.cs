/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:PizzaClient.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using BankClient.ViewModel;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

namespace BankClient.ViewModel
{
   
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            if (ViewModelBase.IsInDesignModeStatic)
            {            }
            else
            {      
            }
            SimpleIoc.Default.Register<AccountViewModel>();
            SimpleIoc.Default.Register<AccountDetailsViewModel>();
            SimpleIoc.Default.Register<CustomerDetailsViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public LoginViewModel LoginView
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
            }
        }

        public AccountDetailsViewModel AccountDetailsView
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AccountDetailsViewModel>();
            }
        }

        public AccountViewModel AccountView
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AccountViewModel>();
            }
        }

        public CustomerDetailsViewModel CustomerDetailsView
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CustomerDetailsViewModel>();
            }
        }

        public static void Cleanup()
        {
        }
    }
}