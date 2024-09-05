using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Core.CoreDbEntities
{
    public class Country
    {
        public int Id { get; set; }
        public char CountryCode { get; set; }
        public string Name { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
        public string ?UserModified { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
