using System.Collections;
using System.Collections.Generic;

namespace Common.Web.Controls.DropDownList
{
    public class DataSource
    {
        public DataSource(){}

        //public IEnumerable<Infrastructure.AggregateResult> AggregateResults { get; set; }
        //public IList<AggregateDescriptor> Aggregates { get; }
        //public bool AutoSync { get; set; }
        //public bool Batch { get; set; }
        //public IDictionary<string, object> CustomTransport { get; set; }
        //public string CustomType { get; set; }
        //public IEnumerable Data { get; set; }
        //public IDictionary<string, object> Events { get; }
        //public IList<IFilterDescriptor> Filters { get; }
        //public IList<GroupDescriptor> Groups { get; }
        //public bool IsClientBinding { get; }
        //public bool IsClientOperationMode { get; }
        //public IDictionary<string, object> OfflineStorage { get; set; }
        //public string OfflineStorageKey { get; set; }
        ////public IList<SortDescriptor> OrderBy { get; }
        //public int Page { get; set; }
        //public int PageSize { get; set; }
        //public IEnumerable RawData { get; set; }
        //public DataSourceSchema Schema { get; }
        //public bool ServerAggregates { get; set; }
        //public bool ServerFiltering { get; set; }
        //public bool ServerGrouping { get; set; }
        //public bool ServerPaging { get; set; }
        //public bool ServerSorting { get; set; }
        //public int Total { get; set; }
        //public int TotalPages { get; set; }
        public Transport transport { get; set; }
        //public Transport Transport { get; protected set; }
        //public DataSourceType? Type { get; set; }

        //public void ModelType(Type modelType);
        //public void Process(DataSourceRequest request, bool processData);
        //protected override void Serialize(IDictionary<string, object> json);
    }
}
