using BankDAL.Entities;
using System.Data.Entity;

namespace BankDAL
{
    public class AccountContext : DbContext
    {
        public static string ConnectionString = @"BankClient";
        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }

        public AccountContext(string connStr) : base(connStr)
        {

        }
        public AccountContext()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>();
      
        }
    }
}
