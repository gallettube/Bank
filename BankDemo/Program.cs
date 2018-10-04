using BankDAL;
using BankDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (AccountContext ctx = new AccountContext(AccountContext.ConnectionString))
            {

                Account Cuenta1 = new Account
                {
                    Description = "Cuenta 1",
               
                    AccountNumber = "123456789123456789119"
                };
                Account Cuenta2 = new Account
                {
                    Description = "Cuenta 3",
                    AccountNumber = "123456789123456789118"
                };
                Account Cuenta3 = new Account
                {
                    Description = "Cuenta 2",
                    AccountNumber = "123456789123456789117"
                };
                Account Cuenta4 = new Account
                {
                    Description = "Cuenta 4",
                    AccountNumber = "123456789123456789116"
                };
                Account Cuenta5 = new Account
                {
                    Description = "Cuenta 5",
                    AccountNumber = "123456789123456789114"

                };

                ctx.Accounts.Add(Cuenta1);
                ctx.Accounts.Add(Cuenta2);
                ctx.Accounts.Add(Cuenta3);
                ctx.Accounts.Add(Cuenta4);
                ctx.Accounts.Add(Cuenta5);
            
                User testUser1 = new User
                {
                    UserName = "a",
                    Password = "1",
                    EmailAddress = "a@a.com",
                    Firstname = "a",
                    Lastname = "a",
                    Country = "a",
                    Region = "a",
                    City = "a",
                    Address = "a",
                    Accounts = new List<Account>(){
                        Cuenta1, Cuenta2, Cuenta3
                    }
                };

                User testUser2 = new User
                {
                    UserName = "b",
                    Password = "1",
                    EmailAddress = "a@a.com",
                    Firstname = "a",
                    Lastname = "a",
                    Country = "a",
                    Region = "a",
                    City = "a",
                    Address = "a",
                    Accounts = new List<Account>(){
                        Cuenta4, Cuenta5
                    }
                };


                ctx.Users.Add(testUser1);       
                ctx.Users.Add(testUser2);

                ctx.SaveChanges();
            }
        }
    }
}
