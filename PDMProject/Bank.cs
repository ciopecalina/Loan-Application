using System;
using System.Collections.Generic;
using System.Text;

namespace PDMProject
{
    class Bank
    {
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
        public string Logo
        {
            get
            {
                return Name.Replace(" ", "") + ".jpg";
            }
        }

        public Bank()
        {
            Name = "";
        }

    }
}