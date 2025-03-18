using AutoMapper;

using ModelLayer.Model;
using RepositoryLayer.Entity;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RequestModel, AddressBookEntity>();
        CreateMap<AddressBookEntity, EntryModel>();
    }
}