using BasicWebAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebAPI.Dal.Interfaces;
public interface ICountryRepository
{
    Task<List<Country>> GetCountryAsync();
    Task<Country> CreateCountryAsync(Country country);
    Task<Country> UpdateCountryAsync(Country updateCounty);
    void DeleteCountryAsync(int countryId);
}
