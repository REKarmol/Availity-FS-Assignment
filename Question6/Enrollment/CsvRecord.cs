using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enrollment
{
    class CsvRecord
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Version { get; set; }
        public CsvRecord(string userId, string firstName, string lastName, int version)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Version = version;
        }
    }
}
