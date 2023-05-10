using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace holidayRequestSystem
{
    internal class HolidayRequest
    {
        string name, surname;
        DateTime holidayStartDate, holidayEndDate;

        public HolidayRequest(string name, string surname, DateTime holidayStartDate, DateTime holidayEndDate)
        {
            this.name = name;
            this.surname = surname;
            this.holidayStartDate = holidayStartDate;
            this.holidayEndDate = holidayEndDate;
        }

        static public string isValid(string name, string surname, DateTime holidayStartDate, DateTime holidayEndDate)
        {
            string namePattern = @"^[A-Z][a-zA-Z]{1,}$";

            if (!Regex.IsMatch(name, namePattern))
            {
                return "Niepoprawne imię";
            }

            if (!Regex.IsMatch(surname, namePattern))
            {
                return "Niepoprawne nazwisko";
            }

            if (holidayStartDate < DateTime.Today)
            {
                return "Data początkowa nie może być przed datą dzisiejszą";
            }

            if (holidayEndDate < holidayStartDate)
            {
                return "Data końcowa musi być późniejsza niż data początkowa";
            }

            return "OK";
        }


        private int daysCounting(DateTime startDate, DateTime endDate)
        {
            int days = 0;
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                    days++;
            }
            return days;
        }

        public string GetSummary()
        {
            string summary = string.Empty;
            int days = daysCounting(holidayStartDate, holidayEndDate);
            summary = "Przyjęto wniosek urlopowy dla " + name + " " + surname + " od " + holidayStartDate.ToString("d") + " do " + holidayEndDate.ToString("d");
            summary += "\nIlość dni:" + days;
            return summary;
        }
    }
}