using Bingogo.Core;
using Bingogo.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace Bingogo.WebApi.Http;

public class ExceptionHandler
{
    public bool IncludeExceptionDetails { get; init; }

    public Task Handle(HttpContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        var result = GetProblemResult(context);
        result.ProblemDetails.Extensions["traceId"] = Activity.Current?.Id ?? context.TraceIdentifier;
        return result.ExecuteAsync(context);
    }

    private ProblemHttpResult GetProblemResult(HttpContext context)
    {
        if (context.Features.Get<IExceptionHandlerFeature>() is { } feature)
            return GetProblemResult(feature);

        return TypedResults.Problem();
    }

    private ProblemHttpResult GetProblemResult(IExceptionHandlerFeature feature)
    {
        var problem = GetProblem(feature.Error);
        problem.Instance ??= feature.Path;
        return TypedResults.Problem(problem);
    }

    private ProblemDetails GetProblem(Exception error) => error switch
    {
        EntityNotFoundException => Problems.NotFound(),
        UnauthorizedAccessException => Problems.Unauthorized(),
        ApplicationException e => Problems.BadRequest(e.Message),
        DbUpdateException e => Problems.BadRequest(e.InnerException.Message),
        _ => Unhandled(error),
    };

    private ProblemDetails Unhandled(Exception error)
    {
        var problem = new ProblemDetails();

        if (IncludeExceptionDetails)
        {
            problem.Detail = error.Message;
            problem.Extensions["stackTrace"] = error
                .StackTrace?
                .Split(Environment.NewLine)
                .Select(s => s.Trim());
        }

        return problem;
    }
}