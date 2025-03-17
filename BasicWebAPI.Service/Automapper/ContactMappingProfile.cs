using AutoMapper;
using BasicWebAPI.Domain.Models;
using BasicWebAPI.Service.Dtos.Company;
using BasicWebAPI.Service.Dtos.Contact;
using BasicWebAPI.Service.Dtos.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebAPI.Service.Automapper;
public class ContactMappingProfile:Profile
{
    public ContactMappingProfile() 
    {
        CreateMap<Contact, ContactGetDto>()
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.CompanyName))
            .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.CountryName));
        CreateMap<ContactPostPutDto, Contact>();
    }
}
