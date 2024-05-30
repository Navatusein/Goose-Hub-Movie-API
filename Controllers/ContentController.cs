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
    public class ContentController : ControllerBase
    {
        private static Serilog.ILogger Logger => Serilog.Log.ForContext<ContentController>();

        private readonly IMapper _mapper;
        private readonly PreviewService _previewService;
        private readonly MovieService _movieService;
        private readonly SerialService _serialService;
        private readonly AnimeService _animeService;

        /// <summary>
        /// Constructor
        /// </summary>
        public ContentController(IMapper mapper, PreviewService previewService, MovieService movieService, SerialService serialService, AnimeService animeService)
        {
            _mapper = mapper;
            _previewService = previewService;
            _movieService = movieService;
            _serialService = serialService;
            _animeService = animeService;
        }

        /// <summary>
        /// Get Preview By Franchise
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("preview/franchise/{id}")]
        [AllowAnonymous]
        [SwaggerResponse(statusCode: 200, type: typeof(List<PreviewDto>), description: "OK")]
        public async Task<IActionResult> GetContentFranchiseId([FromRoute(Name = "id")][Required] string id)
        {
            var models = await _previewService.GetPreviewsByFranchiseAsync(id);
            var dtos = models.Select(x => _mapper.Map<PreviewDto>(x)).ToList();

            return StatusCode(200, dtos);
        }

        /// <summary>
        /// Get Preview By Query
        /// </summary>
        /// <param name="query"></param>
        /// <response code="200">OK</response>
        [HttpPost]
        [Route("preview/query")]
        [AllowAnonymous]
        [SwaggerResponse(statusCode: 200, type: typeof(PaginationDto), description: "OK")]
        public async Task<IActionResult> GetContentQuery([FromBody] QueryDto query)
        {
            var models = await _previewService.GetPreviewsByQueryAsync(query);
            var totalCount = await _previewService.CountPreviewsByQueryAsync(query);

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
        /// Get Preview By Ids
        /// </summary>
        /// <param name="ids"></param>
        /// <response code="200">OK</response>
        [HttpPost]
        [Route("preview/ids")]
        [AllowAnonymous]
        [SwaggerResponse(statusCode: 200, type: typeof(List<PreviewDto>), description: "OK")]
        public async Task<IActionResult> GetContentIds([FromBody] List<string> ids)
        {
            var models = await _previewService.GetPreviewsByIdsAsync(ids);
            var dtos = models.Select(x => _mapper.Map<PreviewDto>(x)).ToList();

            return StatusCode(200, dtos);
        }


        /// <summary>
        /// Get Content By Id
        /// </summary>
        /// <response code="200">OK</response>
        [HttpPost]
        [Route("{id}")]
        [AllowAnonymous]
        [SwaggerResponse(statusCode: 200, type: typeof(PreviewDto), description: "OK")]
        public async Task<IActionResult> Test([FromRoute] string id)
        {
            var preview = await _previewService.GetPreviewsByIdAsync(id);

            Preview model = null!;
            PreviewDto previewDto = null!;

            switch (preview.DataType)
            {
                case DataTypeEnum.Movie:
                    model = await _movieService.GetByIdAsync(id);
                    previewDto = _mapper.Map<MovieDto>(model);
                    break;
                case DataTypeEnum.Serial:
                    model = await _serialService.GetByIdAsync(id);
                    previewDto = _mapper.Map<SerialDto>(model);
                    break;
                case DataTypeEnum.Anime:
                    model = await _animeService.GetByIdAsync(id);
                    previewDto = _mapper.Map<AnimeDto>(model);
                    break;
            }

            return StatusCode(200, previewDto);
        }
    }
}
