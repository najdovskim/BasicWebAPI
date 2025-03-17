using AutoMapper;
using BasicWebAPI.Dal.Interfaces;
using BasicWebAPI.Domain.Models;
using BasicWebAPI.Service.Dtos.Country;
using BasicWebAPI.Service.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicWebAPI.Service.Services;
public class CountryService : ICountryService
{
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CountryService> _logger;

    public CountryService(ICountryRepository countryRepository, IMapper mapper, ILogger<CountryService> logger)
    {
        _countryRepository = countryRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CountryGetDto> CreateCountryAsync(CountryPostPutDto country)
    {
        try
        {
            var domainCountry = _mapper.Map<Country>(country);
            await _countryRepository.CreateCountryAsync(domainCountry);
            return _mapper.Map<CountryGetDto>(domainCountry);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating country");
            throw;
        }
    }

    public void DeleteCountryAsync(int countryId)
    {
        try
        {
            _countryRepository.DeleteCountryAsync(countryId);
            Console.WriteLine("Country deleted");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting country with ID {CountryId}", countryId);
            throw;
        }
    }

    public async Task<List<CountryGetDto>> GetCountryAsync()
    {
        try
        {
            var countries = await _countryRepository.GetCountryAsync();
            return _mapper.Map<List<CountryGetDto>>(countries);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving countries");
            throw;
        }
    }

    public async Task<CountryGetDto> UpdateCountryAsync(CountryPostPutDto updateCountry, int countryId)
    {
        try
        {
            var toUpdate = _mapper.Map<Country>(updateCountry);
            toUpdate.SetCountryId(countryId);
            await _countryRepository.UpdateCountryAsync(toUpdate);
            return _mapper.Map<CountryGetDto>(toUpdate);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating country with ID {CountryId}", countryId);
            throw;
        }
    }
}
