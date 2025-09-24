using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AntimicrobialPrescriptions.API.Controllers;
using AntimicrobialPrescriptions.API.Models;
using AntimicrobialPrescriptions.Application.DTOs;
using AntimicrobialPrescriptions.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AntimicrobialPrescriptions.API.Tests.Controllers
{
    public class PrescriptionsControllerTests
    {
        private readonly Mock<IPrescriptionService> _serviceMock;
        private readonly PrescriptionsController _controller;

        public PrescriptionsControllerTests()
        {
            _serviceMock = new Mock<IPrescriptionService>();
            _controller = new PrescriptionsController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkWithMappedResponse()
        {
            var dtos = new List<PrescriptionDto>
            {
                new PrescriptionDto
                {
                    Id = Guid.NewGuid(),
                    PatientId = "Patient1",
                    Status = Domain.Enums.PrescriptionStatus.Active
                },
                new PrescriptionDto
                {
                    Id = Guid.NewGuid(),
                    PatientId = "Patient2",
                    Status = Domain.Enums.PrescriptionStatus.Reviewed
                }
            };

            _serviceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(dtos);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var responses = Assert.IsAssignableFrom<IEnumerable<PrescriptionResponse>>(okResult.Value);

            Assert.Collection(responses,
                item => Assert.Equal(ApiPrescriptionStatus.Active, item.Status),
                item => Assert.Equal(ApiPrescriptionStatus.Reviewed, item.Status));
        }

        [Fact]
        public async Task GetById_WithFound_ReturnsOkWithMappedResponse()
        {
            var id = Guid.NewGuid();
            var dto = new PrescriptionDto
            {
                Id = id,
                PatientId = "Patient1",
                Status = Domain.Enums.PrescriptionStatus.Discontinued
            };
            _serviceMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(dto);

            var result = await _controller.GetById(id);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<PrescriptionResponse>(okResult.Value);
            Assert.Equal(ApiPrescriptionStatus.Discontinued, response.Status);
            Assert.Equal("Patient1", response.PatientId);
        }

        [Fact]
        public async Task GetById_WithNotFound_ReturnsNotFound()
        {
            _serviceMock
     .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
     .ReturnsAsync((PrescriptionDto)null!);


            var result = await _controller.GetById(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_ValidRequest_ReturnsCreatedAtAction()
        {
            var newId = Guid.NewGuid();

            _serviceMock.Setup(x => x.CreateAsync(It.IsAny<PrescriptionDto>())).ReturnsAsync(newId);

            var request = new CreatePrescriptionRequest
            {
                PatientId = "PatientNew",
                AntimicrobialName = "Amox",
                Dose = "500mg",
                Frequency = "3/day",
                Route = "Oral",
                Indication = "Infection",
                StartDate = DateTime.UtcNow,
                ExpectedEndDate = DateTime.UtcNow.AddDays(7),
                PrescriberName = "Dr Smith",
                PrescriberRole = "Clinician"
            };

            var result = await _controller.Create(request);

            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(newId, createdAtResult.Value);
        }

        [Fact]
        public async Task Update_ValidRequest_ReturnsNoContent()
        {
            var id = Guid.NewGuid();
            var request = new UpdatePrescriptionRequest
            {
                Id = id,
                PatientId = "PatientUpdated",
                AntimicrobialName = "Amox",
                Dose = "500mg",
                Frequency = "2/day",
                Route = "Oral",
                Indication = "Infection",
                StartDate = DateTime.UtcNow,
                ExpectedEndDate = DateTime.UtcNow.AddDays(7),
                PrescriberName = "Dr Updated",
                PrescriberRole = "Clinician",
                Status = "Active"
            };

            _serviceMock.Setup(x => x.UpdateAsync(It.IsAny<PrescriptionDto>())).Returns(Task.CompletedTask);

            var result = await _controller.Update(id, request);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_IdMismatch_ReturnsBadRequest()
        {
            var request = new UpdatePrescriptionRequest
            {
                Id = Guid.NewGuid(),
                Status = "Active"
            };

            var result = await _controller.Update(Guid.NewGuid(), request);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("ID in URL and body do not match", badRequest.Value);
        }

        [Fact]
        public async Task MarkReviewed_ReturnsNoContent()
        {
            var id = Guid.NewGuid();
            _serviceMock.Setup(x => x.MarkReviewedAsync(id)).Returns(Task.CompletedTask);

            var result = await _controller.MarkReviewed(id);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Discontinue_ReturnsNoContent()
        {
            var id = Guid.NewGuid();
            _serviceMock.Setup(x => x.DiscontinueAsync(id)).Returns(Task.CompletedTask);

            var result = await _controller.Discontinue(id);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
