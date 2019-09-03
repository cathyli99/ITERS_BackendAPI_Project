using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItersTutoriov1.Models
{
    public class ApiReturnMessage
    {
        public int? StatusCode { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string InternalMessage { get; set; }
    }
}
