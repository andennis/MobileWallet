using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Pass.Container.BL.Caching
{
    public static class FileCacheStore
    {
        static readonly ObjectCache Cache = MemoryCache.Default;

        public static bool Exist(string key)
        {
            var fileContents = Cache[key] as string;
            return fileContents!= null;
        }

        public static string Add(string key, string filePath, DateTimeOffset dateTimeOffset)
        {
            var policy = new CacheItemPolicy { AbsoluteExpiration = dateTimeOffset };
            var filePaths = new List<string> { filePath };

            policy.ChangeMonitors.Add(new HostFileChangeMonitor(filePaths));

            // Fetch the file contents.
            string fileContents = File.ReadAllText(filePath);
            Cache.Set(key, fileContents, policy);
            return fileContents;
        }

        public static string Get(string key)
        {
            return Cache[key] as string;
        }

        public static void Remove(string key)
        {
            var fileContents = Cache[key] as string;
            if (fileContents != null)
                Cache.Remove(key);
        }
    }
}
