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
        if (experiment is null) { return NotFound(); }
        return Ok(experiment);
    }
}