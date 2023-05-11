using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using holidayRequestSystem;
using MySql.Data.MySqlClient;

namespace database
{
    internal class Sql
    {
        public bool actionSql(HolidayRequest holidayRequest)
        {
            string connStr = "server=localhost;user=root;database=wnioskiUrlopowe;port=3306;password=DrT%432ws";
            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                connection.Open();
                string userID = holidayRequest.createId();
                
                string insertUserStatement = "INSERT INTO uzytkownicy (id, imie, nazwisko) VALUES (@Value1, @Value2, @Value3)";

                MySqlCommand commandUser = new MySqlCommand(insertUserStatement, connection);
                commandUser.Parameters.AddWithValue("@Value1", userID);
                commandUser.Parameters.AddWithValue("@Value2", holidayRequest.getName());
                commandUser.Parameters.AddWithValue("@Value3", holidayRequest.getSurname());

                // execute the command
                int rowsUserAffected = commandUser.ExecuteNonQuery();
                

                string insertRequestStatement = "INSERT INTO wnioski (dataStart, dataKoniec, idUzytkownik) VALUES (@Value1, @Value2, @Value3)";

                MySqlCommand commandRequest = new MySqlCommand(insertRequestStatement, connection);
                commandRequest.Parameters.AddWithValue("@Value1", holidayRequest.getStartDate());
                commandRequest.Parameters.AddWithValue("@Value2", holidayRequest.getEndDate());
                commandRequest.Parameters.AddWithValue("@Value3", userID);


                // execute the command
                int rowsRequestAffected = commandRequest.ExecuteNonQuery();
                if (rowsUserAffected > 0 && rowsRequestAffected > 0)
                {
                    connection.Close();
                    return true;
                }
                else
                {
                    connection.Close();
                    return false;
                }
            }
        }


    }
}
