using EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Entities.Base;

namespace EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Entities;

public class Product : EntityAudit
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int StatusId { get; set; }
    public int Stock { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal? Price { get; set; }
}