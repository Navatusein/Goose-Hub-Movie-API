using AutoMapper;
using MongoDB.Bson;
using MovieApi.Dtos;
using MovieApi.Models;
using MovieApi.Service;

namespace MovieApi.AppMapping
{
    /// <summary>
    /// AppMappingService
    /// </summary>
    public class AppMappingService : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AppMappingService()
        {
            CreateMap<Preview, PreviewDto>()
                .ForMember(dest => dest.PosterUrl, opt => opt.MapFrom<ImageUrlResolver, string?>(src => src.PosterPath))
                .ForMember(dest => dest.BannerUrl, opt => opt.MapFrom<ImageUrlResolver, string?>(src => src.BannerPath))
                .ReverseMap();

            CreateMap<Anime, AnimeDto>()
                .ForMember(dest => dest.ScreenshotUrls, opt => opt.MapFrom<ImageListUrlResolver, List<string>>(src => src.ScreenshotPath))
                .ReverseMap();

            CreateMap<Movie, MovieDto>()
                .ForMember(dest => dest.ScreenshotUrls, opt => opt.MapFrom<ImageListUrlResolver, List<string>>(src => src.ScreenshotPath))
                .ReverseMap();

            CreateMap<Serial, SerialDto>()
                .ForMember(dest => dest.ScreenshotUrls, opt => opt.MapFrom<ImageListUrlResolver, List<string>>(src => src.ScreenshotPath))
                .ReverseMap();

            CreateMap<Season, SeasonDto>()
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id ?? ObjectId.GenerateNewId().ToString()));

            CreateMap<Episode, EpisodeDto>()
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id ?? ObjectId.GenerateNewId().ToString()));

            CreateMap<Content, ContentDto>()
                .ForMember(dest => dest.Url, opt => opt.MapFrom<ContentUrlResolver, string>(src => src.Path))
                .ReverseMap();

            CreateMap<Franchise, FranchiseDto>()
                .ReverseMap();
        }
    }
}
