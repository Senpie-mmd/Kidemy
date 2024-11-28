namespace Kidemy.Application.Services.Interfaces
{
    public interface ICacheManager
    {
        void Flush();
        Task FlushAsync();
        T? Get<T>(string key, Func<T> dataRetriver, TimeSpan? expiration = null);
        Task<T?> GetAsync<T>(string key, Func<Task<T>> dataRetriver, TimeSpan? expiration = null, CancellationToken cancellationToken = default);
        void RemoveByPattern(string pattern);
        Task RemoveByPatternAsync(string pattern);
        void RemoveByPrefix(string prefix);
        Task RemoveByPrefixAsync(string prefix);
        void Set<T>(string key, T cacheValue, TimeSpan? expiration = null);
        Task SetAsync<T>(string key, T cacheValue, TimeSpan? expiration = null, CancellationToken cancellationToken = default);
    }
}