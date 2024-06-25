using System.Net;

namespace EA.Tekton.TechnicalTest.Cross.Dto;

public class ResponseService<T>
{
    public bool Status { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }
    public string? Message { get; set; }
    public T Data { get; set; }

    public ResponseService()
    {
        Status = false;
        Message = string.Empty;
    }
}
