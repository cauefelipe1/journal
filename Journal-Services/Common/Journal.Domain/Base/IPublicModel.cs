namespace Journal.Domain.Base;

/// <summary>
/// Interface that defines a public model for the application.
/// </summary>
/// <typeparam name="TPublicModel">The public model itself.</typeparam>
/// <typeparam name="TBusinessModel">The real business model that the <see cref="TPublicModel"/> represents.</typeparam>
public interface IPublicModel<in TBusinessModel, out TPublicModel>
    where TPublicModel : IPublicModel<TBusinessModel, TPublicModel>, new()
    where TBusinessModel : new()
{
    /// <summary>
    /// Creates an instance of <see cref="TPublicModel"/> based on the <see cref="TBusinessModel"/>.
    /// </summary>
    /// <param name="source">The <see cref="TBusinessModel"/> instance.</param>
    /// <returns>The <see cref="TPublicModel"/> instance.</returns>
    static abstract TPublicModel FromModel(TBusinessModel source);
}