using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Service;
using MovieApi.Services.DataServices;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;

namespace MovieApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/movie-api/v1/info")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private static Serilog.ILogger Logger => Serilog.Log.ForContext<InfoController>();
      
        private readonly CommonService _dataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public InfoController(CommonService dataService)
        {
            _dataService = dataService;
        }

        /// <summary>
        /// Get Directed By
        /// </summary>
        /// <param name="query"></param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("directed-by")]
        [AllowAnonymous]
        [SwaggerResponse(statusCode: 200, type: typeof(List<string>), description: "OK")]
        public async Task<IActionResult> GetInfoDirectedBy([FromQuery] string query)
        {

            var list = await _dataService.GetDirectedByAsync(query);
            return StatusCode(200, list);
        }

        /// <summary>
        /// Get Genres
        /// </summary>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("genres")]
        [AllowAnonymous]
        [SwaggerResponse(statusCode: 200, type: typeof(List<string>), description: "OK")]
        public async Task<IActionResult> GetInfoGenres()
        {
            var list = await _dataService.GetGenresAsync();
            return StatusCode(200, list);
        }

        /// <summary>
        /// Get Years
        /// </summary>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("years")]
        [AllowAnonymous]
        [SwaggerResponse(statusCode: 200, type: typeof(List<int>), description: "OK")]
        public async Task<IActionResult> GetInfoYears()
        {
            var list = await _dataService.GetYearsAsync();
            return StatusCode(200, list);
        }
    }
}
