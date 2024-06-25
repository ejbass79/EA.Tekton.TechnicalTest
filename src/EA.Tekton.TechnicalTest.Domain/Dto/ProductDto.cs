namespace EA.Tekton.TechnicalTest.Domain.Dto;

public class ProductDto
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public int StatusId { get; set; }
    public int Stock { get; set; }
    public string Description { get; set; }
    public decimal? Price { get; set; }
    public DateTime CreationDate { get; set; }
    public string CreationUser { get; set; }
    public DateTime? ModificationDate { get; set; }
    public string? ModificationUser { get; set; }
    public bool? Deleted { get; set; }
}