using AutoMapper;
using EFCoreRelationSample.Models;

namespace EFCoreRelationSample.DTO
{
    public class MyProfile : Profile
    {
        public MyProfile()
        {
            CreateMap<BlogOfFk, BlogOfFkDTO>();
            CreateMap<PostOfFk, PostOfFkDTO>();
        }
    }
}