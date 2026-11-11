using ExperimentsApi.Models;
namespace ExperimentsApi.Repositories;


public interface IExperimentRepository
{
    Task<List<Experiment>> GetAllAsync();
    Task<Experiment?> GetByIdAsync(int id);
    Task SaveAllAsync(List<Experiment> experiments);

}