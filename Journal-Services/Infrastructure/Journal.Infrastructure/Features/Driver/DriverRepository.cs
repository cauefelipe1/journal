using Journal.Domain.Base;
using Journal.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Journal.Infrastructure.Features.Driver;

/// <inheritdoc/>
public class DriverRepository : IDriverRepository
{
    private readonly DatabaseContext _dbContext;

    public DriverRepository(DatabaseContext dbContext) => _dbContext = dbContext;

    /// <inheritdoc/>
    public DriverDTO? GetDriverById(int driverId)
    {
        var driver =
            _dbContext.Driver
                .Where(d => d.DriverId == driverId)
                .AsNoTracking()
                .FirstOrDefault();

        return driver;
    }

    /// <inheritdoc/>
    public DriverDTO? GetDriverBySecondaryId(Guid secondaryId)
    {
        var driver =
            _dbContext.Driver
                .Where(d => d.SecondaryId == secondaryId)
                .AsNoTracking()
                .FirstOrDefault();

        return driver;
    }

    /// <inheritdoc/>
    public ModelDoublePK InsertDriver(DriverDTO dto)
    {
        _dbContext.Driver.Add(dto);
        _dbContext.SaveChanges();

        return new ModelDoublePK(dto.DriverId, dto.SecondaryId);
    }
}