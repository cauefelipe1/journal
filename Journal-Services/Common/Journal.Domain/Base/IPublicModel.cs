namespace Journal.Domain.Base;

/// <summary>
/// Interface that defines a public model for the application.
/// </summary>
/// <typeparam name="TPublicModel">The public model itself.</typeparam>
/// <typeparam name="TDomainModel">The real business model that the class represents.</typeparam>
public interface IPublicModel<in TDomainModel, out TPublicModel>
    where TPublicModel : IPublicModel<TDomainModel, TPublicModel>, new()
    where TDomainModel : new()
{
    /// <summary>
    /// Creates an instance of the public model based on the business model.
    /// </summary>
    /// <param name="source">The domain instance.</param>
    /// <returns>The public model instance.</returns>
    static abstract TPublicModel FromModel(TDomainModel source);
}