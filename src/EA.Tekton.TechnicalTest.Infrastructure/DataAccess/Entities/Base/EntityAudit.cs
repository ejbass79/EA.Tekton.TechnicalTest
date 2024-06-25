namespace EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Entities.Base;

public abstract class EntityAudit
{
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public string CreationUser { get; set; } = string.Empty;
    public DateTime? ModificationDate { get; set; } = DateTime.Now;
    public string? ModificationUser { get; set; } = string.Empty;
    public bool? Deleted { get; set; } = false;
}
