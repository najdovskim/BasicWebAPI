using AutoMapper;
using BasicWebAPI.Dal.Interfaces;
using BasicWebAPI.Domain.Models;
using BasicWebAPI.Service.Dtos.Company;
using BasicWebAPI.Service.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicWebAPI.Service.Services;
public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CompanyService> _logger;

    public CompanyService(ICompanyRepository companyRepository, IMapper mapper, ILogger<CompanyService> logger)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CompanyGetDto> CreateCompanyAsync(CompanyPostPutDto company)
    {
        try
        {
            var domainCompany = _mapper.Map<Company>(company);
            await _companyRepository.CreateCompanyAsync(domainCompany);
            return _mapper.Map<CompanyGetDto>(domainCompany);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating company");
            throw;
        }
    }

    public void DeleteCompanyAsync(int companyId)
    {
        try
        {
            _companyRepository.DeleteCompanyAsync(companyId);
            Console.WriteLine("Company deleted");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting company with ID {CompanyId}", companyId);
            throw;
        }
    }

    public async Task<List<CompanyGetDto>> GetCompanyAsync()
    {
        try
        {
            var companies = await _companyRepository.GetCompanyAsync();
            return _mapper.Map<List<CompanyGetDto>>(companies);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving companies");
            throw;
        }
    }

    public async Task<CompanyGetDto> GetCompanyByIdAsync(int companyId)
    {
        try
        {
            var company = await _companyRepository.GetCompanyByIdAsync(companyId);
            if (company == null)
                return null;
            return _mapper.Map<CompanyGetDto>(company);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving company with ID {CompanyId}", companyId);
            throw;
        }
    }

    public async Task<CompanyGetDto> UpdateCompanyAsync(CompanyPostPutDto updatedCompany, int companyId)
    {
        try
        {
            var toUpdate = _mapper.Map<Company>(updatedCompany);
            toUpdate.SetCompanyId(companyId);
            await _companyRepository.UpdateCompanyAsync(toUpdate);
            return _mapper.Map<CompanyGetDto>(toUpdate);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating company with ID {CompanyId}", companyId);
            throw;
        }
    }
}
