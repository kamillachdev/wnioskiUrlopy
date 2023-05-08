using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Program
{
    class Data
    {
        private string name, surname, startDate, endDate;

        public bool nameValidation(string name)
        {
            string namePattern = @"^[a-zA-Z]+$";
            bool isMatchN = Regex.IsMatch(name, namePattern);
            if (!isMatchN)
            {
                Console.WriteLine("Wprowadzono niepoprawne imie!");
                return false;
            }
            else
                return true;
        }

        public bool surnameValidation(string surname)
        {
            string namePattern = @"^[a-zA-Z-]+$";
            bool isMatchS = Regex.IsMatch(surname, namePattern);
            if (!isMatchS)
            {
                Console.WriteLine("Wprowadzono niepoprawne nazwisko!");
                return false;
            }
            else
                return true;
        }

        public bool dateValidation(string sD, string eD)
        {
            string datePattern = @"^\d{2}-\d{2}-\d{4}$";
            bool isMatchsD = Regex.IsMatch(sD, datePattern);
            bool isMatcheD = Regex.IsMatch(eD, datePattern);
            
            if(!(isMatcheD && isMatchsD) )
            {
                Console.WriteLine("Wpowadzono nieprawdidłową datę. Oczekiwany format: \"DD-MM-RRRR\"");
                return false;
            }


            DateTime startDate = DateTime.ParseExact(sD, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(eD, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            if (startDate <= endDate)
                return true;
            else
            {
                Console.WriteLine("Data początkowa nie może być później niż data końcowa!");
                return false;
            }
        }

       
        public void getData()
        {
            do
     
            Console.WriteLine("Podaj imię: ");
            name = Console.ReadLine();

            Console.WriteLine("Podaj nazwisko: ");
            surname = Console.ReadLine();

            do
            {
                Console.WriteLine("Podaj date początkową urlopu: ");
                startDate = Console.ReadLine();

                Console.WriteLine("Podaj date końcową urlopu: ");
                endDate = Console.ReadLine();
            } while(!dateValidation(startDate, endDate));

            
        }
        public void printData()
        {

        }
    }
    class Menu
    {
        static void Main()
        {
            Console.WriteLine("Nowy wniosek urlopowy");
            Data data = new Data();
            data.getData();

        }
    }
}