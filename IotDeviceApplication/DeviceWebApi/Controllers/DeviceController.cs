using DeviceWebApi.Context;
using DeviceWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly DeviceContext _deviceContext;

        public DeviceController(DeviceContext deviceContext)
        {
            _deviceContext = deviceContext;
        }

        /// <summary>
        /// Gets a list of all devices
        /// </summary>
        /// <returns>List of all devices</returns>
        /// <response code="200">Returns List of all devices</response>
        /// <response code="400">If the item is null</response>  
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<DeviceMaster>> GetDevices()
        {
            var result = _deviceContext.DeviceMaster;
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        /// <summary>
        /// Gets a single device with id specified
        /// </summary>
        /// <returns>Returns a single device</returns>
        /// <response code="200">Returns result of device by id</response>
        /// <response code="404">If the result is null</response> 
        /// <param name="uid"></param>  
        [HttpGet("{uid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DeviceMaster> GetById(int uid)
        {
            var result = _deviceContext.DeviceMaster.Find(uid);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        /// <summary>
        /// Gets a reading of a given device in a period of time
        /// </summary>
        /// <returns>Returns a reading of device either temperature/humidity</returns>
        /// <response code="200">Returns result of readings of a device in a period of time</response>
        /// <response code="404">If the result is null</response> 
        /// <param name="uid"></param>  
        /// <param name="parameter"></param> 
        /// <param name="startOn"></param>  
        /// <param name="endOn"></param>  
        [HttpGet("{uid}/readings/{parameter}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetByReadingInAGivenPeriod(int uid, string parameter, [Required]DateTime startOn, [Required]DateTime endOn)
        {
            var deviceResult = _deviceContext.DeviceMaster.Find(uid);

            if (parameter == "temperature")
            {
                var tempResult = _deviceContext.TemperatureReading.Find(deviceResult.Uid);
                if (tempResult == null)
                {
                    return NotFound("Temperature Result not found");
                }

                if ((tempResult.ReadingTime > startOn) && (tempResult.ReadingTime < endOn))
                {
                    return Ok(tempResult);
                }
            }
            else if (parameter == "humidity")
            {
                var humidResult = _deviceContext.HumidityReading.Find(deviceResult.Uid);
                if(humidResult == null)
                {
                    return NotFound("Humidity Result not found");
                }
                if ((humidResult.ReadingTime > startOn) && (humidResult.ReadingTime < endOn))
                {
                    return Ok(humidResult);
                }
            }
            return BadRequest("Please provide valid parameter: temperature or humidity");
        }

        /// <summary>
        /// Deletes a single device with id specified
        /// </summary>
        /// <returns>Deletes a single device</returns>
        /// <response code="204">Returns no content</response>
        /// <response code="404">If the result is null</response>  
        /// <param name="uid"></param>  
        [HttpDelete("{uid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int uid)
        {
            var result = _deviceContext.DeviceMaster.Find(uid);

            if (result == null)
            {
                return NotFound();
            }

            _deviceContext.DeviceMaster.Remove(result);
            _deviceContext.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Creates a device
        /// </summary>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Post([FromBody] DeviceMaster device)
        {
            if (device == null)
            {
                return BadRequest(device);
            }
            _deviceContext.DeviceMaster.Add(device);
            _deviceContext.SaveChanges();

            return CreatedAtRoute("GetDevice", new { id = device.Uid }, device);
        }
    }
}
