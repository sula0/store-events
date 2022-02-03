using Microsoft.AspNetCore.Mvc;
using Store.Core.Domain;
using Store.Core.Domain.ErrorHandling;

namespace Store.Core.Infrastructure.AspNet;

public static class ControllerBaseExtensions
{
    public static IActionResult HandleError(this ControllerBase controller, Error error)
    {
        Ensure.NotNull(error);
            
        return error switch
        {
            NotFoundError => controller.NotFound(),
            _ => controller.BadRequest(error.Message)
        };
    }

    public static PagingParams CreatePagingParams<TKey>
    (
        this ControllerBase controller, 
        TKey nextCursor, 
        TKey previousCursor, 
        int limit
    ) => new(
            nextCursor, 
            previousCursor,
            limit,
            controller.Request.Query);
}