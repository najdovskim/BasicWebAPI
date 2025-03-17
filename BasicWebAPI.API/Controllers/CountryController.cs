using BasicWebAPI.Domain.Models;
using BasicWebAPI.Service.Dtos.Country;
using BasicWebAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BasicWebAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountryController : Controller
{
    private readonly ICountryService _countryService;
    private readonly ILogger<CountryController> _logger;

    public CountryController(ICountryService countryService, ILogger<CountryController> logger)
    {
        _countryService = countryService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCountries()
    {
        try
        {
            var countries = await _countryService.GetCountryAsync();
            return Ok(countries);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all countries");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateCountry([FromBody] CountryPostPutDto country)
    {
        try
        {
            if (country == null)
                return BadRequest(ModelState);

            var countryPost = await _countryService.CreateCountryAsync(country);
            return CreatedAtAction(nameof(GetAllCountries), new { id = countryPost.CountryId }, countryPost);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating a new country");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    [HttpPut]
    [Route("{countryId}")]
    public async Task<IActionResult> UpdateCountry([FromBody] CountryPostPutDto updatedCountry, int countryId)
    {
        try
        {
            var update = await _countryService.UpdateCountryAsync(updatedCountry, countryId);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating country with ID {CountryId}", countryId);
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    [HttpDelete]
    [Route("{countryId}")]
    public IActionResult DeleteCountry(int countryId)
    {
        try
        {
            _countryService.DeleteCountryAsync(countryId);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting country with ID {CountryId}", countryId);
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}
