using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Dto;
using MovieApi.Service;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MovieApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/movie-api/v1/movie")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private static Serilog.ILogger Logger => Serilog.Log.ForContext<MovieController>();

        /// <summary>
        /// Get Movie By Id
        /// </summary>
        /// <param name="id">Movie Id</param>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse(statusCode: 200, type: typeof(MovieDto), description: "OK")]
        [SwaggerResponse(statusCode: 404, type: typeof(ErrorDto), description: "Not Found")]
        public async Task<IActionResult> Get([FromRoute(Name = "id")][Required] string id)
        {

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(MovieDto));
            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(ErrorDto));

            throw new NotImplementedException();
        }

        /// <summary>
        /// Create Movie
        /// </summary>
        /// <param name="movieDto"></param>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(statusCode: 201, type: typeof(MovieDto), description: "Created")]
        public async Task<IActionResult> Post([FromBody] MovieDto movieDto)
        {

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(201, default(MovieDto));

            throw new NotImplementedException();
        }

        /// <summary>
        /// Update Movie
        /// </summary>
        /// <param name="id">Movie Id</param>
        /// <param name="movieDto"></param>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(statusCode: 200, type: typeof(MovieDto), description: "OK")]
        [SwaggerResponse(statusCode: 404, type: typeof(ErrorDto), description: "Not Found")]
        public async Task<IActionResult> Put([FromRoute(Name = "id")][Required] string id, [FromBody] MovieDto movieDto)
        {

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(MovieDto));
            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(ErrorDto));

            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete Movie
        /// </summary>
        /// <param name="id">Movie Id</param>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute(Name = "id")][Required] string id)
        {

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200);
            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);

            throw new NotImplementedException();
        }
    }
}
