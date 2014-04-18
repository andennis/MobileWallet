using Common.Repository;

namespace Common.Configuration
{
    public class AppDbConfig : AppConfigBase, IDbConfig
    {
        public AppDbConfig(string sectionName = null)
            :base(sectionName)
        {
        }

        public string ConnectionString
        {
            get { return GetValue("ConnectionStringName"); }
        }
    }
}
