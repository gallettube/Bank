using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDAL.Entities
{
    public class User
    {
        [Key]
        [Index(IsUnique = true)]
        public int Id { get; set; }
        [MaxLength(10)]
        [Index(IsUnique = true)]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public ICollection<Account> Accounts { get; set; }

        public User()
        {
          Accounts = new List<Account>();
        }
    }
}
