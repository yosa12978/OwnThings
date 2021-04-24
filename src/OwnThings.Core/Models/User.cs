using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OwnThings.Core.Models
{
    public class User
    {
        public long id { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        [JsonIgnore]
        public string password { get; set; }
        [Required]
        public string role { get; set; } = Role.USER.ToString();
        public string token { get; set; }
        [Required]
        public DateTime regDate { get; set; } = DateTime.Now;
        [JsonIgnore]
        public List<Device> devices { get; set; }
        [JsonIgnore]
        public List<Measurement> measurements { get; set; }
    }
}
