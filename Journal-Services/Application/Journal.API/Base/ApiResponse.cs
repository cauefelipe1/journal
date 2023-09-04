namespace Journal.API.Base;

/// <summary>
/// Defines a model for any Api response.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed record ApiResponse<T>
{
    /// <summary>
    /// The data to be returned.
    /// </summary>
    public T? Data { get; private set; }

    /// <summary>
    /// Flag that indicates whether success on the request.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; private set; }

    /// <summary>
    /// When the request is not checked as success, it contains the failure's details.
    /// Empty or null otherwise.
    /// </summary>
    /// <example>THe current event is behind the last odometer for provided date.</example>
    public string? Details { get; private set; }


    // Just to make the class be created only from the template methods.
    private ApiResponse() {}

    /// <summary>
    /// Creates an instance for successful requests.
    /// </summary>
    /// <param name="data"><see cref="Data"/></param>
    /// <typeparam name="T"><see cref="T"/></typeparam>
    /// <returns>An ApiResponse instance.</returns>
    public static ApiResponse<T> WithSuccess(T data)
    {
        var response = new ApiResponse<T>
        {
            Success = true,
            Data = data,
            Details = null
        };

        return response;
    }

    /// <summary>
    /// Creates an instance for non-successful requests.
    /// </summary>
    /// <param name="details"><see cref="Details"/></param>
    /// <typeparam name="T"><see cref="T"/></typeparam>
    /// <returns>An ApiResponse instance.</returns>
    public static ApiResponse<T?> NonSuccess(string details)
    {
        var response = new ApiResponse<T?>
        {
            Success = false,
            Data = default,
            Details = details
        };

        return response;
    }
}