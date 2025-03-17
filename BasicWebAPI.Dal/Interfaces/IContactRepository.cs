using BasicWebAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebAPI.Dal.Interfaces;
public interface IContactRepository
{
    Task<List<Contact>> GetContactAsync();
    Task<Contact> CreateContactAsync(Contact contact, int countryId, int companyId);
    Task<Contact> UpdateContactAsync(Contact updateContact, int contactId, int companyId, int countryId);
    Task<List<Contact>> FilterContactsAsync(int? countryId, int? companyId);
    void DeleteContactAsync(int contactId);
}


