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
                .ForMember(dest => dest.PosterUrl, opt => opt.MapFrom<UrlResolver, string>(src => src.PosterPath))
                .ReverseMap();

            CreateMap<Anime, AnimeDto>()
                .ForMember(dest => dest.ScreenshotUrls, opt => opt.MapFrom<ListUrlResolver, List<string>>(src => src.ScreenshotPath))
                .ReverseMap();

            CreateMap<Movie, MovieDto>()
                .ForMember(dest => dest.ScreenshotUrls, opt => opt.MapFrom<ListUrlResolver, List<string>>(src => src.ScreenshotPath))
                .ReverseMap();

            CreateMap<Serial, SerialDto>()
                .ForMember(dest => dest.ScreenshotUrls, opt => opt.MapFrom<ListUrlResolver, List<string>>(src => src.ScreenshotPath))
                .ReverseMap();

            CreateMap<Season, SeasonDto>().ReverseMap();

            CreateMap<Episode, EpisodeDto>().ReverseMap();

            CreateMap<Content, ContentDto>()
                .ForMember(dest => dest.Url, opt => opt.MapFrom<UrlResolver, string>(src => src.Path))
                .ReverseMap();
        }
    }
}
