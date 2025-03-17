using AutoMapper;
using BasicWebAPI.Dal.Interfaces;
using BasicWebAPI.Domain.Models;
using BasicWebAPI.Service.Dtos.Company;
using BasicWebAPI.Service.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicWebAPI.Tests.Services
{
    public class CompanyServiceTests
    {
        private readonly Mock<ICompanyRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<CompanyService>> _mockLogger;
        private readonly CompanyService _service;

        public CompanyServiceTests()
        {
            _mockRepo = new Mock<ICompanyRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<CompanyService>>();
            _service = new CompanyService(_mockRepo.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task CreateCompanyAsync_ShouldReturnCompanyGetDto()
        {
            // Arrange
            var companyPostPutDto = new CompanyPostPutDto { CompanyName = "Test Company" };
            var company = new Company { CompanyId = 1, CompanyName = "Test Company" };
            var companyGetDto = new CompanyGetDto { CompanyId = 1, CompanyName = "Test Company" };

            _mockMapper.Setup(m => m.Map<Company>(companyPostPutDto)).Returns(company);
            _mockRepo.Setup(r => r.CreateCompanyAsync(It.IsAny<Company>())).ReturnsAsync(company);
            _mockMapper.Setup(m => m.Map<CompanyGetDto>(company)).Returns(companyGetDto);

            // Act
            var result = await _service.CreateCompanyAsync(companyPostPutDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(companyGetDto.CompanyId, result.CompanyId);
            Assert.Equal(companyGetDto.CompanyName, result.CompanyName);
        }

        [Fact]
        public async Task GetCompanyAsync_ShouldReturnListOfCompanyGetDto()
        {
            // Arrange
            var companies = new List<Company>
            {
                new Company { CompanyId = 1, CompanyName = "Company 1" },
                new Company { CompanyId = 2, CompanyName = "Company 2" }
            };
            var companyGetDtos = new List<CompanyGetDto>
            {
                new CompanyGetDto { CompanyId = 1, CompanyName = "Company 1" },
                new CompanyGetDto { CompanyId = 2, CompanyName = "Company 2" }
            };

            _mockRepo.Setup(r => r.GetCompanyAsync()).ReturnsAsync(companies);
            _mockMapper.Setup(m => m.Map<List<CompanyGetDto>>(companies)).Returns(companyGetDtos);

            // Act
            var result = await _service.GetCompanyAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(companyGetDtos[0].CompanyId, result[0].CompanyId);
            Assert.Equal(companyGetDtos[1].CompanyName, result[1].CompanyName);
        }

        [Fact]
        public async Task GetCompanyByIdAsync_ShouldReturnCompanyGetDto()
        {
            // Arrange
            int companyId = 1;
            var company = new Company { CompanyId = companyId, CompanyName = "Test Company" };
            var companyGetDto = new CompanyGetDto { CompanyId = companyId, CompanyName = "Test Company" };

            _mockRepo.Setup(r => r.GetCompanyByIdAsync(companyId)).ReturnsAsync(company);
            _mockMapper.Setup(m => m.Map<CompanyGetDto>(company)).Returns(companyGetDto);

            // Act
            var result = await _service.GetCompanyByIdAsync(companyId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(companyGetDto.CompanyId, result.CompanyId);
            Assert.Equal(companyGetDto.CompanyName, result.CompanyName);
        }

        [Fact]
        public async Task UpdateCompanyAsync_ShouldReturnUpdatedCompanyGetDto()
        {
            // Arrange
            int companyId = 1;
            var companyPostPutDto = new CompanyPostPutDto { CompanyName = "Updated Company" };
            var updatedCompany = new Company { CompanyId = companyId, CompanyName = "Updated Company" };
            var updatedCompanyGetDto = new CompanyGetDto { CompanyId = companyId, CompanyName = "Updated Company" };

            _mockMapper.Setup(m => m.Map<Company>(companyPostPutDto)).Returns(updatedCompany);
            _mockRepo.Setup(r => r.UpdateCompanyAsync(It.IsAny<Company>())).ReturnsAsync(updatedCompany);
            _mockMapper.Setup(m => m.Map<CompanyGetDto>(updatedCompany)).Returns(updatedCompanyGetDto);

            // Act
            var result = await _service.UpdateCompanyAsync(companyPostPutDto, companyId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedCompanyGetDto.CompanyId, result.CompanyId);
            Assert.Equal(updatedCompanyGetDto.CompanyName, result.CompanyName);
        }
    }
}
