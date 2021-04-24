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
    public class MeasurementRepository : IMeasurementRepository
    {
        private readonly OwnThingsContext _db;
        private readonly ILogger<MeasurementRepository> _logger;
        public MeasurementRepository(OwnThingsContext db, ILogger<MeasurementRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public Measurement CreateMeasurement(string payload, string device_token, string username)
        {
            Measurement measurement = new Measurement
            {
                payload = payload,
                device = _db.devices.FirstOrDefault(m => m.token == device_token),
                owner = _db.users.FirstOrDefault(m => m.username == username)
            };
            _db.measurements.Add(measurement);
            _db.SaveChanges();
            _logger.LogInformation($"Created measurement of device {device_token} for user {username}");
            return measurement;
        }

        public MeasurementViewModel GetMeasurementsOfDevice(string token, string username, int page)
        {
            int pageSize = 50;
            IQueryable<Measurement> source = _db.measurements
                                            .Include(m => m.owner)
                                            .Include(m => m.device)
                                            .Where(m => m.device.token == token && m.owner.username == username)
                                            .OrderByDescending(m => m.id)
                                            .AsQueryable();
            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            MeasurementViewModel viewModel = new MeasurementViewModel
            {
                PageViewModel = pageViewModel,
                measurements = items
            };
            _logger.LogInformation($"Returning measurements of device {token} and user {username}; page {page}");
            return viewModel;
        }

        public MeasurementViewModel GetMeasurementsOfUser(string username, int page)
        {
            int pageSize = 50;
            IQueryable<Measurement> source = _db.measurements
                                            .Include(m => m.owner)
                                            .Include(m => m.device)
                                            .Where(m => m.owner.username == username)
                                            .OrderByDescending(m => m.id)
                                            .AsQueryable();
            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            MeasurementViewModel viewModel = new MeasurementViewModel
            {
                PageViewModel = pageViewModel,
                measurements = items
            };
            _logger.LogInformation($"Returning measurements of user {username}; page {page}");
            return viewModel;
        }

        public void DeleteMeasurement(long id, string username)
        {
            Measurement measurement = _db.measurements
                .Include(m => m.owner)
                .FirstOrDefault(m => m.id == id && m.owner.username == username);
            if (measurement == null)
                return;
            _logger.LogInformation($"Deleted measurement with id {id} and user {username}");
            _db.measurements.Remove(measurement);
            _db.SaveChanges();
        }
    }
}
