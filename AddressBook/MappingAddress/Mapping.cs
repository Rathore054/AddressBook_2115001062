using AutoMapper;

using ModelLayer.Model;
using RepositoryLayer.Entity;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<RequestModel, AddressBookEntity>();
        CreateMap<AddressBookEntity, EntryModel>();
    }
}