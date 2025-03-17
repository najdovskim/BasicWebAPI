using BasicWebAPI.API.Controllers;
using BasicWebAPI.Service.Dtos.Country;
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
    public class CountryControllerTests
    {
        private readonly Mock<ICountryService> _mockService;
        private readonly Mock<ILogger<CountryController>> _mockLogger;
        private readonly CountryController _controller;

        public CountryControllerTests()
        {
            _mockService = new Mock<ICountryService>();
            _mockLogger = new Mock<ILogger<CountryController>>();
            _controller = new CountryController(_mockService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAllCountries_ReturnsOkResultWithCountries()
        {
            var countries = new List<CountryGetDto>
            {
                new CountryGetDto { CountryId = 1, CountryName = "Country A" }
            };
            _mockService.Setup(s => s.GetCountryAsync()).ReturnsAsync(countries);

            var result = await _controller.GetAllCountries();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(countries, okResult.Value);
        }

      

        [Fact]
        public async Task CreateCountry_ValidInput_ReturnsCreatedResult()
        {
            var countryDto = new CountryPostPutDto { CountryName = "New Country" };
            var createdCountry = new CountryGetDto { CountryId = 1, CountryName = "New Country" };
            _mockService.Setup(s => s.CreateCountryAsync(countryDto)).ReturnsAsync(createdCountry);

            var result = await _controller.CreateCountry(countryDto);

            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetAllCountries", createdAtResult.ActionName);
            Assert.Equal(1, createdAtResult.RouteValues["id"]);
            Assert.Equal(createdCountry, createdAtResult.Value);
        }

        [Fact]
        public async Task CreateCountry_NullInput_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("Error", "Test error");

            var result = await _controller.CreateCountry(null);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateCountry_ValidRequest_ReturnsNoContent()
        {
            var countryDto = new CountryPostPutDto { CountryName = "Updated Country" };
            _mockService.Setup(s => s.UpdateCountryAsync(countryDto, 1)).ReturnsAsync(new CountryGetDto());

            var result = await _controller.UpdateCountry(countryDto, 1);

            Assert.IsType<NoContentResult>(result);
            _mockService.Verify(s => s.UpdateCountryAsync(countryDto, 1), Times.Once);
        }

        [Fact]
        public void DeleteCountry_ValidId_ReturnsNoContent()
        {
            var result = _controller.DeleteCountry(1);

            Assert.IsType<NoContentResult>(result);
            _mockService.Verify(s => s.DeleteCountryAsync(1), Times.Once);
        }

    }
}
