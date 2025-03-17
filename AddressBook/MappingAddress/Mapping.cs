using AutoMapper;
using RepositoryLayer.Entity;
using ModelLayer.Model;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<AddressBookEntity, EntryModel>().ReverseMap();
    }
}