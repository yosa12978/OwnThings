using System;
using System.Collections.Generic;
using System.Text;

namespace OwnThings.Core.Models
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public string JWTSecret { get; set; }
    }
}
