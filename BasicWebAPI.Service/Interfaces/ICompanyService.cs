using BasicWebAPI.Domain.Models;
using BasicWebAPI.Service.Dtos.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebAPI.Service.Interfaces;
public interface ICompanyService
{
    Task<List<CompanyGetDto>> GetCompanyAsync();
    Task<CompanyGetDto> GetCompanyByIdAsync(int companyId);
    Task<CompanyGetDto> CreateCompanyAsync(CompanyPostPutDto company);
    Task<CompanyGetDto> UpdateCompanyAsync(CompanyPostPutDto updatedCompany, int companyId);
    void DeleteCompanyAsync(int companyId);
}
