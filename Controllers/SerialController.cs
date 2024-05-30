using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Dtos;
using MovieApi.Models;
using MovieApi.Service;
using MovieApi.Services.DataServices;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MovieApi.Controllers
{
    /// <summary>
    /// Serial Controller
    /// </summary>
    [Route("v1/serial")]
    [ApiController]
    public class SerialController : ControllerBase
    {
        private static Serilog.ILogger Logger => Serilog.Log.ForContext<SerialController>();

        private readonly IMapper _mapper;
        private readonly SerialService _dataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public SerialController(IMapper mapper, SerialService dataService)
        {
            _mapper = mapper;
            _dataService = dataService;
        }

        /// <summary>
        /// Get Serial By Id
        /// </summary>
        /// <param name="id">Serial Id</param>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse(statusCode: 200, type: typeof(SerialDto), description: "OK")]
        [SwaggerResponse(statusCode: 404, type: typeof(ErrorDto), description: "Not Found")]
        public async Task<IActionResult> Get([FromRoute(Name = "id")][Required] string id)
        {
            var model = await _dataService.GetByIdAsync(id);

            if (model == null)
                return StatusCode(404, new ErrorDto("Serial not found", "404"));

            var dto = _mapper.Map<SerialDto>(model);

            return StatusCode(200, dto);
        }

        /// <summary>
        /// Create Serial
        /// </summary>
        /// <param name="serialDto"></param>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(statusCode: 201, type: typeof(SerialDto), description: "Created")]
        public async Task<IActionResult> Post([FromBody] SerialDto serialDto)
        {
            var model = await _dataService.CreateAsync(_mapper.Map<Serial>(serialDto));
            var dto = _mapper.Map<SerialDto>(model);

            return StatusCode(201, model);
        }

        /// <summary>
        /// Update Serial
        /// </summary>
        /// <param name="id">Serial Id</param>
        /// <param name="serialDto"></param>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(statusCode: 200, type: typeof(SerialDto), description: "OK")]
        [SwaggerResponse(statusCode: 404, type: typeof(ErrorDto), description: "Not Found")]
        public async Task<IActionResult> Put([FromRoute(Name = "id")][Required] string id, [FromBody] SerialDto serialDto)
        {
            var model = await _dataService.UpdateAsync(id, _mapper.Map<Serial>(serialDto));

            if (model == null)
                return StatusCode(404, new ErrorDto("Serial not found", "404"));

            var dto = _mapper.Map<SerialDto>(model);

            return StatusCode(200, dto);
        }

        /// <summary>
        /// Delete Serial
        /// </summary>
        /// <param name="id">Serial Id</param>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(statusCode: 200, description: "OK")]
        public async Task<IActionResult> Delete([FromRoute(Name = "id")][Required] string id)
        {
            var isDeleted = await _dataService.DeleteAsync(id);

            if (!isDeleted)
                return StatusCode(404, new ErrorDto("Serial not found", "404"));

            return StatusCode(200);
        }
    }
}
