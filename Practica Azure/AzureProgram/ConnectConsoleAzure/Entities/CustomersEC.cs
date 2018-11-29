using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectConsoleAzure.Entities
{
    class CustomersEC :TableEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public CustomersEC(string _name, string _email)
        {
            Name = _name;
            Email = _email;
        }
    }
}
