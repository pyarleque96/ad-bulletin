using AdBulletin.Domain.Data.Entities;
using AdBulletin.Infrastructure.Transport;
using AutoMapper;

namespace AdBulletin.WebAPI.Maps
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();
            CreateMap<AdCategory, AdCategoryDto>().ReverseMap();
            CreateMap<Ad, AdDto>().ReverseMap();
        }
    }
}
