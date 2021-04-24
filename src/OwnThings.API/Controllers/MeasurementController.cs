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
using OwnThings.Core.ViewModels;

namespace OwnThings.API.Controllers
{
    [Route("api/v1/measurements/")]
    [ApiController]
    [Authorize]
    public class MeasurementController : ControllerBase
    {
        private readonly IMeasurementRepository _measurementRepository;
        private readonly IMapper _mapper;
        public MeasurementController(IMeasurementRepository measurementRepository, IMapper mapper)
        {
            _measurementRepository = measurementRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all measurements of user
        /// </summary>
        /// <param name="page">page</param>
        /// <returns>List of measurements</returns>
        [HttpGet("")]
        public MeasurementPageResponse GetMeasurementOfUser([FromQuery] int page = 1)
        {
            MeasurementViewModel measurements = _measurementRepository.GetMeasurementsOfUser(HttpContext.User.Identity.Name, page);
            return _mapper.Map<MeasurementPageResponse>(measurements);
        }

        /// <summary>
        /// Get measurements of deviec by token
        /// </summary>
        /// <param name="token">device token</param>
        /// <param name="page">page</param>
        /// <returns>List of device measurements</returns>
        [HttpGet("{token}")]
        public MeasurementPageResponse GetMeasurementsOfDevice([FromRoute]string token, [FromQuery] int page = 1)
        {
            MeasurementViewModel measurements = _measurementRepository.GetMeasurementsOfDevice(token, HttpContext.User.Identity.Name, page);
            return _mapper.Map<MeasurementPageResponse>(measurements);
        }

        /// <summary>
        /// Create measurement
        /// </summary>
        /// <param name="token">device token</param>
        /// <param name="measurementRequest">payload</param>
        /// <returns>status code</returns>
        [HttpPost("{token}")]
        public IActionResult CreateMeasurement([FromRoute] string token, [FromBody] CreateMeasurementRequest measurementRequest)
        {
            Measurement measurement = _measurementRepository.CreateMeasurement(measurementRequest.payload, token, HttpContext.User.Identity.Name);
            if (measurement == null)
                return BadRequest(new { statusCode = 400, message = "bad_request" });
            return StatusCode(201, new { statusCode = 201, message = "created" });
        }

        /// <summary>
        /// Delete measurement
        /// </summary>
        /// <param name="id">measurement id</param>
        /// <returns>status code</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteMeasurement([FromRoute] long id)
        {
            _measurementRepository.DeleteMeasurement(id, HttpContext.User.Identity.Name);
            return StatusCode(200, new { statusCode = 200, message = "deleted" });
        }
    }
}