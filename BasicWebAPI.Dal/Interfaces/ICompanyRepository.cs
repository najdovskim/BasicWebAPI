using BasicWebAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebAPI.Dal.Interfaces;
public interface ICompanyRepository
{
    Task<List<Company>> GetCompanyAsync();
    Task<Company> GetCompanyByIdAsync(int companyId);
    Task<Company> CreateCompanyAsync(Company company);
    Task<Company> UpdateCompanyAsync(Company updatedCompany);
    void DeleteCompanyAsync(int companyId);
}
