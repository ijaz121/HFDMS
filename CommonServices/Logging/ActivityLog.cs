using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.Logging
{
    public class ActivityLog
    {
        public string? LogID { get; set; } 
        public string? UserName { get; set; } 
        public string? RequestPath { get; set; } 
        public string? RequestMethod { get; set; } 
        public string? RequestData { get; set; } 
        public DateTime RequestTime { get; set; } 
        public DateTime ResponseTime { get; set; }
        public int? ResponseStatusCode { get; set; } 
        public string? ResponseData { get; set; } 
    }

}
