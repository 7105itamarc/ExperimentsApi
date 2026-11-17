using ExperimentsApi.Dtos;

namespace ExperimentsApi.Services;

public interface IExperimentService
{
    public Task<List<ExperimentSummaryDto>> GetAllAsync();
    public Task<ExperimentSummaryDto?> GetByIdAsync(int id);
    public Task<ExperimentSummaryDto> CreateAsync(CreateExperimentDto dto);
    public Task<ExperimentSummaryDto?> UpdateAsync(int id, UpdateExperimentDto dto);
}