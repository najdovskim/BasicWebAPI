using AutoMapper;
using BasicWebAPI.Dal.Interfaces;
using BasicWebAPI.Domain.Models;
using BasicWebAPI.Service.Dtos.Contact;
using BasicWebAPI.Service.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicWebAPI.Tests.Services
{
    public class ContactServiceTests
    {
        private readonly Mock<IContactRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<ContactService>> _mockLogger;
        private readonly ContactService _service;

        public ContactServiceTests()
        {
            _mockRepo = new Mock<IContactRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<ContactService>>();
            _service = new ContactService(_mockRepo.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task CreateContactAsync_ValidRequest_ReturnsContactDto()
        {
            // Arrange
            var contactDto = new ContactPostPutDto { ContactName = "John Doe" };
            var contact = new Contact { ContactId = 1, ContactName = "John Doe" };
            var expectedDto = new ContactGetDto { ContactId = 1, ContactName = "John Doe" };

            _mockMapper.Setup(m => m.Map<Contact>(contactDto)).Returns(contact);
            _mockRepo.Setup(r => r.CreateContactAsync(contact, 1, 1)).ReturnsAsync(contact);
            _mockMapper.Setup(m => m.Map<ContactGetDto>(contact)).Returns(expectedDto);

            // Act
            var result = await _service.CreateContactAsync(contactDto, 1, 1);

            // Assert
            Assert.Equal(expectedDto.ContactId, result.ContactId);
            _mockRepo.Verify(r => r.CreateContactAsync(contact, 1, 1), Times.Once);
        }

       

        [Fact]
        public void DeleteContactAsync_ValidId_DeletesContact()
        {
            // Arrange
            const int contactId = 1;

            // Act
            _service.DeleteContactAsync(contactId);

            // Assert
            _mockRepo.Verify(r => r.DeleteContactAsync(contactId), Times.Once);
        }

       

        [Fact]
        public async Task GetContactAsync_ReturnsMappedDtos()
        {
            // Arrange
            var contacts = new List<Contact>
            {
                new Contact { ContactId = 1, ContactName = "John" }
            };
            var dtos = new List<ContactGetDto>
            {
                new ContactGetDto { ContactId = 1, ContactName = "John" }
            };

            _mockRepo.Setup(r => r.GetContactAsync()).ReturnsAsync(contacts);
            _mockMapper.Setup(m => m.Map<List<ContactGetDto>>(contacts)).Returns(dtos);

            // Act
            var result = await _service.GetContactAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal("John", result[0].ContactName);
        }

        [Fact]
        public async Task FilterContactsAsync_ValidParameters_ReturnsFilteredResults()
        {
            // Arrange
            const int companyId = 1, countryId = 1;
            var contacts = new List<Contact> { new Contact { ContactId = 1 } };
            var dtos = new List<ContactGetDto> { new ContactGetDto { ContactId = 1 } };

            _mockRepo.Setup(r => r.FilterContactsAsync(companyId, countryId))
                   .ReturnsAsync(contacts);
            _mockMapper.Setup(m => m.Map<List<ContactGetDto>>(contacts)).Returns(dtos);

            // Act
            var result = await _service.FilterContactsAsync(companyId, countryId);

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].ContactId);
        }

        [Fact]
        public async Task UpdateContactAsync_ValidRequest_ReturnsUpdatedDto()
        {
            // Arrange
            const int contactId = 1, companyId = 1, countryId = 1;
            var updateDto = new ContactPostPutDto { ContactName = "Updated Name" };
            var contact = new Contact { ContactId = contactId, ContactName = "Updated Name" };
            var expectedDto = new ContactGetDto { ContactId = contactId, ContactName = "Updated Name" };

            _mockMapper.Setup(m => m.Map<Contact>(updateDto)).Returns(contact);
            _mockMapper.Setup(m => m.Map<ContactGetDto>(contact)).Returns(expectedDto);

            // Act
            var result = await _service.UpdateContactAsync(updateDto, contactId, companyId, countryId);

            // Assert
            Assert.Equal("Updated Name", result.ContactName);
            _mockRepo.Verify(r => r.UpdateContactAsync(
                contact,
                contactId,
                companyId,
                countryId
            ), Times.Once);
        }

       
    }
}
