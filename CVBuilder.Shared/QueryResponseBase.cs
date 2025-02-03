using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Shared;

public class QueryResponseBase<T>
{
    public T Data { get; set; } = default!;
    public ProblemDetails? ProblemDetails { get; set; }
}