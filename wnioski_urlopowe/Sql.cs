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

        public bool createUser()
        {
            return true;
        }
        public bool dataManagement(HolidayRequest holidayRequest)
        {
            string connStr = "server=localhost;user=root;database=wnioskiUrlopowe;port=3306;password=DrT%432ws;";
            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                connection.Open();
                string userID = "";
                // Check if user already exists in uzytkownicy table
                string checkUserStatement = "SELECT id FROM uzytkownicy WHERE imie = @Value1 AND nazwisko = @Value2";
                MySqlCommand checkUserCommand = new MySqlCommand(checkUserStatement, connection);
                checkUserCommand.Parameters.AddWithValue("@Value1", holidayRequest.getName());
                checkUserCommand.Parameters.AddWithValue("@Value2", holidayRequest.getSurname());
                using (MySqlDataReader reader = checkUserCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        // User already exists, retrieve their ID
                        reader.Read();
                        userID = reader.GetString(0);
                    }
                    else
                    {
                        // User does not exist, insert new user into uzytkownicy table
                        reader.Close();
                        userID = holidayRequest.createId();
                        string insertUserStatement = "INSERT INTO uzytkownicy (id, imie, nazwisko) VALUES (@Value1, @Value2, @Value3)";
                        MySqlCommand commandUser = new MySqlCommand(insertUserStatement, connection);
                        commandUser.Parameters.AddWithValue("@Value1", userID);
                        commandUser.Parameters.AddWithValue("@Value2", holidayRequest.getName());
                        commandUser.Parameters.AddWithValue("@Value3", holidayRequest.getSurname());
                        int rowsUserAffected = commandUser.ExecuteNonQuery();
                        if (rowsUserAffected == 0)
                        {
                            connection.Close();
                            return false;
                        }
                    }
                }

                // Insert new holiday request into wnioski table using userID
                string insertRequestStatement = "INSERT INTO wnioski (dataStart, dataKoniec, idUzytkownik) VALUES (@Value1, @Value2, @Value3)";
                MySqlCommand commandRequest = new MySqlCommand(insertRequestStatement, connection);
                commandRequest.Parameters.AddWithValue("@Value1", holidayRequest.getStartDate());
                commandRequest.Parameters.AddWithValue("@Value2", holidayRequest.getEndDate());
                commandRequest.Parameters.AddWithValue("@Value3", userID);
                int rowsRequestAffected = commandRequest.ExecuteNonQuery();
                if (rowsRequestAffected == 0)
                {
                    connection.Close();
                    return false;
                }
                connection.Close();
                return true;
            }
        }

        public string getAllRequestsForUser(HolidayRequest holidayReques)
        {
            string connStr = "server=localhost;user=root;database=wnioskiUrlopowe;port=3306;password=DrT%432ws";
            StringBuilder sb = new StringBuilder();

            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                connection.Open();
                // Find the user ID
                string getUserIDStatement = "SELECT id FROM uzytkownicy WHERE imie = @Value1 AND nazwisko = @Value2";
                MySqlCommand getUserIDCommand = new MySqlCommand(getUserIDStatement, connection);
                getUserIDCommand.Parameters.AddWithValue("@Value1", holidayReques.getName());
                getUserIDCommand.Parameters.AddWithValue("@Value2", holidayReques.getSurname());
                string userID = "";
                using (MySqlDataReader reader = getUserIDCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        userID = reader.GetString(0);
                    }
                    else
                    {
                        sb.AppendLine("Nie utworzono żadnych wniosków.");
                        connection.Close();
                        return sb.ToString();
                    }
                }

                // Find all requests for the user
                string getRequestsStatement = "SELECT * FROM wnioski WHERE idUzytkownik = @Value1";
                MySqlCommand getRequestsCommand = new MySqlCommand(getRequestsStatement, connection);
                getRequestsCommand.Parameters.AddWithValue("@Value1", userID);
                using (MySqlDataReader reader = getRequestsCommand.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        sb.AppendLine("Podany użytkownik nie ma żadnych wniosków.");
                        connection.Close();
                        return sb.ToString();
                    }
                    sb.AppendLine("Lista użytkownika " + holidayReques.getName() + " " + holidayReques.getSurname() + ":");
                    while (reader.Read())
                    {
                        string startDate = reader.GetDateTime(1).ToString("yyyy-MM-dd");
                        string endDate = reader.GetDateTime(2).ToString("yyyy-MM-dd");
                        sb.AppendLine("Data rozpoczęcia: " + startDate + " Data zakończenia: " + endDate);
                    }
                }
                connection.Close();
                return sb.ToString();
            }
        }
    }
}
