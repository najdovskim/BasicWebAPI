using AutoMapper;
using BasicWebAPI.Domain.Models;
using BasicWebAPI.Service.Dtos.Company;
using BasicWebAPI.Service.Dtos.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebAPI.Service.Automapper;
public class CountryMappingProfile :Profile
{
    public CountryMappingProfile()
    {
        CreateMap<Country, CountryGetDto>();
        CreateMap<CountryPostPutDto, Country>();
    }
}
