using AntimicrobialPrescriptions.API.Models;
using AntimicrobialPrescriptions.Application.DTOs;
using AntimicrobialPrescriptions.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntimicrobialPrescriptions.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;
        public PrescriptionsController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpGet]
        [Authorize(Roles = "Clinician,InfectionControl")]
        public async Task<ActionResult<IEnumerable<PrescriptionResponse>>> GetAll()
        {
            var prescriptionsDtos = await _prescriptionService.GetAllAsync();
            var response = prescriptionsDtos.Select(dto => MapToResponse(dto));
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Clinician,InfectionControl")]
        public async Task<ActionResult<PrescriptionResponse>> GetById(Guid id)
        {
            var dto = await _prescriptionService.GetByIdAsync(id);
            if (dto == null) return NotFound();
            return Ok(MapToResponse(dto));
        }

        [HttpPost]
       [Authorize(Roles = "Clinician")]
        public async Task<ActionResult<Guid>> Create(CreatePrescriptionRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dto = new PrescriptionDto
            {
                PatientId = request.PatientId,
                AntimicrobialName = request.AntimicrobialName,
                Dose = request.Dose,
                Frequency = request.Frequency,
                Route = request.Route,
                Indication = request.Indication,
                StartDate = request.StartDate,
                ExpectedEndDate = request.ExpectedEndDate,
                PrescriberName = request.PrescriberName,
                PrescriberRole = request.PrescriberRole,
               
                Status = MapToDomainStatus(ApiPrescriptionStatus.Active)
            };

            var newId = await _prescriptionService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = newId }, newId);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Clinician")]
        public async Task<IActionResult> Update(Guid id, UpdatePrescriptionRequest request)
        {
            if (id != request.Id)
                return BadRequest("ID in URL and body do not match");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var domainStatus = Enum.TryParse<Domain.Enums.PrescriptionStatus>(request.Status, true, out var parsedStatus)
                ? parsedStatus
                : Domain.Enums.PrescriptionStatus.Active;

            var dto = new PrescriptionDto
            {
                Id = request.Id,
                PatientId = request.PatientId,
                AntimicrobialName = request.AntimicrobialName,
                Dose = request.Dose,
                Frequency = request.Frequency,
                Route = request.Route,
                Indication = request.Indication,
                StartDate = request.StartDate,
                ExpectedEndDate = request.ExpectedEndDate,
                PrescriberName = request.PrescriberName,
                PrescriberRole = request.PrescriberRole,
                Status = domainStatus
            };

            await _prescriptionService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpPost("{id}/review")]
        [Authorize(Roles = "InfectionControl")]
        public async Task<IActionResult> MarkReviewed(Guid id)
        {
            await _prescriptionService.MarkReviewedAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/discontinue")]
        [Authorize(Roles = "InfectionControl")]
        public async Task<IActionResult> Discontinue(Guid id)
        {
            await _prescriptionService.DiscontinueAsync(id);
            return NoContent();
        }

        private static PrescriptionResponse MapToResponse(PrescriptionDto dto)
        {
            return new PrescriptionResponse
            {
                Id = dto.Id,
                PatientId = dto.PatientId,
                AntimicrobialName = dto.AntimicrobialName,
                Dose = dto.Dose,
                Frequency = dto.Frequency,
                Route = dto.Route,
                Indication = dto.Indication,
                StartDate = dto.StartDate,
                ExpectedEndDate = dto.ExpectedEndDate,
                PrescriberName = dto.PrescriberName,
                PrescriberRole = dto.PrescriberRole,
                Status = MapToApiStatus(dto.Status)
            };
        }

        private static ApiPrescriptionStatus MapToApiStatus(Domain.Enums.PrescriptionStatus status)
        {
            return status switch
            {
                Domain.Enums.PrescriptionStatus.Active => ApiPrescriptionStatus.Active,
                Domain.Enums.PrescriptionStatus.Reviewed => ApiPrescriptionStatus.Reviewed,
                Domain.Enums.PrescriptionStatus.Discontinued => ApiPrescriptionStatus.Discontinued,
                _ => throw new ArgumentOutOfRangeException(nameof(status), "Unknown status")
            };
        }

        private static Domain.Enums.PrescriptionStatus MapToDomainStatus(ApiPrescriptionStatus apiStatus)
        {
            return apiStatus switch
            {
                ApiPrescriptionStatus.Active => Domain.Enums.PrescriptionStatus.Active,
                ApiPrescriptionStatus.Reviewed => Domain.Enums.PrescriptionStatus.Reviewed,
                ApiPrescriptionStatus.Discontinued => Domain.Enums.PrescriptionStatus.Discontinued,
                _ => throw new ArgumentOutOfRangeException(nameof(apiStatus), "Unknown API status")
            };
        }
    }
}
