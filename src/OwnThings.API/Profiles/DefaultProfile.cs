using AutoMapper;
using OwnThings.API.Payload.Response;
using OwnThings.Core.Models;
using OwnThings.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnThings.API.Profiles
{
    public class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            CreateMap<User, UserResponse>();
            CreateMap<Device, DeviceResponse>();
            CreateMap<Measurement, MeasurementResponse>();
            CreateMap<PageViewModel, PageResponse>();
            CreateMap<MeasurementViewModel, MeasurementPageResponse>();
        }
    }
}
