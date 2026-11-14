using System.Text.Json;
using System.Text.Json.Serialization;
using ExperimentsApi.Models;

namespace ExperimentsApi.Repositories;

public class JsonExperimentRepository : IExperimentRepository
{
    private readonly string _filePath = Path.Combine("Data", "experiments.json");

    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public async Task<List<Experiment>> GetAllAsync()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Experiment>();
        }

        string json = await File.ReadAllTextAsync(_filePath);
        return JsonSerializer.Deserialize<List<Experiment>>(json, _options) ?? new List<Experiment>();
    }

    public async Task<Experiment?> GetByIdAsync(int id)
    {
        List<Experiment> experiments = await GetAllAsync();
        return experiments.FirstOrDefault(ex => ex.Id == id);
    }

    // להשלים את השלישי באנטרפייס



}
