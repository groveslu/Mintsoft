using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mintsoft.API
{

    public class APIResult
    {
        public int ID { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string WarningMessage { get; set; }
    }

}
