using BasicWebAPI.Dal.Interfaces;
using BasicWebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebAPI.Dal.Repository;
public class CompanyRepository : ICompanyRepository
{
    private readonly DataContext _ctx;
    private readonly ILogger<CompanyRepository> _logger;

    public CompanyRepository(DataContext ctx, ILogger<CompanyRepository> logger)
    {
        _ctx = ctx;
        _logger = logger;
    }


    public async Task<Company> CreateCompanyAsync(Company company)
    {
        try
        {
            _ctx.Companies.Add(company);
            await _ctx.SaveChangesAsync();
            return company;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error creating company");
            throw; 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error creating company");
            throw; 
        }
    }

    public void DeleteCompanyAsync(int companyId)
    {
        try
        {
            var company = _ctx.Companies.FirstOrDefault(c => c.CompanyId == companyId);

            if (company != null)
            {
                _ctx.Companies.Remove(company);
                _ctx.SaveChanges();
            }
            else
            {
                _logger.LogWarning("Company not found for deletion");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting company");
            throw; 
        }
    }

    public async Task<List<Company>> GetCompanyAsync()
    {
        try
        {
            return await _ctx.Companies.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving companies");
            throw;
        }
    }

    public async Task<Company> GetCompanyByIdAsync(int companyId)
    {
        try
        {
            var company = await _ctx.Companies.FirstOrDefaultAsync(c => c.CompanyId == companyId);
            if (company == null)
            {
                _logger.LogWarning("Company not found");
                return null;
            }
            return company;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving company by ID");
            throw; 
        }
    }

    public async Task<Company> UpdateCompanyAsync(Company updatedCompany)
    {
        try
        {
            _ctx.Companies.Update(updatedCompany);
            await _ctx.SaveChangesAsync();
            return updatedCompany;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error updating company");
            throw; 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error updating company");
            throw; 
        }
    }

}
