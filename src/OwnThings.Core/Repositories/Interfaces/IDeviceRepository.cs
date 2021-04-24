using OwnThings.Core.Models;
using OwnThings.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace OwnThings.Core.Repositories.Interfaces
{
    public interface IDeviceRepository
    {
        List<Device> GetDevices(string username);
        List<Device> SearchDevice(string name, string username);
        Device GetDeviceById(long id, string username);
        Device GetDeviceByToken(string token, string username);
        Device CreateDevice(string name, string token, string username);
        void DeleteDevice(string token, string username);
    }
}
