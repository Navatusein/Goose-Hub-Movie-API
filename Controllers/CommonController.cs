using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Dtos;
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
    [Route("/api/movie-api/v1/content")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private static Serilog.ILogger Logger => Serilog.Log.ForContext<CommonController>();

        private readonly IMapper _mapper;
        private readonly CommonService _dataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public CommonController(IMapper mapper, CommonService dataService)
        {
            _mapper = mapper;
            _dataService = dataService;
        }

        /// <summary>
        /// Get Content By Franchise
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("franchise/{id}")]
        [AllowAnonymous]
        [SwaggerResponse(statusCode: 200, type: typeof(List<PreviewDto>), description: "OK")]
        public async Task<IActionResult> GetContentFranchiseId([FromRoute(Name = "id")][Required] string id)
        {
            var models = await _dataService.GetPreviewsByFranchiseAsync(id);
            var dtos = models.Select(x => _mapper.Map<PreviewDto>(x)).ToList();

            return StatusCode(200, dtos);
        }

        /// <summary>
        /// Get Content By Query
        /// </summary>
        /// <param name="query"></param>
        /// <response code="200">OK</response>
        [HttpPost]
        [Route("query")]
        [AllowAnonymous]
        [SwaggerResponse(statusCode: 200, type: typeof(List<PreviewDto>), description: "OK")]
        public async Task<IActionResult> GetQuery([FromBody] QueryDto query)
        {
            var models = await _dataService.GetPreviewsByQueryAsync(query);
            var dtos = models.Select(x => _mapper.Map<PreviewDto>(x)).ToList();

            return StatusCode(200, dtos);
        }
    }
}
