using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Primitives;
using Store.Core.Domain;

namespace Store.Core.Infrastructure.AspNet;

public class CursorHandler
{
    private const string CursorQueryParameterName = "cursor";
    private readonly ISerializer _serializer;

    public CursorHandler(ISerializer serializer)
        => _serializer = Ensure.NotNull(serializer);
    
    public T Parse<T>(string composedCursor)
    {
        if (composedCursor == default) return default;

        string decoded = Encoding.UTF8.GetString(
            Convert.FromBase64String(composedCursor));
        return _serializer.Deserialize<T>(decoded);
    }

    public string Compose<T>(T data)
    {
        if (data == null) return null;
        
        return Convert.ToBase64String(
            _serializer.SerializeToBytes(data));
    }

    public (string, string) BuildCursorQueries(
        string nextCursor, 
        string previousCursor, 
        IEnumerable<KeyValuePair<string, StringValues>> otherQueryParameters, 
        string cursorQueryParameterName = CursorQueryParameterName)
    {
        List<KeyValuePair<string, StringValues>> queryParameters = otherQueryParameters
            .Where(kv => kv.Key != cursorQueryParameterName)
            .ToList(); 
        
        QueryBuilder nextQueryBuilder = null;
        if (nextCursor != null)
        {
            nextQueryBuilder = new();

            foreach (var (key, value) in queryParameters)
            {
                if (value.Any())
                {
                    nextQueryBuilder.Add(key, value.AsEnumerable());
                }
            }
            
            nextQueryBuilder.Add(cursorQueryParameterName, nextCursor);
        }
        
        QueryBuilder previousQueryBuilder = null;
        if (previousCursor != null)
        {
            previousQueryBuilder = new();
            
            foreach (var (key, value) in queryParameters)
            {
                if (value.Any())
                {
                    previousQueryBuilder.Add(key, value.AsEnumerable());
                }
            }
            previousQueryBuilder.Add(cursorQueryParameterName, previousCursor);
        }
        
        return (
            nextQueryBuilder?.ToString(), 
            previousQueryBuilder?.ToString()
        );
    }
}