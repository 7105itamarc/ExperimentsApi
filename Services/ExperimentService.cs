using ExperimentsApi.Dtos;
using ExperimentsApi.Repositories;
using ExperimentsApi.Models;

namespace ExperimentsApi.Services;


public class ExperimentService : IExperimentService
{
    private readonly IExperimentRepository _repository;

    public ExperimentService(IExperimentRepository repository)
    {
        _repository = repository;
    }
    public async Task<List<ExperimentSummaryDto>> GetAllAsync()
    {
        List<Experiment> experiments = await _repository.GetAllAsync();
        return experiments.Select(ex => ToSummaryDto(ex)).ToList();
    }
    public async Task<ExperimentSummaryDto?> GetByIdAsync(int id)
    {
        Experiment? ex = await _repository.GetByIdAsync(id);
        if (ex is null) { return null; }
        return ToSummaryDto(ex);
    }

    private static ExperimentSummaryDto ToSummaryDto(Experiment experiment)
    {
        return new ExperimentSummaryDto
        {
            Id = experiment.Id,
            Name = experiment.Name,
            Date = experiment.Date,
            Location = experiment.Location,
            Type = experiment.Type,
            Status = experiment.Status.ToString()
        };
    }
}