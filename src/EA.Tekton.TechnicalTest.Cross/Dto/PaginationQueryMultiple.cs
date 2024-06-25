namespace EA.Tekton.TechnicalTest.Cross.Dto;

public class PaginationQueryMultiple
{
    public string OrderBy { get; set; }

    public bool Ascendent { get; set; }

    public int PageNumber { get; set; }

    public int ResultsPerPage { get; set; }
}
