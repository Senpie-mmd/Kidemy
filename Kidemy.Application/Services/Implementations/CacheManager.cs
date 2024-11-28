using EasyCaching.Core;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Domain.Statics;

namespace Kidemy.Application.Services.Implementations
{
    public class CacheManager : ICacheManager
	{
		private readonly IEasyCachingProvider _provider;

		public CacheManager(IEasyCachingProvider provider)
		{
			_provider = provider;
		}

		public T? Get<T>(string key, Func<T> dataRetriver, TimeSpan? expiration = null)
		{
			var result = _provider.Get(key, dataRetriver, expiration ?? TimeSpan.FromMinutes(SiteTools.CachesDefaultExpirationTimeInMinutes));

			if (result.HasValue) return result.Value;

			return default;
		}

		public async Task<T?> GetAsync<T>(string key, Func<Task<T>> dataRetriver, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
		{
			var result = await _provider.GetAsync(
							key,
							dataRetriver,
							expiration ?? TimeSpan.FromMinutes(SiteTools.CachesDefaultExpirationTimeInMinutes),
							cancellationToken);

			if (result.HasValue) return result.Value;

			return default;
		}

		public void Set<T>(string key, T cacheValue, TimeSpan? expiration = null)
		{
			_provider.Set(key, cacheValue, expiration ?? TimeSpan.FromMinutes(SiteTools.CachesDefaultExpirationTimeInMinutes));
		}

		public Task SetAsync<T>(string key, T cacheValue, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
		{
			return _provider.SetAsync(key, cacheValue, expiration ?? TimeSpan.FromMinutes(SiteTools.CachesDefaultExpirationTimeInMinutes), cancellationToken);
		}

		public void RemoveByPrefix(string prefix)
		{
			_provider.RemoveByPrefix(prefix);
		}

		public Task RemoveByPrefixAsync(string prefix)
		{
			return _provider.RemoveByPrefixAsync(prefix);
		}

		public void RemoveByPattern(string pattern)
		{
			_provider.RemoveByPattern(pattern);
		}

		public Task RemoveByPatternAsync(string pattern)
		{
			return _provider.RemoveByPatternAsync(pattern);
		}

		public void Flush()
		{
			_provider.Flush();
		}

		public Task FlushAsync()
		{
			return _provider.FlushAsync();
		}
	}
}
