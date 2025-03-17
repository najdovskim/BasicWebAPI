using AutoMapper;
using BasicWebAPI.API.Controllers;
using BasicWebAPI.Service.Dtos.Contact;
using BasicWebAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BasicWebAPI.Tests.Controllers
{
    public class ContactControllerTests
    {
        private readonly Mock<IContactService> _mockService;
        private readonly Mock<ILogger<ContactController>> _mockLogger;
        private readonly ContactController _controller;

        public ContactControllerTests()
        {
            _mockService = new Mock<IContactService>();
            _mockLogger = new Mock<ILogger<ContactController>>();
            _controller = new ContactController(_mockService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAllContact_ReturnsOkResultWithContacts()
        {
            var contacts = new List<ContactGetDto>
            {
                new ContactGetDto { ContactId = 1, ContactName = "John Doe" }
            };
            _mockService.Setup(s => s.GetContactAsync()).ReturnsAsync(contacts);

            var result = await _controller.GetAllContact();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(contacts, okResult.Value);
        }


        [Fact]
        public async Task FilterContactsAsync_ValidParams_ReturnsFilteredResults()
        {
            var contacts = new List<ContactGetDto>
            {
                new ContactGetDto { ContactId = 1 }
            };
            _mockService.Setup(s => s.FilterContactsAsync(1, 1)).ReturnsAsync(contacts);

            var result = await _controller.FilterContactsAsync(1, 1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(contacts, okResult.Value);
        }

   
        [Fact]
        public async Task CreateContact_ValidInput_ReturnsCreatedResult()
        {
            var contactDto = new ContactPostPutDto { ContactName = "New Contact" };
            var createdContact = new ContactGetDto { ContactId = 1 };
            _mockService.Setup(s => s.CreateContactAsync(contactDto, 1, 1)).ReturnsAsync(createdContact);

            var result = await _controller.CreateContact(contactDto, 1, 1);

            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetAllContact", createdAtResult.ActionName);
            Assert.Equal(1, createdAtResult.RouteValues["id"]);
            Assert.Equal(createdContact, createdAtResult.Value);
        }

        [Fact]
        public async Task CreateContact_NullInput_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("Error", "Test error");

            var result = await _controller.CreateContact(null, 1, 1);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateContact_ValidRequest_ReturnsNoContent()
        {
            var contactDto = new ContactPostPutDto { ContactName = "Updated Contact" };
            _mockService.Setup(s => s.UpdateContactAsync(contactDto, 1, 1, 1))
                      .ReturnsAsync(new ContactGetDto());

            var result = await _controller.UpdateContact(contactDto, 1, 1, 1);

            Assert.IsType<NoContentResult>(result);
        }

   
        [Fact]
        public void DeleteContact_ValidId_ReturnsNoContent()
        {
            var result = _controller.DeleteContact(1);

            Assert.IsType<NoContentResult>(result);
            _mockService.Verify(s => s.DeleteContactAsync(1), Times.Once);
        }

      
    }
}
