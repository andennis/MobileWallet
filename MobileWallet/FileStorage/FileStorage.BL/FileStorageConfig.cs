using Common.Configuration;
using FileStorage.Core;

namespace FileStorage.BL
{
    public sealed class FileStorageConfig : AppConfigBase, IFileStorageConfig
    {
        public FileStorageConfig()
            : base("FileStorage")
        {
        }

        public int StorageDeep
        {
            get
            {
                int n;
                if (int.TryParse(GetValue("StorageDeep"), out n))
                    return n;

                return -1;
            }
        }
        public int MaxItemsNumber
        {
            get
            {
                int n;
                if (int.TryParse(GetValue("MaxItemsNumber"), out n))
                    return n;

                return -1;
            }
        }
        public string StoragePath
        {
            get { return GetValue("StoragePath"); }
        }
        public string ConnectionString
        {
            get { return GetValue("ConnectionStringName"); }
        }
    }
}
