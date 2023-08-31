using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PDMProject
{
    class NetUtils
    {
        const string url
            = "https://bitbucket.org/ciopecalina/pdm-xml-file/raw/12186e38c64da43edd0a46b91ef0750f225eec8d/FileBucuresti";

        public async static Task<List<Bank>> GetBank()
        {
            List<Bank> listBanks = new List<Bank>();

            HttpClient httpClient = new HttpClient();
            Stream stream = await httpClient.GetStreamAsync(url);

            XmlReader reader = XmlReader.Create(stream);
            string name = null;
            Bank bank = null;

            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    if (reader.Name == name)
                    {
                        reader.Read();
                        name = reader.Value;
                    }

                    if (reader.Name == "Bank")
                    {
                        bank = new Bank();
                        name = reader["name"];
                        bank.Name = reader["name"];
                        bank.Adress = reader["adress"];
                        bank.Telephone = reader["telephone"];
                        bank.Email = reader["email"];
                        bank.Web = reader["web"];

                        listBanks.Add(bank);

                        reader.Read();
                    }
                }
            }

            return listBanks;
        }
    }
}