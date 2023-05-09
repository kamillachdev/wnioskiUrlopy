using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Program
{
    class Data
    {
        private string name, surname, startDateString, endDateString;
        private DateTime startDate, endDate;

        private bool nameValidation(string name)
        {
            string namePattern = @"^[A-Z][a-zA-Z]{1,}$";
            bool isMatchN = Regex.IsMatch(name, namePattern);
            if (!isMatchN)
            {
                Console.WriteLine("Wprowadzono niepoprawne imie!");
                return false;
            }
            else
                return true;
        }

        private bool surnameValidation(string surname)
        {
            string namePattern = @"^[A-Z][a-zA-Z-]{1,}$";
            bool isMatchS = Regex.IsMatch(surname, namePattern);
            if (!isMatchS)
            {
                Console.WriteLine("Wprowadzono niepoprawne nazwisko!");
                return false;
            }
            else
                return true;
        }

        private bool dateValidation(string sD, string eD)
        {
            string datePattern = @"^\d{2}-\d{2}-\d{4}$";
            bool isMatchsD = Regex.IsMatch(sD, datePattern);
            bool isMatcheD = Regex.IsMatch(eD, datePattern);

            if (!(isMatcheD && isMatchsD))
            {
                Console.WriteLine("Wpowadzono nieprawdidłową datę. Oczekiwany format: \"DD-MM-RRRR\"");
                return false;
            }


            try
            {
                startDate = DateTime.ParseExact(sD, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                endDate = DateTime.ParseExact(eD, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {
                Console.WriteLine("Data jest nieprawidłowa!");
                return false;
            }


            if (DateTime.Compare(startDate, DateTime.Now) <= 0)
            {
                Console.WriteLine("Wprowadzona data początkowa nie może być przed dzisiejsza datą!");
                return false;   
            }

            if (startDate <= endDate)
                return true;
            else
            {
                Console.WriteLine("Data początkowa nie może być później niż data końcowa!");
                return false;
            }
        }

        private int daysCounting(DateTime startDate, DateTime endDate)
        {
            int days = 0;
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if(date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                    days++;
            }
            return days;
        }

       
        public void getData()
        {
            do
            {
                Console.WriteLine("Podaj imię: ");
                name = Console.ReadLine();

            } while (!nameValidation(name));

            do
            {
                Console.WriteLine("Podaj nazwisko: ");
                surname = Console.ReadLine();

            } while(!surnameValidation(surname));

            do
            {
                Console.WriteLine("Podaj date początkową urlopu (\"DD-MM-RRRR\"): ");
                startDateString = Console.ReadLine();

                Console.WriteLine("Podaj date końcową urlopu (\"DD-MM-RRRR\"): ");
                endDateString = Console.ReadLine();

            } while(!dateValidation(startDateString, endDateString));
            
        }

        public void showSummary()
        {
            int days = daysCounting(startDate, endDate);
            Console.WriteLine($"Przyjęto wniosek urlopowy dla {name} {surname} od {endDateString} do {endDateString}");
            Console.WriteLine($"Ilość dni: {days}");
        }
    }
    class Menu
    {
        static string choice;

        static void choiceAction(Data dataObject)
        {
           
            bool validChoice = false;
            while (!validChoice)
            {
                Console.WriteLine("Wybierz: ");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        validChoice = true;
                        dataObject.getData();
                        dataObject.showSummary();
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór!");
                        break;
                }
            }
        }
        
        static void Main()
        {
            Data data = new Data();
            Console.WriteLine("1. Nowy wniosek urlopowy");
            choiceAction(data);
        }
    }
}