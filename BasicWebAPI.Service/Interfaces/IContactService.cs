using BasicWebAPI.Domain.Models;
using BasicWebAPI.Service.Dtos.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebAPI.Service.Interfaces;
public interface IContactService
{
    Task<List<ContactGetDto>> GetContactAsync();
    Task<ContactGetDto> CreateContactAsync(ContactPostPutDto contact, int countryId, int companyId);
    Task<ContactGetDto> UpdateContactAsync(ContactPostPutDto updateContact, int contactId, int companyId, int countryId);
    Task<List<ContactGetDto>> FilterContactsAsync(int? countryId, int? companyId);
    void DeleteContactAsync(int contactId);
}
