using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PDMProject
{
    class DBManager
    {
        SQLiteConnection connection;

        public DBManager()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "loan_history.db");
            connection = new SQLiteConnection(path);
            connection.CreateTable<Loan>();
        }

        public int AddLoanToDB(Loan loan)
        {
            return connection.Insert(loan);
        }
        public int UpdateLoan(Loan loan)
        {
            return connection.Update(loan);
        }

        //For Settings Menu
        public int DeleteAllData()
        {
            return connection.DropTable<Loan>();
            //OR
            //return connection.DeleteAll<Loan>();
        }

        public List<Loan> LoadData()
        {
            return connection.Query<Loan>("SELECT * FROM loan");


        }

    }
}
