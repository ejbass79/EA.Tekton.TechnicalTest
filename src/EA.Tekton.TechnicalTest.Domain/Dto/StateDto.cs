namespace EA.Tekton.TechnicalTest.Domain.Dto;

public class StateDto
{
    public int StatusId { get; set; }
    public string StatusName { get; set; }
    public DateTime CreationDate { get; set; }
    public string CreationUser { get; set; }
    public DateTime? ModificationDate { get; set; }
    public string? ModificationUser { get; set; }
    public bool? Deleted { get; set; }
}