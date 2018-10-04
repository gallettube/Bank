using BankDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BankService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBankOrder" in both code and config file together.
    [ServiceContract]
    public interface IBankOrder
    {
        [OperationContract]
        bool Register(string userName, string password, string emailAddress, string firstName, 
            string lastName, string country, string region, string city, string address);

        [OperationContract]
        bool Update(string userName, string password, string emailAddress, string firstName,
            string lastName, string country, string region, string city, string address);

        [OperationContract]
        bool Delete(string username);


        [OperationContract]
        Guid Login(User user);

        [OperationContract]
        bool Logout(Guid guid);

        [OperationContract]
        List<Account> GetAllAccounts(Guid guid);

        [OperationContract]
        List<Account> GetAccountsByUser(string username);

        [OperationContract]
        User GetUserById(int Id);
    }
}
