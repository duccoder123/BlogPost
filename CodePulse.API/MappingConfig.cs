using AutoMapper;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTOs;

namespace CodePulse.API
{
    public class MappingConfig : Profile
    {
        public static MapperConfiguration RegisterMap()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CategoryDto, Category>().ReverseMap();
                config.CreateMap<CreateBlogPostRequestDto, BlogPost>().ReverseMap();
                config.CreateMap<BlogPost, BlogPostDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
