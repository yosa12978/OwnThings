using OwnThings.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OwnThings.Core.ViewModels
{
    public class DeviceViewModel
    {
        public IEnumerable<Device> devices { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
