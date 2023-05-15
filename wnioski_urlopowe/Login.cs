using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wnioski_urlopowe
{
    internal class Login
    {
        public enum loginMessages
        {
            startLogin, insertName, insertSurname
        };
        public string printMessages(loginMessages messages)
        {
            switch (messages)
            {
                case loginMessages.startLogin:
                    return "LOGOWANIE";
                case loginMessages.insertName:
                    return "Podaj imię: ";
                case loginMessages.insertSurname:
                    return "Podaj nazwisko: ";
                default:
                    return "Błąd";
            }
        }
    }
}
