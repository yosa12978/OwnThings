using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnThings.API.Payload.Response
{
    public class MeasurementPageResponse
    {
        public IEnumerable<MeasurementResponse> measurements { get; set; }
        public PageResponse PageViewModel { get; set; }
    }
}
