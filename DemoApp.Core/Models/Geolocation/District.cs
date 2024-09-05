using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Core.Models.Geolocation
{
    public class District
    {
        public int Id { get; set; }
        public int RegionId { get; set; }
        public string Name { get; set; }
        public string DistrictType { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
        public string? UserModified { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
