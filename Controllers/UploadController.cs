using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Dtos;
using MovieApi.Service;
using MovieApi.Services.DataServices;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace MovieApi.Controllers
{
    /// <summary>
    /// Upload Controller
    /// </summary>
    [Route("api/movie-api/v1/upload")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private static Serilog.ILogger Logger => Serilog.Log.ForContext<UploadController>();

        private readonly CommonService _dataService;
        private readonly MinioService _minioService;

        /// <summary>
        /// Constructor
        /// </summary>
        public UploadController(CommonService dataService, MinioService minioService)
        {
            _dataService = dataService;
            _minioService = minioService;
        }

        /// <summary>
        /// Upload Screenshot
        /// </summary>
        /// <param name="uploadDto"></param>
        /// <response code="201">OK</response>
        [HttpPost]
        [Route("screenshot")]
        [AllowAnonymous]
        [SwaggerResponse(statusCode: 201, description: "OK")]
        public async Task<IActionResult> UploadScreenshot([FromForm] UploadDto uploadDto)
        {
            var isExists = await _dataService.ContentExistAsync(uploadDto.ContentId);

            if (!isExists)
                return StatusCode(404, new ErrorDto("Content not found", "404"));

            string filePath = await _minioService.UploadImage(uploadDto.File);

            await _dataService.AddScreenshotAsync(uploadDto.ContentId, filePath);

            return StatusCode(201);
        }

        /// <summary>
        /// Upload Poster
        /// </summary>
        /// <param name="uploadDto"></param>
        /// <response code="201">OK</response>
        [HttpPost]
        [Route("poster")]
        [AllowAnonymous]
        [SwaggerResponse(statusCode: 201, description: "OK")]
        public async Task<IActionResult> UploadPoster([FromForm] UploadDto uploadDto)
        {
            var isExists = await _dataService.ContentExistAsync(uploadDto.ContentId);

            if (!isExists)
                return StatusCode(404, new ErrorDto("Content not found", "404"));

            string filePath = await _minioService.UploadImage(uploadDto.File);

            await _dataService.AddPosterAsync(uploadDto.ContentId, filePath);

            return StatusCode(201);
        }

        /// <summary>
        /// Upload Banner
        /// </summary>
        /// <param name="uploadDto"></param>
        /// <response code="201">OK</response>
        [HttpPost]
        [Route("banner")]
        [AllowAnonymous]
        [SwaggerResponse(statusCode: 201, description: "OK")]
        public async Task<IActionResult> UploadBanner([FromForm] UploadDto uploadDto)
        {
            var isExists = await _dataService.ContentExistAsync(uploadDto.ContentId);

            if (!isExists)
                return StatusCode(404, new ErrorDto("Content not found", "404"));

            string filePath = await _minioService.UploadImage(uploadDto.File);

            await _dataService.AddBannerAsync(uploadDto.ContentId, filePath);

            return StatusCode(201);
        }

        /// <summary>
        /// Delete Screenshot
        /// </summary>
        /// <param name="id"></param>
        /// <param name="path"></param>
        /// <response code="200">OK</response>
        [HttpDelete]
        [Route("screenshot/{id}/{path}")]
        [AllowAnonymous]
        [SwaggerResponse(statusCode: 200, description: "OK")]
        public async Task<IActionResult> DeleteScreenshot([FromRoute(Name = "id")][Required] string id, [FromRoute(Name = "path")][Required] string path)
        {
            var isExists = await _dataService.ContentExistAsync(id);

            if (!isExists)
                return StatusCode(404, new ErrorDto("Content not found", "404"));

            var isDeleted = await _dataService.RemoveScreenshotAsync(id, path);

            if (!isDeleted)
                return StatusCode(404, new ErrorDto("Screenshot not found", "404"));

            await _minioService.RemoveImage(path);

            return StatusCode(200);
        }

        /// <summary>
        /// Delete Poster
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">OK</response>
        [HttpDelete]
        [Route("poster/{id}")]
        [AllowAnonymous]
        [SwaggerResponse(statusCode: 200, description: "OK")]
        public async Task<IActionResult> DeletePoster([FromRoute(Name = "id")][Required] string id)
        {
            var isExists = await _dataService.ContentExistAsync(id);

            if (!isExists)
                return StatusCode(404, new ErrorDto("Content not found", "404"));

            var preview = await _dataService.GetPreviewsByIdAsync(id);

            if (preview.PosterPath == null)
                return StatusCode(404, new ErrorDto("Poster not found", "404"));

            await _dataService.RemovePosterAsync(id);
            await _minioService.RemoveImage(preview.PosterPath!);

            return StatusCode(200);
        }

        /// <summary>
        /// Delete Banner
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">OK</response>
        [HttpDelete]
        [Route("banner/{id}")]
        [AllowAnonymous]
        [SwaggerResponse(statusCode: 200, description: "OK")]
        public async Task<IActionResult> DeleteBanner([FromRoute(Name = "id")][Required] string id)
        {
            var isExists = await _dataService.ContentExistAsync(id);

            if (!isExists)
                return StatusCode(404, new ErrorDto("Content not found", "404"));

            var preview = await _dataService.GetPreviewsByIdAsync(id);

            if (preview.BannerPath == null)
                return StatusCode(404, new ErrorDto("Banner not found", "404"));

            await _dataService.RemoveBannerAsync(id);
            await _minioService.RemoveImage(preview.BannerPath!);

            return StatusCode(200);
        }
    }
}
