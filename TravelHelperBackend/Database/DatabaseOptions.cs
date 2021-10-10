using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelHelperBackend.Database
{
    public class DatabaseOptions
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string DbName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
