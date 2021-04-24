using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnThings.API.Payload.Response
{
    public class DeviceResponse
    {
        public long id { get; set; }
        public string name { get; set; }
        public UserResponse owner { get; set; }
        public string token { get; set; }
    }
}
