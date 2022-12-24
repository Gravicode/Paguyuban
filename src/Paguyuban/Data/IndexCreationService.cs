

using Paguyuban.Data;
using Paguyuban.Models;

namespace Redis.OM.Skeleton.HostedServices;

public class IndexCreationService 
{
    private readonly RedisConnectionProvider _provider;
    public IndexCreationService()
    {
        _provider = new RedisConnectionProvider(AppConstants.RedisCon);

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
    }

}