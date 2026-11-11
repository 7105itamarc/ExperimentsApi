namespace ExperimentsApi.Models;
public enum ExperimentStatus
{
    Planned,
    InProgress,
    Completed
}

public class Experiment{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Location { get; set; } = "";
    public string Type { get; set; } = "";
    public DateTime Date { get; set; }
    public ExperimentStatus Status { get; set; }

}