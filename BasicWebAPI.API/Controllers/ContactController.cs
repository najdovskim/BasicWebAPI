using BasicWebAPI.Domain.Models;
using BasicWebAPI.Service.Dtos.Contact;
using BasicWebAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BasicWebAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : Controller
{
    private readonly IContactService _contactService;
    private readonly ILogger<ContactController> _logger;

    public ContactController(IContactService contactService, ILogger<ContactController> logger)
    {
        _contactService = contactService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllContact()
    {
        try
        {
            var contacts = await _contactService.GetContactAsync();
            return Ok(contacts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all contacts");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    [HttpGet("filter")]
    public async Task<IActionResult> FilterContactsAsync(int? countryId, int? companyId)
    {
        try
        {
            var contacts = await _contactService.FilterContactsAsync(countryId, companyId);
            return Ok(contacts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while filtering contacts");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateContact([FromBody] ContactPostPutDto contact, int companyId, int countryId)
    {
        try
        {
            if (contact == null)
                return BadRequest(ModelState);

            var contactPost = await _contactService.CreateContactAsync(contact, companyId, countryId);
            return CreatedAtAction(nameof(GetAllContact), new { id = contactPost.ContactId }, contactPost);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating a new contact");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    [HttpPut]
    [Route("{contactId}")]
    public async Task<IActionResult> UpdateContact([FromBody] ContactPostPutDto updatedcontact, int contactId, int companyId, int countryId)
    {
        try
        {
            var update = await _contactService.UpdateContactAsync(updatedcontact, contactId, companyId, countryId);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating contact with ID {ContactId}", contactId);
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    [HttpDelete]
    [Route("{contactId}")]
    public IActionResult DeleteContact(int contactId)
    {
        try
        {
            _contactService.DeleteContactAsync(contactId);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting contact with ID {ContactId}", contactId);
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}
