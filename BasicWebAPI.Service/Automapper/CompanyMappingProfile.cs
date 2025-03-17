using AutoMapper;
using BasicWebAPI.Domain.Models;
using BasicWebAPI.Service.Dtos.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebAPI.Service.Automapper;
public class CompanyMappingProfile : Profile
{
    public CompanyMappingProfile() 
    {
        CreateMap<Company, CompanyGetDto>();
        CreateMap<CompanyPostPutDto, Company>();
    }
}
