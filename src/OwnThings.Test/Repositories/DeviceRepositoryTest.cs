using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using OwnThings.Core.Data;
using OwnThings.Core.Models;
using OwnThings.Core.Repositories.Interfaces;
using OwnThings.Core.Repositories.Mocks;
using OwnThings.Core.Seed;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OwnThings.Test.Repositories
{
    public class DeviceRepositoryTest
    {
        private Mock<IDeviceRepository> _deviceRepository;

        public DeviceRepositoryTest()
        {
            _deviceRepository = new Mock<IDeviceRepository>();
        }

        [Fact]
        public void GetDeviceByIdTest()
        {
            _deviceRepository.Setup(m => m.GetDeviceById(1L, "user")).Returns(new Device());
            Device device = _deviceRepository.Object.GetDeviceById(1L, "user");

            Assert.NotNull(device);
        }


        [Fact]
        public void GetDevicesTest()
        {
            _deviceRepository.Setup(m => m.GetDevices("user")).Returns(new List<Device>());
            List<Device> device = _deviceRepository.Object.GetDevices("user");

            Assert.NotNull(device);
        }
    }
}
