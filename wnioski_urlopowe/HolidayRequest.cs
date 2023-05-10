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
            string namePattern = @"^[A-Z][a-zA-Z]{1,}$";
            string datePattern = @"^\d{2}\-\d{2}\-\d{4}$";

            bool isMatchName = false, isMatchSurname = false, isMatchStartDate = false, isMatchEndDate = false;

            do
            {
                isMatchName = Regex.IsMatch(name, namePattern);
                if (!isMatchName)
                {
                    Console.WriteLine("Wprowadzono niepoprawne imię, wprowadź ponownie: ");
                    name = Console.ReadLine();
                }
                else
                    this.name = name;
            } while (!isMatchName);

            do
            {
                isMatchSurname = Regex.IsMatch(surname, namePattern);
                if (!isMatchSurname)
                {
                    Console.WriteLine("Wprowadzono niepoprawne nazwisko, wprowadź ponownie: ");
                    surname = Console.ReadLine();
                }
                else
                    this.surname = surname;
            } while (!isMatchSurname);

            do
            {
                isMatchStartDate = DateTime.TryParseExact(holidayStartDate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", null, DateTimeStyles.None, out holidayStartDate);
                if (!isMatchStartDate)
                {
                    Console.WriteLine("Wprowadzono niepoprawną datę początkową, wprowadź ponownie w formacie dd-MM-yyyy: ");
                    holidayEndDate = DateTime.Parse(Console.ReadLine());
                }
                else if (holidayStartDate < DateTime.Today)
                {
                    Console.WriteLine("Data początkowa nie może być przed datą dzisiejszą, wprowadź ponownie: ");
                    holidayStartDate = DateTime.Parse(Console.ReadLine());
                    isMatchStartDate = false;
                }
                else
                    this.holidayStartDate = holidayStartDate;
            } while (!isMatchStartDate);

            do
            {
                isMatchEndDate = DateTime.TryParseExact(holidayEndDate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", null, DateTimeStyles.None, out holidayEndDate);
                if (!isMatchEndDate)
                {
                    Console.WriteLine("Wprowadzono niepoprawną datę końcową, wprowadź ponownie w formacie dd-MM-yyyy: ");
                    holidayEndDate = DateTime.Parse(Console.ReadLine());
                }
                else if (holidayEndDate < holidayStartDate)
                {
                    Console.WriteLine("Data końcowa musi być późniejsza niż data początkowa, wprowadź ponownie: ");
                    holidayEndDate = DateTime.Parse(Console.ReadLine());
                    isMatchEndDate = false;
                }
                else
                    this.holidayEndDate = holidayEndDate;
            } while (!isMatchEndDate);
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