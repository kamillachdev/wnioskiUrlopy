using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Text.RegularExpressions;
using wnioski_urlopowe;

namespace Program
{
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