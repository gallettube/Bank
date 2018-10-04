using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDAL.Entities
{
    public class Account
    {

        [Index(IsUnique = true)]
        public int Id { get; set; }

        //[MaxLength(20)]
        public string AccountNumber { get; set;  }
        public string Description { get; set; }


        public Account()
        {
        }

    }
}
