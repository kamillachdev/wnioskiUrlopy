using database;
using Google.Protobuf;
using holidayRequestSystem;
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

        public enum menuMessages { insertChoice, insertStartDate, insertEndDate };

        public string printMessages(menuMessages messages)
        {
            switch (messages)
            {
                case menuMessages.insertChoice:
                    return "Wybór: ";
                case menuMessages.insertStartDate:
                    return "Podaj datę początkową w formacie dd-MM-yyyy: ";
                case menuMessages.insertEndDate:
                    return "Podaj datę końcową w formacie dd-MM-yyyy: ";
                default:
                    return "Błąd";
            }
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

        public enum MenuChoice { Undefined, CreateHolidayRequest, showRequests, LogOut };

        public MenuChoice ReadChoice(string choice)
        {
            switch (choice)
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
    }
}