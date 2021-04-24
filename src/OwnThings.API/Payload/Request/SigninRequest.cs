using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnThings.API.Payload.Request
{
    public class SigninRequest
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
