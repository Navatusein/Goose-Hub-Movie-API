using AutoMapper;
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
                .ForMember(dest => dest.PosterUrl, opt => opt.MapFrom<ImageUrlResolver, string>(src => src.PosterPath))
                .ForMember(dest => dest.BannerUrl, opt => opt.MapFrom<ImageUrlResolver, string>(src => src.BannerPath))
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

            CreateMap<Season, SeasonDto>().ReverseMap();

            CreateMap<Episode, EpisodeDto>().ReverseMap();

            CreateMap<Content, ContentDto>()
                .ForMember(dest => dest.Url, opt => opt.MapFrom<ContentUrlResolver, string>(src => src.Path))
                .ReverseMap();
        }
    }
}
