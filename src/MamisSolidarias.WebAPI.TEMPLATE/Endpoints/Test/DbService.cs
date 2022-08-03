using MamisSolidarias.Infrastructure.TEMPLATE;

namespace MamisSolidarias.WebAPI.TEMPLATE.Endpoints.Test;

internal class DbService
{
    private readonly TEMPLATEDbContext? _dbContext;

    public DbService() { }
    public DbService(TEMPLATEDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
}