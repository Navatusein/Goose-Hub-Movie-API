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
    /// Franchise Controller
    /// </summary>
    [Route("api/movie-api/v1/franchise")]
    [ApiController]
    public class FranchiseController : ControllerBase
    {
        private static Serilog.ILogger Logger => Serilog.Log.ForContext<FranchiseController>();

        private readonly IMapper _mapper;
        private readonly FranchiseService _dataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public FranchiseController(IMapper mapper, FranchiseService dataService)
        {
            _mapper = mapper;
            _dataService = dataService;
        }

        /// <summary>
        /// Get Franchise By Id
        /// </summary>
        /// <param name="id">Franchise Id</param>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse(statusCode: 200, type: typeof(FranchiseDto), description: "OK")]
        [SwaggerResponse(statusCode: 404, type: typeof(ErrorDto), description: "Not Found")]
        public async Task<IActionResult> Get([FromRoute(Name = "id")][Required] string id)
        {
            var model = await _dataService.GetAsync(id);

            if (model == null)
                return StatusCode(404, new ErrorDto("Franchise not found", "404"));

            var dto = _mapper.Map<FranchiseDto>(model);

            return StatusCode(200, dto);
        }

        /// <summary>
        /// Create Franchise
        /// </summary>
        /// <param name="franchiseDto"></param>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(statusCode: 201, type: typeof(FranchiseDto), description: "Created")]
        public async Task<IActionResult> Post([FromBody] FranchiseDto franchiseDto)
        {
            var model = await _dataService.CreateAsync(_mapper.Map<Franchise>(franchiseDto));
            var dto = _mapper.Map<FranchiseDto>(model);

            return StatusCode(201, dto);
        }

        /// <summary>
        /// Update Franchise
        /// </summary>
        /// <param name="id">Franchise Id</param>
        /// <param name="franchiseDto"></param>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(statusCode: 200, type: typeof(FranchiseDto), description: "OK")]
        [SwaggerResponse(statusCode: 404, type: typeof(ErrorDto), description: "Not Found")]
        public async Task<IActionResult> Put([FromRoute(Name = "id")][Required] string id, [FromBody] FranchiseDto franchiseDto)
        {
            var model = await _dataService.UpdateAsync(id, _mapper.Map<Franchise>(franchiseDto));

            if (model == null)
                return StatusCode(404, new ErrorDto("Franchise not found", "404"));

            var dto = _mapper.Map<FranchiseDto>(model);

            return StatusCode(200, dto);
        }

        /// <summary>
        /// Delete Franchise
        /// </summary>
        /// <param name="id">Franchise Id</param>
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
                return StatusCode(404, new ErrorDto("Franchise not found", "404"));

            return StatusCode(200);
        }
    }
}
