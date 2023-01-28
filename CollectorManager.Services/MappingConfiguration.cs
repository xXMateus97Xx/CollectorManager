using AutoMapper;
using CollectorManager.Data;
using CollectorManager.Data.Domains;
using CollectorManager.Services.Auth.DTOs;
using CollectorManager.Services.CollectionAuthors.DTOs;
using CollectorManager.Services.CollectionFormats.DTOs;
using CollectorManager.Services.CollectionItems.DTOs;
using CollectorManager.Services.CollectionItems.MappingResolvers;
using CollectorManager.Services.Collections.DTOs;
using CollectorManager.Services.Users.DTOs;

namespace CollectorManager.Services;

internal class MappingConfiguration : Profile
{
    public MappingConfiguration()
    {
        #region User

        CreateProjection<User, LoginResponse>();
        CreateProjection<User, UserResume>();
        CreateProjection<User, UserPassword>();
        CreateProjection<User, BaseEntityModel>();

        CreateMap<SignUpRequest, User>()
            .ForMember(x => x.Id, x => x.Ignore())
            .ForMember(x => x.Password, x => x.MapFrom(y => y.HashedPassword))
            .ForMember(x => x.PasswordSalt, x => x.MapFrom(y => y.PasswordSalt));

        CreateMap<ChangePasswordRequest, User>()
            .ForMember(x => x.Password, x => x.MapFrom(x => x.HashedPassword))
            .ForMember(x => x.PasswordSalt, x => x.MapFrom(x => x.PasswordSalt));

        #endregion

        #region Collection

        CreateProjection<Collection, CollectionResumeModel>()
            .ForMember(x => x.Items, x => x.MapFrom(y => y.Items.Count()));

        CreateProjection<Collection, CollectionModel>();

        CreateMap<CollectionModel, Collection>()
            .ForMember(x => x.Id, x => x.Ignore())
            .ForMember(x => x.UserId, x => x.MapFrom(y => y.UserId));

        #endregion

        #region Format

        CreateProjection<CollectionFormat, FormatResumeModel>();

        CreateProjection<CollectionFormat, FormatModel>();

        CreateMap<FormatModel, CollectionFormat>()
            .ForMember(x => x.Id, x => x.Ignore());

        #endregion

        #region Author

        CreateProjection<CollectionAuthor, AuthorResumeModel>();

        CreateProjection<CollectionAuthor, AuthorModel>();

        CreateMap<AuthorModel, CollectionAuthor>()
            .ForMember(x => x.Id, x => x.Ignore());


        #endregion

        #region Collection Item

        CreateProjection<CollectionItem, CollectionItemResumeModel>()
            .ForMember(x => x.FormatName, x => x.MapFrom(y => y.Format.Name))
            .ForMember(x => x.Authors, x => x.MapFrom(y => y.ItemAuthors.Select(w => w.CollectionAuthor.Name)));

        CreateProjection<CollectionItem, CollectionItemModel>()
            .ForMember(x => x.AuthorsIds, x => x.MapFrom(y => y.ItemAuthors.Select(w => w.AuthorId)));

        CreateMap<CollectionItemModel, CollectionItem>()
            .ForMember(x => x.Id, x => x.Ignore())
            .ForMember(x => x.ItemAuthors, x => x.MapFrom<CollectionItemAuthorsMapping>())
            .ForMember(x => x.LastUpdateAt, x => x.MapFrom(y => y.LastUpdateAt))
            .ForMember(x => x.CreatedAt, x => x.MapFrom((y, x, i, c) => y.CreatedAt ?? x.CreatedAt));

        #endregion
    }
}
