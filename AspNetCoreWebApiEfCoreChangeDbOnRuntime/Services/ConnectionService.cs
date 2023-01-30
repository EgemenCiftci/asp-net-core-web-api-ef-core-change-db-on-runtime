using AspNetCoreWebApiEfCoreChangeDbOnRuntime.Models;

namespace AspNetCoreWebApiEfCoreChangeDbOnRuntime.Services;

public class ConnectionService
{
    private readonly IConfiguration _configuration;

    public ConnectionService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IEnumerable<Connection> GetConnections()
    {
        var dict = _configuration.GetRequiredSection("Connections").Get<Dictionary<string, Connection>>();

        if (dict == null)
        {
            throw new InvalidOperationException("Connections section is missing in appsettings.json.");
        }

        foreach (var item in dict)
        {
            item.Value.Name = item.Key;
            item.Value.ConnectionString = _configuration.GetConnectionString(item.Key);
        }

        return dict.Values;
    }

    public Connection? GetDefaultConnection()
    {
        return GetConnections().FirstOrDefault(f => f.IsDefault);
    }

    public Connection? GetConnection(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return null;
        }

        return GetConnections().FirstOrDefault(f => f.Name == name);
    }
}
