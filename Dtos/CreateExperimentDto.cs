using System.ComponentModel.DataAnnotations;

namespace ExperimentsApi.Dtos;

public class CreateExperimentDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = "";
    [Required]
    public string Location { get; set; } = "";
    [Required]
    public string Type { get; set; } = "";
    [Required]
    public DateTime? Date { get; set; }
    [Required]
    public string Status { get; set; } = "";

}