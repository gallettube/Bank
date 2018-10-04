using BankDAL;
using BankDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Migrations;

namespace BankService
{
    public class BankOrder : IBankOrder
    {

        public bool Register(string userName, string password, string emailAddress, string firstName, 
            string lastName, string country, string region, string city, string address)
        {
            bool registerSucceeded = false;
            using (AccountContext ctx = new AccountContext(AccountContext.ConnectionString))
            {
                var q = from user in ctx.Users
                        where user.UserName == userName && user.Password == password
                        select user;

                if (q.ToList().Count == 0)
                {
                    ctx.Users.Add(new User
                    {
                        UserName = userName,
                        Password = password,
                        EmailAddress = emailAddress,
                        Firstname = firstName,
                        Lastname = lastName,
                        Country = country,
                        Region = region,
                        City = city,
                        Address = address
                    });
                    ctx.SaveChanges();
                    registerSucceeded = true;
                }
            }
            return registerSucceeded;
        }

        public bool Update(string userName, string password, string emailAddress, string firstName,
          string lastName, string country, string region, string city, string address)
        {
            bool updateSucceeded = false;
            using (AccountContext ctx = new AccountContext(AccountContext.ConnectionString))
            {
                var q = from user in ctx.Users
                        where user.UserName == userName && user.Password == password
                        select user;

                if (q.ToList().Count == 1)
                {

                    User u = q.FirstOrDefault();
                    /*
                    User u = new User
                                        {
                                            UserName = userName,
                                            Password = password,
                                            EmailAddress = emailAddress,
                                            Firstname = firstName,
                                            Lastname = lastName,
                                            Country = country,
                                            Region = region,
                                            City = city,
                                            Address = address
                                        };
                     */                 
                    //ctx.Users.Attach(user);
                    u.UserName = userName;
                    u.Password = password;
                    u.EmailAddress = emailAddress;
                    u.Firstname = firstName;
                    u.Lastname = lastName;
                    u.Country = country;
                    u.Region = region;
                    u.City = city;
                    u.Address = address;
                    ctx.Users.AddOrUpdate(u);
                    ctx.SaveChanges();
                    updateSucceeded = true;
                }
            }
            return updateSucceeded;
        }

        public bool Delete(string userName)
        {
            bool deleteSucceeded = false;
            using (AccountContext ctx = new AccountContext(AccountContext.ConnectionString))
            {
                var q = from user in ctx.Users
                         where user.UserName == userName
                         select user;

             
                if (q.ToList().Count == 1)
                {
                    User u = q.FirstOrDefault();
                    ctx.Users.Attach(u);
                    ctx.Users.Remove(u);
                    ctx.SaveChanges();

                    deleteSucceeded = true;
                }
            }
            return deleteSucceeded;
        }

        public Guid Login(User user)
        {
            using (AccountContext ctx = new AccountContext(AccountContext.ConnectionString))
            {
                var q = from u in ctx.Users
                        where u.UserName == user.UserName && u.Password == user.Password
                        select u;
                if (q.ToList().Count == 1)
                {
                    return SessionManager.Instance.AddUser(user.UserName);
                }
                else
                {
                    return new Guid();
                }
            }
        }
   
     
      
        public bool Logout(Guid guid)
        {
            bool success = false;
            SessionManager.Instance.ActiveUsers.Remove(guid);
            success = true;
            return success;
        }

        public List<Account> GetAllAccounts(Guid guid)
        {
            if (SessionManager.Instance.ValidateUser(guid))
            {
                using (AccountContext ctx = new AccountContext(AccountContext.ConnectionString))
                {
                    List<Account> accounts = ctx.Accounts.ToList();
                    return accounts;
                }
            }
            else
            {
                return null;
            }
        }


        public List<Account> GetAccountsByUser(string username)
        {
            using (AccountContext ctx = new AccountContext(AccountContext.ConnectionString))
            {
                var Id = (from usr in ctx.Users
                          where usr.UserName == username
                          select usr.Id).FirstOrDefault();

                /*var l = from users in ctx.Users
                        where users.UserName == username
                        select users.Accounts;*/

                var l = ctx.Database.SqlQuery<Account>(
                    "select * from dbo.Accounts as a inner join dbo.Users as u on u.Id = a.User_Id where u.UserName = '" + username + "'"
                    );

                if (l.ToList().Count > 0)
                {
                    List<Account> accounts = l.ToList();
                    return accounts;
                }
                else
                {
                    return null;
                }
            }
        }

        public User GetUserById(int userId){
            using (AccountContext ctx = new AccountContext(AccountContext.ConnectionString))
            {
                var u = from user in ctx.Users
                        where user.Id == userId
                        select user;
                return u.FirstOrDefault();
                }
            }
        }
}
