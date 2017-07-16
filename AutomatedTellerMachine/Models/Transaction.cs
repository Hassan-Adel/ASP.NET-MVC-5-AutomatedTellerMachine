using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomatedTellerMachine.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Required]
        public string CheckingAccountId { get; set; }

        public virtual CheckingAccount CheckingAccount { get; set; }

    }
}