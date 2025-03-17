using BasicWebAPI.Domain.Models;
using BasicWebAPI.Service.Dtos.Company;
using BasicWebAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BasicWebAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : Controller
{
    private readonly ICompanyService _companyService;
    private readonly ILogger<CompanyController> _logger;

    public CompanyController(ICompanyService companyService, ILogger<CompanyController> logger)
    {
        _companyService = companyService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCompanies()
    {
        try
        {
            var companies = await _companyService.GetCompanyAsync();
            return Ok(companies);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all companies");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    [HttpGet]
    [Route("{companyId}")]
    public async Task<IActionResult> GetCompanyById(int id)
    {
        try
        {
            var company = await _companyService.GetCompanyByIdAsync(id);
            if (company == null)
                return NotFound("Company not found");

            return Ok(company);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting company with ID {CompanyId}", id);
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateCompany([FromBody] CompanyPostPutDto company)
    {
      if (company == null)
            return BadRequest(ModelState);

        var companyPost = await _companyService.CreateCompanyAsync(company);

        return CreatedAtAction(nameof(CreateCompany), new { id = companyPost.CompanyId }, companyPost);
    }

    [HttpPut]
    [Route("{companyId}")]
    public async Task<IActionResult> UpdateCompany([FromBody] CompanyPostPutDto updatedCompany, int companyId)
    {
        try
        {
            var update = await _companyService.UpdateCompanyAsync(updatedCompany, companyId);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating company with ID {CompanyId}", companyId);
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    [HttpDelete]
    [Route("{companyId}")]
    public IActionResult DeleteCompany(int companyId)
    {
        try
        {
            _companyService.DeleteCompanyAsync(companyId);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting company with ID {CompanyId}", companyId);
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}
