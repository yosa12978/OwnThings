using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OwnThings.Core.Data;
using OwnThings.Core.Models;
using OwnThings.Core.Repositories.Interfaces;
using OwnThings.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OwnThings.Core.Repositories.Mocks
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly OwnThingsContext _db;
        private readonly ILogger<DeviceRepository> _logger;
        public DeviceRepository(OwnThingsContext db, ILogger<DeviceRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public Device CreateDevice(string name, string token, string username)
        {
            if (_db.devices.Any(m => m.name == name))
                return null;

            Device device = new Device
            {
                name = name,
                token = token,
                owner = _db.users.FirstOrDefault(m => m.username == username)
            };
            _db.devices.Add(device);
            _db.SaveChanges();
            _logger.LogInformation($"Created Device with id {device.id}");
            return device;
        }

        public Device GetDeviceById(long id, string username)
        {
            _logger.LogInformation($"Returning device with id {id} for user {username}");
            return _db.devices
                .Include(m => m.owner)
                .Include(m => m.measurements)
                .FirstOrDefault(m => m.id == id && m.owner.username == username);
        }

        public Device GetDeviceByToken(string token, string username)
        {
            _logger.LogInformation($"Returning device with token {token} for user {username}");
            return _db.devices
                .Include(m => m.owner)
                .Include(m => m.measurements)
                .FirstOrDefault(m => m.token == token && m.owner.username == username);
        }

        //TODO: Change Search method return type to DeviceViewModel
        public List<Device> SearchDevice(string name, string username)
        {
            _logger.LogInformation($"Searching device with query {name} for user {username}");
            return _db.devices
                .Include(m => m.owner)
                .Where(m => m.owner.username == username && m.name.Contains(name))
                .OrderByDescending(m => m.id)
                .ToList();
        }

        //TODO: Change method return type to DeviceViewModel
        public List<Device> GetDevices(string username)
        {
            _logger.LogInformation($"Returning devices for user {username}");
            return _db.devices
                .Include(m => m.owner)
                .Where(m => m.owner.username == username)
                .OrderByDescending(m => m.id)
                .ToList();
        }

        public void DeleteDevice(string token, string username)
        {
            //TODO: Add trigger for not found
            Device device = _db.devices
                .Include(m => m.owner)
                .FirstOrDefault(m => m.owner.username == username && m.token == token);
            if (device == null)
                return;
            _logger.LogInformation($"Deleted device with token {token} and user {username}");
            _db.devices.Remove(device);
            _db.SaveChanges();
        }
    }
}
