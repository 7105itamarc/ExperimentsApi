namespace ExperimentsApi.Dtos;

public class ExperimentSummaryDto
{    
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Location { get; set; } = "";
    public string Type { get; set; } = "";
    public DateTime Date { get; set; }
    public string Status { get; set; } = "";

}