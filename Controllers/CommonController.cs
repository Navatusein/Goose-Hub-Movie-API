using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Dtos;
using MovieApi.Models;
using MovieApi.Services.DataServices;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace MovieApi.Controllers
{
    /// <summary>
    /// Common Controller
    /// </summary>
    [Route("v1/content")]
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
        [SwaggerResponse(statusCode: 200, type: typeof(PaginationDto), description: "OK")]
        public async Task<IActionResult> GetContentQuery([FromBody] QueryDto query)
        {
            var models = await _dataService.GetPreviewsByQueryAsync(query);
            var totalCount = await _dataService.CountPreviewsByQueryAsync(query);

            var dtos = models.Select(x => _mapper.Map<PreviewDto>(x)).ToList();

            var paginationDto = new PaginationDto() { 
                Page = query.Page,
                PageSize = query.PageSize,
                TotalCount = totalCount,
                ReturnedCount = dtos.Count,
                Data = dtos
            };

            return StatusCode(200, paginationDto);
        }

        /// <summary>
        /// Get Content By Ids
        /// </summary>
        /// <param name="ids"></param>
        /// <response code="200">OK</response>
        [HttpPost]
        [Route("ids")]
        [AllowAnonymous]
        [SwaggerResponse(statusCode: 200, type: typeof(List<PreviewDto>), description: "OK")]
        public async Task<IActionResult> GetContentIds([FromBody] List<string> ids)
        {
            var models = await _dataService.GetPreviewsByIdsAsync(ids);
            var dtos = models.Select(x => _mapper.Map<PreviewDto>(x)).ToList();

            return StatusCode(200, dtos);
        }

        /// <summary>
        /// Get Content By Ids
        /// </summary>
        /// <response code="200">OK</response>
        [HttpPost]
        [Route("test/{id}")]
        [AllowAnonymous]
        [SwaggerResponse(statusCode: 200, type: typeof(PreviewDto), description: "OK")]
        public async Task<IActionResult> Test([FromRoute] string id, [FromServices] MovieService movieService)
        {
            var preview = await _dataService.GetPreviewsByIdAsync(id);

            if ((int)preview.DataType == 1)
            {
                var model = await movieService.GetByIdAsync(id);
                PreviewDto dto = _mapper.Map<MovieDto>(model);
                return StatusCode(200, dto);
            }

            return StatusCode(200);
        }
    }
}
