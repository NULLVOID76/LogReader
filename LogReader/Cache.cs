using Microsoft.Extensions.Caching.Memory;
using System;

namespace LogReader
{
    internal class Cache
    {

        private readonly IMemoryCache _cache;

        private readonly TimeSpan _ttl = TimeSpan.FromMinutes(1);

        public Cache()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
        }
        public bool TryMarkAndRun(string psn)
        {
            try
            {
                if (_cache.TryGetValue(psn, out _))
                    return false;

                _cache.Set(psn, true, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = _ttl
                });

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Cache error for PSN {psn}: {ex.Message}");
                return true; // Allow processing if cache fails
            }
        }
    }

}

