

using Paguyuban.Data;
using Paguyuban.Models;
using StackExchange.Redis;

namespace Redis.OM.Skeleton.HostedServices;

public class IndexCreationService 
{
    private readonly RedisConnectionProvider _provider;
    public IndexCreationService()
    {
        var options = ConfigurationOptions.Parse(AppConstants.RedisCon); // host1:port1, host2:port2, ...
        if (!string.IsNullOrEmpty(AppConstants.RedisPassword))
        {

            options.Password = AppConstants.RedisPassword;

        }
        _provider = new RedisConnectionProvider(options);

    }

    public async Task CreateIndex()
    {
        await _provider.Connection.CreateIndexAsync(typeof(Log));
        await _provider.Connection.CreateIndexAsync(typeof(UserProfile));
        await _provider.Connection.CreateIndexAsync(typeof(MessageBox));
        await _provider.Connection.CreateIndexAsync(typeof(GroupMessage));
        await _provider.Connection.CreateIndexAsync(typeof(Notification));
        await _provider.Connection.CreateIndexAsync(typeof(Note));
        await _provider.Connection.CreateIndexAsync(typeof(Todo));
        await _provider.Connection.CreateIndexAsync(typeof(Contact));
        await _provider.Connection.CreateIndexAsync(typeof(MessageDetail));
        await _provider.Connection.CreateIndexAsync(typeof(CallLog));
    }

}