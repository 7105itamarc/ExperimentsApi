using ExperimentsApi.Dtos;
using ExperimentsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExperimentsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExperimentsController : ControllerBase
{
    private readonly IExperimentService _experimentService;
    public ExperimentsController(IExperimentService experimentService)
    {
        _experimentService = experimentService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ExperimentSummaryDto>>> GetAll()
    {
        List<ExperimentSummaryDto> experiments = await _experimentService.GetAllAsync();
        return Ok(experiments);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExperimentSummaryDto>> GetById(int id)
    {
        ExperimentSummaryDto? experiment = await _experimentService.GetByIdAsync(id);
        if (experiment is null) { return NotFound("id doesn't exist"); }
        return Ok(experiment);
    }

    [HttpPost]
    public async Task<ActionResult<ExperimentSummaryDto>> Create(CreateExperimentDto dto)
    {
        try
        {
            ExperimentSummaryDto createdDtoSummary = await _experimentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdDtoSummary.Id }, createdDtoSummary);
            // shortcut for Response.Headers.Location = $"/api/experiments/{created.Id}"; return StatusCode(201, createdDtoSummary);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ExperimentSummaryDto>> Update(int id, UpdateExperimentDto dto)
    {
        try
        {
            ExperimentSummaryDto? updated = await _experimentService.UpdateAsync(id, dto);
            if (updated is null) { return NotFound("id doesn't exist"); }
            return Ok(updated);

        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
