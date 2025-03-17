using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebAPI.Domain.Models;
public class Country
{
    public int CountryId { get;  set; }
    public string CountryName { get;  set; }

    public ICollection<Contact>? Contacts { get;  set; }

    public void SetCountryId(int countryId)
    {
        CountryId = countryId;
    }
}
