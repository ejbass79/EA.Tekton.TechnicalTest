namespace EA.Tekton.TechnicalTest.Cross.Dto;

public class QueryMultipleResponse<T>
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public long TotalCount { get; set; }
    public IEnumerable<T> Result { get; set; }
}
