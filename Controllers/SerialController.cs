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
    [Route("api/movie-api/v1/serial")]
    [ApiController]
    public class SerialController : ControllerBase
    {
        private static Serilog.ILogger Logger => Serilog.Log.ForContext<SerialController>();

        /// <summary>
        /// Get Serial By Id
        /// </summary>
        /// <param name="id">Serial Id</param>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse(statusCode: 200, type: typeof(SerialDto), description: "OK")]
        [SwaggerResponse(statusCode: 404, type: typeof(ErrorDto), description: "Not Found")]
        public async Task<IActionResult> Get([FromRoute(Name = "id")][Required] string id)
        {

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(SerialDto));
            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(ErrorDto));

            throw new NotImplementedException();
        }

        /// <summary>
        /// Create Serial
        /// </summary>
        /// <param name="serialDto"></param>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(statusCode: 201, type: typeof(SerialDto), description: "Created")]
        public async Task<IActionResult> Post([FromBody] SerialDto serialDto)
        {

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(201, default(SerialDto));

            throw new NotImplementedException();
        }

        /// <summary>
        /// Update Serial
        /// </summary>
        /// <param name="id">Serial Id</param>
        /// <param name="serialDto"></param>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(statusCode: 200, type: typeof(SerialDto), description: "OK")]
        [SwaggerResponse(statusCode: 404, type: typeof(ErrorDto), description: "Not Found")]
        public async Task<IActionResult> Put([FromRoute(Name = "id")][Required] string id, [FromBody] SerialDto serialDto)
        {

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(SerialDto));
            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(ErrorDto));

            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete Serial
        /// </summary>
        /// <param name="id">Serial Id</param>
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
