using holidayRequestSystem;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace manageDatabase
{
    public class User
    {
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public Request Request { get; set; }
    }

    public class Request
    {
        public int RequestID { get; set; }
        public DateTime? dateStart { get; set; }
        public DateTime? dateEnd { get; set; }

        public ICollection<User> Users { get; set; }
    }

    public class HolidayContext : DbContext
    {
        public HolidayContext(): base()
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Request> Requests { get; set; }
    }
}
