using System.Web.Mvc;

namespace Common.Web.Controls.DropDownList
{
    public class ReadOnlyDataSourceBuilder
    {
        //private readonly ViewContext _viewContext;

        public ReadOnlyDataSourceBuilder( /*DataSource dataSource, ViewContext viewContext*/)

        {
            //_viewContext = viewContext;
        }
        //public ReadOnlyDataSourceBuilder Read(Action<CrudOperationBuilder> configurator);
        public ReadOnlyDataSourceBuilder Read(string actionName, string controllerName, object routeValues)
        {
            return this;
        }

        public ReadOnlyDataSourceBuilder Read(string actionName, string controllerName)
        {
            return this;
        }
        //public ReadOnlyDataSourceBuilder ServerFiltering();
        //public ReadOnlyDataSourceBuilder ServerFiltering(bool enabled);
        //public ReadOnlyDataSourceBuilder Events(Action<DataSourceEventBuilder> configurator);
        //protected virtual void SetOperationUrl(CrudOperation operation, string actionName, string controllerName, object routeValues);

    }
}
