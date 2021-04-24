using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OwnThings.Core.Models
{
    public class Device
    {
        public long id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string token { get; set; }
        public List<Measurement> measurements { get; set; }
        [Required]
        public User owner { get; set; }
    }
}
