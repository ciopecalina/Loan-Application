using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PDMProject
{
    [Table("loan")]
   public class Loan
    {
        public Loan()
        {
            LoanType = "Personal";
            Currency = "RON";
        }
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        //Entry data
        public string LoanType { get; set; }
        public double Amount { get; set; }
        public double Rate { get; set; }
        public int Tenure { get; set; }
        public string Currency { get; set; }

        //Calculated data
        public double PrincipalAmount { get; set; }
        public double TotalInterest { get; set; }
        public double TotalPayable { get; set; }
        public double InitialPayment { get; set; }
        public double EMIAmount { get; set; }

        

    }
}
