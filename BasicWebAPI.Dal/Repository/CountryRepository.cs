using BasicWebAPI.Dal.Interfaces;
using BasicWebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicWebAPI.Dal.Repository;
public class CountryRepository : ICountryRepository
{
    private readonly DataContext _ctx;
    private readonly ILogger<CountryRepository> _logger;

    public CountryRepository(DataContext ctx, ILogger<CountryRepository> logger)
    {
        _ctx = ctx;
        _logger = logger;
    }

    public async Task<Country> CreateCountryAsync(Country country)
    {
        try
        {
            _ctx.Countries.Add(country);
            await _ctx.SaveChangesAsync();
            return country;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error creating country");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error creating country");
            throw;
        }
    }

    public void DeleteCountryAsync(int countryId)
    {
        try
        {
            var country = _ctx.Countries.FirstOrDefault(c => c.CountryId == countryId);
            if (country != null)
            {
                _ctx.Countries.Remove(country);
                _ctx.SaveChanges();
            }
            else
            {
                _logger.LogWarning("Country not found for deletion: {CountryId}", countryId);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting country");
            throw;
        }
    }

    public async Task<List<Country>> GetCountryAsync()
    {
        try
        {
            return await _ctx.Countries.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving countries");
            throw;
        }
    }

    public async Task<Country> UpdateCountryAsync(Country updateCountry)
    {
        try
        {
            _ctx.Countries.Update(updateCountry);
            await _ctx.SaveChangesAsync();
            return updateCountry;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error updating country");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error updating country");
            throw;
        }
    }
}
