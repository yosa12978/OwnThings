using OwnThings.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OwnThings.Core.ViewModels
{
    public class MeasurementViewModel
    {
        public IEnumerable<Measurement> measurements { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
