using AutoMapper;
using BasicWebAPI.API.Controllers;
using BasicWebAPI.Service.Dtos.Company;
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
    public class CompanyControllerTests
    {
        private readonly Mock<ICompanyService> _mockService;
        private readonly Mock<ILogger<CompanyController>> _mockLogger;
        private readonly CompanyController _controller;

        public CompanyControllerTests()
        {
            _mockService = new Mock<ICompanyService>();
            _mockLogger = new Mock<ILogger<CompanyController>>();
            _controller = new CompanyController(_mockService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAllCompanies_ReturnsOkResultWithCompanies()
        {
            var companies = new List<CompanyGetDto>
            {
                new CompanyGetDto { CompanyId = 1, CompanyName = "Company A" }
            };
            _mockService.Setup(s => s.GetCompanyAsync()).ReturnsAsync(companies);

            var result = await _controller.GetAllCompanies();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(companies, okResult.Value);
        }

      
        [Fact]
        public async Task GetCompanyById_ValidId_ReturnsCompany()
        {
            var company = new CompanyGetDto { CompanyId = 1 };
            _mockService.Setup(s => s.GetCompanyByIdAsync(1)).ReturnsAsync(company);

            var result = await _controller.GetCompanyById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(company, okResult.Value);
        }

        [Fact]
        public async Task GetCompanyById_InvalidId_ReturnsNotFound()
        {
            _mockService.Setup(s => s.GetCompanyByIdAsync(1)).ReturnsAsync((CompanyGetDto)null);

            var result = await _controller.GetCompanyById(1);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task CreateCompany_ValidInput_ReturnsCreatedResult()
        {
            var companyDto = new CompanyPostPutDto { CompanyName = "New Company" };
            var createdCompany = new CompanyGetDto { CompanyId = 1, CompanyName = "New Company" };
            _mockService.Setup(s => s.CreateCompanyAsync(companyDto)).ReturnsAsync(createdCompany);

            var result = await _controller.CreateCompany(companyDto);

            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetCompanyById", createdAtResult.ActionName);
            Assert.Equal(1, createdAtResult.RouteValues["id"]);
            Assert.Equal(createdCompany, createdAtResult.Value);
        }

        [Fact]
        public async Task CreateCompany_NullInput_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("Error", "Test error");

            var result = await _controller.CreateCompany(null);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateCompany_ValidRequest_ReturnsNoContent()
        {
            var companyDto = new CompanyPostPutDto { CompanyName = "Updated Company" };
            _mockService.Setup(s => s.UpdateCompanyAsync(companyDto, 1)).ReturnsAsync(new CompanyGetDto());

            var result = await _controller.UpdateCompany(companyDto, 1);

            Assert.IsType<NoContentResult>(result);
        }

      

        [Fact]
        public void DeleteCompany_ValidId_ReturnsNoContent()
        {
            var result = _controller.DeleteCompany(1);

            Assert.IsType<NoContentResult>(result);
            _mockService.Verify(s => s.DeleteCompanyAsync(1), Times.Once);
        }

       
    }
}
