namespace ExperimentsApi.Services;
using ExperimentsApi.Dtos;
public interface IExperimentService
{
    public Task<List<ExperimentSummaryDto>> GetAllAsync();
    public Task<ExperimentSummaryDto?> GetByIdAsync(int id);
}