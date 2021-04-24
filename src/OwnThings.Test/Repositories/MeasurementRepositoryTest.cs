using Moq;
using OwnThings.Core.Repositories.Interfaces;
using OwnThings.Core.Repositories.Mocks;
using OwnThings.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OwnThings.Test.Repositories
{
    public class MeasurementRepositoryTest
    {
        private Mock<IMeasurementRepository> mock;
        public MeasurementRepositoryTest()
        {
            mock = new Mock<IMeasurementRepository>();
        }

        [Fact]
        public void GetMeasurementsOfDevice()
        {
            mock.Setup(m => m.GetMeasurementsOfDevice("token", "user", 1)).Returns(new MeasurementViewModel());
            MeasurementViewModel measurements = mock.Object.GetMeasurementsOfDevice("token", "user", 1);

            Assert.NotNull(measurements);
        }
    }
}
