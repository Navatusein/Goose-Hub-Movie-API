using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Dtos;
using MovieApi.MassTransit.Events;
using MovieApi.Models;
using MovieApi.Service;
using MovieApi.Services.DataServices;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;

namespace MovieApi.Controllers
{
    /// <summary>
    /// Info Controller
    /// </summary>
    [Route("v1/info")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private static Serilog.ILogger Logger => Serilog.Log.ForContext<InfoController>();

        private readonly PreviewService _commonService;

        /// <summary>
        /// Constructor
        /// </summary>
        public InfoController(PreviewService commonService)
        {
            _commonService = commonService;
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
        public async Task<IActionResult> GetInfoDirectedBy([FromQuery] string? query)
        {
            var list = await _commonService.GetDirectedByAsync(query ?? "");
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
        public async Task<IActionResult> GetInfoGenres([FromQuery]ContentTypeEnum? contentType)
        {
            var list = await _commonService.GetGenresAsync(contentType);
            return StatusCode(200, list);
        }

        /// <summary>
        /// Get Years
        /// </summary>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("years")]
        [AllowAnonymous]
        [SwaggerResponse(statusCode: 200, type: typeof(YearsInfoDto), description: "OK")]
        public async Task<IActionResult> GetInfoYears([FromQuery] ContentTypeEnum? contentType)
        {
            var list = await _commonService.GetYearsAsync(contentType);

            var yearInfoDto = new YearsInfoDto()
            {
                MinYear = list.Min(),
                MaxYear = list.Max()
            };

            return StatusCode(200, yearInfoDto);
        }
    }
}
