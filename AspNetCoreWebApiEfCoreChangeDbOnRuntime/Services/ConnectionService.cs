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
        Dictionary<string, Connection>? dict = _configuration.GetRequiredSection("Connections").Get<Dictionary<string, Connection>>();

        if (dict == null)
        {
            throw new InvalidOperationException("Connections section is missing in appsettings.json.");
        }

        foreach (KeyValuePair<string, Connection> item in dict)
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
        return string.IsNullOrWhiteSpace(name) ? null : GetConnections().FirstOrDefault(f => f.Name == name);
    }
}
