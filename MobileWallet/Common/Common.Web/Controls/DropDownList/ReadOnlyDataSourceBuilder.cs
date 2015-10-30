using System;
using System.Web;
using System.Web.Mvc;

namespace Common.Web.Controls.DropDownList
{
    public class ReadOnlyDataSourceBuilder
    {
        //private readonly ViewContext _viewContext;
        private readonly DataSource _dataSource;

        public ReadOnlyDataSourceBuilder( /*DataSource dataSource, ViewContext viewContext*/)

        {
            //_viewContext = viewContext;
        }

        public ReadOnlyDataSourceBuilder(DataSource dataSource/*, ViewContext viewContext*/)
        {
            _dataSource = dataSource;
        }

        public ReadOnlyDataSourceBuilder Read(Action<CrudOperationBuilder> configurator)
        {
            var crudOperationBuilder = new CrudOperationBuilder(new CrudOperation(), new ViewContext());
            configurator(crudOperationBuilder);
            _dataSource.transport = new Transport {read = new CrudOperation()};
            var url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            _dataSource.transport.read.url = url.Action(crudOperationBuilder._operation.ActionName, crudOperationBuilder._operation.ControllerName);
            return this;
        }
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
