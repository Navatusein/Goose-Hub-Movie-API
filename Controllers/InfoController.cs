using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Service;
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

        /// <summary>
        /// Get Directed By
        /// </summary>
        /// <param name="query"></param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("directed-by")]
        [AllowAnonymous]
        [SwaggerResponse(statusCode: 200, type: typeof(List<string>), description: "OK")]
        public async Task<IActionResult> GetInfoDirectedBy([FromBody] string query)
        {

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(List<string>));

            throw new NotImplementedException();
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
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(List<string>));

            throw new NotImplementedException();
        }

        /// <summary>
        /// Get Years
        /// </summary>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("years")]
        [AllowAnonymous]
        [SwaggerResponse(statusCode: 200, type: typeof(List<string>), description: "OK")]
        public async Task<IActionResult> GetInfoYears()
        {

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(List<string>));

            throw new NotImplementedException();
        }
    }
}
