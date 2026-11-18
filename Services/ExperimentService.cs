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
    public async Task<ExperimentSummaryDto> CreateAsync(CreateExperimentDto dto)
    {
        ExperimentStatus status = ParseStatus(dto.Status);
        if (dto.Date is null) { throw new ArgumentException("Date is Required"); }

        List<Experiment> experiments = await _repository.GetAllAsync();
        int newId = experiments.Count == 0 ? 1 : experiments.Max(ex => ex.Id) + 1;


        Experiment newExperiment = new Experiment
        {
            Id = newId,
            Name = dto.Name,
            Location = dto.Location,
            Type = dto.Type,
            Date = dto.Date.Value,
            Status = status
        };
        experiments.Add(newExperiment);
        await _repository.SaveAllAsync(experiments);

        return ToSummaryDto(newExperiment);
    }

    private static ExperimentStatus ParseStatus(string textStatus)
    {
        bool statusOk = Enum.TryParse(textStatus, true, out ExperimentStatus status);

        if (!statusOk || !Enum.IsDefined(status)) { throw new ArgumentException("Invalid status"); }

        return status;
    }

    public async Task<ExperimentSummaryDto?> UpdateAsync(int id, UpdateExperimentDto dto)
    {
        ExperimentStatus status = ParseStatus(dto.Status);
        if (dto.Date is null) { throw new ArgumentException("Date is Required"); }

        List<Experiment> experiments = await _repository.GetAllAsync();
        Experiment? toChange = experiments.FirstOrDefault(ex => ex.Id == id);

        if (toChange is null) { return null; }
        toChange.Name = dto.Name;
        toChange.Location = dto.Location;
        toChange.Type = dto.Type;
        toChange.Date = dto.Date.Value;
        toChange.Status = status;

        await _repository.SaveAllAsync(experiments);
        return ToSummaryDto(toChange);

    }

    public async Task<bool> DeleteAsync(int id)
    {

        List<Experiment> experiments = await _repository.GetAllAsync();
        Experiment? toDelete = experiments.FirstOrDefault(ex => ex.Id == id);
        if (toDelete is null) { return false; }
        experiments.Remove(toDelete);
        await _repository.SaveAllAsync(experiments);
        return true;
    }
}