using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnThings.API.Payload.Request
{
    public class CreateUserRequest
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
