using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OwnThings.Core.Models
{
    public class Measurement
    {
        public long id { get; set; }
        [Required]
        public string payload { get; set; }
        [Required]
        public User owner { get; set; }
        [Required]
        public Device device { get; set; }
    }
}
