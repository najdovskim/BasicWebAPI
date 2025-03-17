using BasicWebAPI.Domain.Models;
using BasicWebAPI.Service.Dtos.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebAPI.Service.Interfaces;
public interface ICountryService
{
    Task<List<CountryGetDto>> GetCountryAsync();
    Task<CountryGetDto> CreateCountryAsync(CountryPostPutDto country);
    Task<CountryGetDto> UpdateCountryAsync(CountryPostPutDto updateCountry, int countryId);
    void DeleteCountryAsync(int countryId);
}
