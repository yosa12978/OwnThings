using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnThings.API.Payload.Response
{
    public class MeasurementResponse
    {
        public long id { get; set; }
        public string payload { get; set; }
        public UserResponse owner { get; set; }
        public DeviceResponse device { get; set; }
    }
}
