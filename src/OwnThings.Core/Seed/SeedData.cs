using Microsoft.Extensions.Logging;
using OwnThings.Core.Data;
using OwnThings.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace OwnThings.Core.Seed
{
    public static class SeedData
    {
        public static void EnsureSeedData(this OwnThingsContext _db)
        {
            User user1 = new User
            {
                username = "user",
                token = Guid.NewGuid().ToString(),
                devices = new List<Device>(),
                measurements = new List<Measurement>()
            };

            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes("12345678"));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                    builder.Append(hashBytes[i].ToString("x2"));
                user1.password = builder.ToString();
            }


            Device device1 = new Device
            {
                name = "Device_1",
                token = Guid.NewGuid().ToString(),
                owner = user1,
                measurements = new List<Measurement>()
            };

            Device device2 = new Device
            {
                name = "Device_2",
                token = Guid.NewGuid().ToString(),
                owner = user1,
                measurements = new List<Measurement>()
            };

            Measurement measurement1 = new Measurement
            {
                payload = "{ \"temperature\": \"25\" }",
                owner = user1,
                device = device1
            };

            Measurement measurement2 = new Measurement
            {
                payload = "{ \"Humidity\": \"54\" }",
                owner = user1,
                device = device2
            };

            device1.measurements.Add(measurement1);
            device1.measurements.Add(measurement2);

            user1.devices.Add(device1);
            user1.devices.Add(device2);

            user1.measurements.Add(measurement1);
            user1.measurements.Add(measurement2);

            if (!_db.users.Any() && !_db.devices.Any() && !_db.measurements.Any()) 
            {
                _db._logger.LogInformation("Seeding database");

                _db.users.Add(user1);
                _db.devices.Add(device1);
                _db.devices.Add(device2);
                _db.measurements.Add(measurement1);
                _db.measurements.Add(measurement2);

                _db.SaveChanges();

                _db._logger.LogInformation("Database seeded");
            }
        }
    }
}
