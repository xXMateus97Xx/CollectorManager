using AutoMapper;
using CollectorManager.Data.Domains;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CollectorManager;

public class MappingConfiguration : Profile
{
    public MappingConfiguration()
    {
        CreateProjection<CollectionFormat, SelectListItem>()
            .ForMember(x => x.Value, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Text, x => x.MapFrom(y => y.Name));

        CreateProjection<CollectionAuthor, SelectListItem>()
            .ForMember(x => x.Value, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Text, x => x.MapFrom(y => y.Name));
    }
}
