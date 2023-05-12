using database;
using Google.Protobuf;
using holidayRequestSystem;
using Program;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Bcpg.Attr.ImageAttrib;

namespace menuSystem
{
    internal class Menu
    {

        public string PrintNextStep(int option)
        {
            string output = "";
            switch (option)
            {
                case 0:
                    output = "LOGOWANIE";
                    break;
                case 1:
                    output = "Podaj imię: ";
                    break;
                case 2:
                    output = "Podaj nazwisko: ";
                    break;
                case 3:
                    output = "Wybór: ";
                    break;
                case 4:
                    output = "Podaj datę początkową w formacie dd-MM-yyyy: ";
                    break;
                case 5:
                    output = "Podaj datę końcową w formacie dd-MM-yyyy: ";
                    break;
            }
            return output;
        }

        public string printMenu()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("MENU");
            sb.AppendLine("1. Nowy wniosek urlopowy");
            sb.AppendLine("2. Pokaż listę wniosków");
            sb.AppendLine("3. Wyloguj się");
            return sb.ToString();
        }

        public enum MenuChoice { Undefined, CreateHolidayRequest, showRequests, LogOut};

        public MenuChoice ReadChoice(string choice)
        {
            switch(choice)
            {
                case "1":
                    return MenuChoice.CreateHolidayRequest;
                case "2":
                    return MenuChoice.showRequests;
                case "3":
                    return MenuChoice.LogOut;
                default:
                    return MenuChoice.Undefined;
            }   
        }

        public string menuAction(MenuChoice menuChoice, HolidayRequest holidayRequest)
        {
            Sql sql = new Sql();
            string actionOutput = "";

            if (menuChoice == MenuChoice.LogOut)
            {
                Environment.Exit(0);
            }
            else if (menuChoice == MenuChoice.showRequests)
            {
                actionOutput = sql.getAllRequestsForUser(holidayRequest);
            }
            else if (menuChoice == MenuChoice.CreateHolidayRequest)
            {
                (DateTime start, DateTime end) = holidayManegement.GetHolidayDates();

                holidayRequest.setDate(start, end);
                string dateValidation = holidayRequest.isDateValid();
                Console.WriteLine(dateValidation);

                if (sql.dataManagement(holidayRequest))
                {
                    actionOutput = holidayRequest.GetSummary();
                }
                else
                {
                    actionOutput = "Nie udało się wysłać wniosku.";
                }
            }

            return actionOutput;
        }


        public DateTime convertToDate(string date)
        {
            DateTime returnDate;
            DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out returnDate);
            return returnDate;
        }

        public bool isDateValid(string date)
        {
            DateTime startDate;

            if (DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
