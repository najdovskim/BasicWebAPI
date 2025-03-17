using AutoMapper;
using BasicWebAPI.Dal.Interfaces;
using BasicWebAPI.Domain.Models;
using BasicWebAPI.Service.Dtos.Contact;
using BasicWebAPI.Service.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicWebAPI.Service.Services;
public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ContactService> _logger;

    public ContactService(IContactRepository contactRepository, IMapper mapper, ILogger<ContactService> logger)
    {
        _contactRepository = contactRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ContactGetDto> CreateContactAsync(ContactPostPutDto contact, int countryId, int companyId)
    {
        try
        {
            var domainContact = _mapper.Map<Contact>(contact);
            await _contactRepository.CreateContactAsync(domainContact, countryId, companyId);
            return _mapper.Map<ContactGetDto>(domainContact);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating contact");
            throw;
        }
    }

    public void DeleteContactAsync(int contactId)
    {
        try
        {
            _contactRepository.DeleteContactAsync(contactId);
            Console.WriteLine("Contact deleted");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting contact with ID {ContactId}", contactId);
            throw;
        }
    }

    public async Task<List<ContactGetDto>> GetContactAsync()
    {
        try
        {
            var contacts = await _contactRepository.GetContactAsync();
            return _mapper.Map<List<ContactGetDto>>(contacts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving contacts");
            throw;
        }
    }

    public async Task<List<ContactGetDto>> FilterContactsAsync(int? countryId, int? companyId)
    {
        try
        {
            var contacts = await _contactRepository.FilterContactsAsync(countryId, companyId);
            return _mapper.Map<List<ContactGetDto>>(contacts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error filtering contacts");
            throw;
        }
    }

    public async Task<ContactGetDto> UpdateContactAsync(ContactPostPutDto updateContact, int contactId, int companyId, int countryId)
    {
        try
        {
            var toUpdate = _mapper.Map<Contact>(updateContact);
            toUpdate.SetContactId(contactId);

            await _contactRepository.UpdateContactAsync(toUpdate, contactId, companyId, countryId);
            return _mapper.Map<ContactGetDto>(toUpdate);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating contact with ID {ContactId}", contactId);
            throw;
        }
    }
}
