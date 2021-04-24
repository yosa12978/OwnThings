using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OwnThings.API.Payload.Request;
using OwnThings.API.Payload.Response;
using OwnThings.Core.Models;
using OwnThings.Core.Repositories.Interfaces;

namespace OwnThings.API.Controllers
{
    [Route("api/v1/devices/")]
    [ApiController]
    [Authorize]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IMapper _mapper;
        public DeviceController(IDeviceRepository deviceRepository, IMapper mapper)
        {
            _deviceRepository = deviceRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get user devices route
        /// </summary>
        /// <returns>Current user devices</returns>
        [HttpGet("")]
        public IEnumerable<DeviceResponse> GetDevices()
        {
            List<Device> devices = _deviceRepository.GetDevices(HttpContext.User.Identity.Name);
            return devices.Select(_mapper.Map<Device, DeviceResponse>);
        }

        /// <summary>
        /// Get user device by id
        /// </summary>
        /// <param name="id">Device id</param>
        /// <returns>Device by id</returns>
        [HttpGet("{id}")]
        public DeviceResponse GetDeviceById([FromRoute]long id)
        {
            Device device = _deviceRepository.GetDeviceById(id, HttpContext.User.Identity.Name);
            return _mapper.Map<DeviceResponse>(device);
        }

        /// <summary>
        /// Search user devices by query param
        /// </summary>
        /// <param name="query">user search query param</param>
        /// <returns>searched devices</returns>
        [HttpGet("search")]
        public IEnumerable<DeviceResponse> SearchDevices([FromQuery]string query)
        {
            List<Device> devices = _deviceRepository.SearchDevice(query, HttpContext.User.Identity.Name);
            return devices.Select(_mapper.Map<Device, DeviceResponse>);
        }

        /// <summary>
        /// Get user device by secret token
        /// </summary>
        /// <param name="token">device access token</param>
        /// <returns>Device</returns>
        [HttpGet("token")]
        public DeviceResponse GetDeviceByToken([FromQuery]string token)
        {
            Device device = _deviceRepository.GetDeviceByToken(token, HttpContext.User.Identity.Name);
            return _mapper.Map<DeviceResponse>(device);
        }

        /// <summary>
        /// Create new device
        /// </summary>
        /// <param name="device">Device name</param>
        /// <returns>status code</returns>
        [HttpPost("")]
        public IActionResult CreateDevice(CreateDeviceRequest device)
        {
            Device created_device = _deviceRepository.CreateDevice(device.name, 
                Guid.NewGuid().ToString(), 
                HttpContext.User.Identity.Name);
            if (created_device == null)
                return BadRequest(new { statusCode = 400, message = "bad_request" });
            return StatusCode(201, new { statusCode = 201, message = "created" });
        }

        /// <summary>
        /// Delete device
        /// </summary>
        /// <param name="token">device token</param>
        /// <returns>status code</returns>
        [HttpDelete("{token}")]
        public IActionResult DeleteDevice([FromRoute] string token)
        {
            _deviceRepository.DeleteDevice(token, HttpContext.User.Identity.Name);
            return StatusCode(200, new { statusCode = 200, message = "deleted" });
        }
    }
}