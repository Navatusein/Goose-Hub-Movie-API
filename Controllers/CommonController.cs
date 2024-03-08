using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Dto;
using MovieApi.Service;
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

        /// <summary>
        /// Get Content By Franchise
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [HttpGet]
        [Route("franchise/{id}")]
        [AllowAnonymous]
        [SwaggerResponse(statusCode: 200, type: typeof(List<PreviewDto>), description: "OK")]
        [SwaggerResponse(statusCode: 404, type: typeof(ErrorDto), description: "Not Found")]
        public async Task<IActionResult> GetContentFranchiseId([FromRoute(Name = "id")][Required] string id)
        {

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(List<PreviewDto>));
            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(ErrorDto));

            throw new NotImplementedException();
        }

        /// <summary>
        /// Get Content By Query
        /// </summary>
        /// <param name="query"></param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("query")]
        [AllowAnonymous]
        [SwaggerResponse(statusCode: 200, type: typeof(PreviewDto), description: "OK")]
        public async Task<IActionResult> GetQuery([FromBody] QueryDto query)
        {

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(PreviewDto));

            throw new NotImplementedException();
        }
    }
}
