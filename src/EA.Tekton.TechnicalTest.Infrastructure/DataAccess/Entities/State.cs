using EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Entities.Base;

namespace EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Entities;

public class State : EntityAudit
{
    public int StatusId { get; set; }
    public string StatusName { get; set; } = string.Empty;
}