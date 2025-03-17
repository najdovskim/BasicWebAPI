using AutoMapper;
using BasicWebAPI.Dal.Interfaces;
using BasicWebAPI.Domain.Models;
using BasicWebAPI.Service.Dtos.Country;
using BasicWebAPI.Service.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicWebAPI.Tests.Services
{
    public class CountryServiceTests
    {
        private readonly Mock<ICountryRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<CountryService>> _mockLogger;
        private readonly CountryService _service;

        public CountryServiceTests()
        {
            _mockRepo = new Mock<ICountryRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<CountryService>>();
            _service = new CountryService(_mockRepo.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task CreateCountryAsync_ValidInput_ReturnsMappedDto()
        {
            // Arrange
            var inputDto = new CountryPostPutDto { CountryName = "Test Country" };
            var domainCountry = new Country { CountryId = 1, CountryName = "Test Country" };
            var expectedDto = new CountryGetDto { CountryId = 1, CountryName = "Test Country" };

            _mockMapper.Setup(m => m.Map<Country>(inputDto)).Returns(domainCountry);
            _mockRepo.Setup(r => r.CreateCountryAsync(domainCountry)).ReturnsAsync(domainCountry);
            _mockMapper.Setup(m => m.Map<CountryGetDto>(domainCountry)).Returns(expectedDto);

            // Act
            var result = await _service.CreateCountryAsync(inputDto);

            // Assert
            Assert.Equal(expectedDto.CountryId, result.CountryId);
            Assert.Equal(expectedDto.CountryName, result.CountryName);
            _mockRepo.Verify(r => r.CreateCountryAsync(domainCountry), Times.Once);
        }


        [Fact]
        public void DeleteCountryAsync_ValidId_DeletesCountry()
        {
            // Arrange
            const int countryId = 1;

            // Act
            _service.DeleteCountryAsync(countryId);

            // Assert
            _mockRepo.Verify(r => r.DeleteCountryAsync(countryId), Times.Once);
        }

    
        [Fact]
        public async Task GetCountryAsync_ReturnsMappedList()
        {
            // Arrange
            var countries = new List<Country>
            {
                new Country { CountryId = 1, CountryName = "Country A" }
            };
            var expectedDtos = new List<CountryGetDto>
            {
                new CountryGetDto { CountryId = 1, CountryName = "Country A" }
            };

            _mockRepo.Setup(r => r.GetCountryAsync()).ReturnsAsync(countries);
            _mockMapper.Setup(m => m.Map<List<CountryGetDto>>(countries)).Returns(expectedDtos);

            // Act
            var result = await _service.GetCountryAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal("Country A", result[0].CountryName);
        }

        [Fact]
        public async Task UpdateCountryAsync_ValidInput_UpdatesAndReturnsDto()
        {
            // Arrange
            const int countryId = 1;
            var inputDto = new CountryPostPutDto { CountryName = "Updated Country" };
            var domainCountry = new Country { CountryId = countryId, CountryName = "Updated Country" };
            var expectedDto = new CountryGetDto { CountryId = countryId, CountryName = "Updated Country" };

            _mockMapper.Setup(m => m.Map<Country>(inputDto)).Returns(domainCountry);
            _mockRepo.Setup(r => r.UpdateCountryAsync(domainCountry)).ReturnsAsync(domainCountry);
            _mockMapper.Setup(m => m.Map<CountryGetDto>(domainCountry)).Returns(expectedDto);

            // Act
            var result = await _service.UpdateCountryAsync(inputDto, countryId);

            // Assert
            Assert.Equal("Updated Country", result.CountryName);
            Assert.Equal(countryId, domainCountry.CountryId);
            _mockRepo.Verify(r => r.UpdateCountryAsync(domainCountry), Times.Once);
        }



    }
}
