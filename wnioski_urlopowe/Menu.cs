﻿using database;
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
        public string printMenu()
        {


            StringBuilder sb = new StringBuilder();
            sb.AppendLine("MENU");
            sb.AppendLine("1. Nowy wniosek urlopowy");
            sb.AppendLine("2. Pokaż listę wniosków");
            return sb.ToString();
        }

        public enum MenuChoice { Undefined, CreateHolidayRequest, showRequests};

        public MenuChoice ReadChoice(string choice)
        {
            switch(choice)
            {
                case "1":
                    return MenuChoice.CreateHolidayRequest;
                case "2":
                    return MenuChoice.showRequests;
                default:
                    return MenuChoice.Undefined;
            }   
        }

        public string menuAction(MenuChoice menuChoice, HolidayRequest holidayRequest)
        {
            Sql sql = new Sql();
            string actionOutput = "";
            if(menuChoice == MenuChoice.CreateHolidayRequest) 
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
            else if(menuChoice == MenuChoice.showRequests)
            {
                actionOutput = sql.getAllRequestsForUser(holidayRequest);
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
