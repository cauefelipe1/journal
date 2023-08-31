namespace Journal.Domain.Base;

/// <summary>
/// Defines the structure of a double PK that identifies an entity.
/// </summary>
/// <param name="Id">A numeric Id used to make the relationship across the entities.</param>
/// <param name="SecondaryId">A GUID key that is used in public scenarios.</param>
public record struct ModelDoublePK(long Id, Guid SecondaryId);