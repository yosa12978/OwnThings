using OwnThings.Core.Models;
using OwnThings.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace OwnThings.Core.Repositories.Interfaces
{
    public interface IMeasurementRepository
    {
        MeasurementViewModel GetMeasurementsOfUser(string username, int page);
        MeasurementViewModel GetMeasurementsOfDevice(string token, string username, int page);
        Measurement CreateMeasurement(string payload, string device, string username);
        void DeleteMeasurement(long id, string username);
    }
}
