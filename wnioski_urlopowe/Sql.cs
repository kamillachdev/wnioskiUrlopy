using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace database
{
    internal class Sql
    {
        public string actionSql()
        {
            string queryOutput = " ";
            string connStr = "server=localhost;user=root;database=wnioskiUrlopowe;port=3306;password=DrT%432ws";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string query = "select * from uzytkownicy";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                //read the data
                while (rdr.Read())
                {
                    queryOutput += rdr[0] + " -- " + rdr[1] + " -- " + rdr[2];
                }
                rdr.Close();
            }
            catch
            {
                queryOutput = "Nie udało się połączyć z bazą!";
            }

            return queryOutput;
        }


    }
}
