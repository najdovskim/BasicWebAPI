using BasicWebAPI.Dal.Interfaces;
using BasicWebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BasicWebAPI.Dal.Repository;
public class ContactRepository : IContactRepository
{
    private readonly DataContext _ctx;
    private readonly ILogger<ContactRepository> _logger;

    public ContactRepository(DataContext ctx, ILogger<ContactRepository> logger)
    {
        _ctx = ctx;
        _logger = logger;
    }


    public async Task<Contact> CreateContactAsync(Contact contact, int countryId, int companyId)
    {
        try
        {
            contact.CountryId = countryId;
            contact.CompanyId = companyId;

            _ctx.Contacts.Add(contact);
            await _ctx.SaveChangesAsync();
            return contact;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error creating contact");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error creating contact");
            throw;
        }
    }

    public void DeleteContactAsync(int contactId)
    {
        try
        {
            var contact = _ctx.Contacts.FirstOrDefault(c => c.ContactId == contactId);

            if (contact != null)
            {
                _ctx.Contacts.Remove(contact);
                _ctx.SaveChanges();
            }
            else
            {
                _logger.LogWarning("Contact not found for deletion");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting contact");
            throw; 
        }
    }

    public async Task<List<Contact>> FilterContactsAsync(int? countryId, int? companyId)
    {
        try
        {
            return await _ctx.Contacts
                .Where(c => (countryId == null || c.CountryId == countryId)
                         && (companyId == null || c.CompanyId == companyId))
                        .Include(c => c.Company)
                        .Include(c => c.Country)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error filtering contacts");
            throw; 
        }
    }

    public async Task<List<Contact>> GetContactAsync()
    {
        try
        {
            return await _ctx.Contacts
                .Include(c => c.Company)
                .Include(c => c.Country)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving contacts");
            throw; 
        }
    }

    public async Task<Contact> UpdateContactAsync(Contact updateContact, int contactId, int countryId, int companyId)
    {
        try
        {
            updateContact.CountryId = countryId;
            updateContact.CompanyId = companyId;
            updateContact.ContactId = contactId;

            _ctx.Contacts.Update(updateContact);
            await _ctx.SaveChangesAsync();

            return updateContact;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error updating contact");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error updating contact");
            throw;
        }
    }

}
