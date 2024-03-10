using Amazon.Auth.AccessControlPolicy;
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
using System.Text.Json;

namespace MovieApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/movie-api/v1/anime")]
    [ApiController]
    public class AnimeController : ControllerBase
    {
        private static Serilog.ILogger Logger => Serilog.Log.ForContext<AnimeController>();

        private readonly IMapper _mapper;
        private readonly AnimeService _dataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public AnimeController(IMapper mapper, AnimeService dataService)
        {
            _mapper = mapper;
            _dataService = dataService;
        }

        /// <summary>
        /// Get Anime By Id
        /// </summary>
        /// <param name="id">Anime Id</param>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse(statusCode: 200, type: typeof(AnimeDto), description: "OK")]
        [SwaggerResponse(statusCode: 404, type: typeof(ErrorDto), description: "Not Found")]
        public async Task<IActionResult> Get([FromRoute(Name = "id")][Required] string id)
        {
            var model = await _dataService.GetAsync(id);

            if (model == null)
                return StatusCode(404, new ErrorDto("Anime not found", "404"));

            var dto = _mapper.Map<AnimeDto>(model);

            return StatusCode(200, dto);
        }

        /// <summary>
        /// Create Anime
        /// </summary>
        /// <param name="animeDto"></param>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(statusCode: 201, type: typeof(AnimeDto), description: "Created")]
        public async Task<IActionResult> Post([FromBody] AnimeDto animeDto)
        {
            var model = await _dataService.CreateAsync(_mapper.Map<Anime>(animeDto));
            var dto = _mapper.Map<AnimeDto>(model);

            return StatusCode(201, dto);
        }

        /// <summary>
        /// Update Anime
        /// </summary>
        /// <param name="id">Anime Id</param>
        /// <param name="animeDto"></param>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(statusCode: 200, type: typeof(AnimeDto), description: "OK")]
        [SwaggerResponse(statusCode: 404, type: typeof(ErrorDto), description: "Not Found")]
        public async Task<IActionResult> Put([FromRoute(Name = "id")][Required] string id, [FromBody] AnimeDto animeDto)
        {
            var model = await _dataService.UpdateAsync(id, _mapper.Map<Anime>(animeDto));

            if (model == null)
                return StatusCode(404, new ErrorDto("Anime not found", "404"));

            var dto = _mapper.Map<AnimeDto>(model);

            return StatusCode(200, dto);
        }

        /// <summary>
        /// Delete Anime
        /// </summary>
        /// <param name="id">Anime Id</param>
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
                return StatusCode(404, new ErrorDto("Anime not found", "404"));

            return StatusCode(200);
        }
    }
}
