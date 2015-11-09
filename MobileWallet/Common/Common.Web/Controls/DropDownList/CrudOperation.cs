using System.Web.Routing;

namespace Common.Web.Controls.DropDownList
{
    public class CrudOperation
    {
        public string ActionName { get; set; }
        public bool Cache { get; set; }
        public string ContentType { get; set; }
        public string ControllerName { get; set; }
        //public ClientHandlerDescriptor Data { get; set; }
        public string DataType { get; set; }
        public string RouteName { get; set; }
        public RouteValueDictionary RouteValues { get; set; }
        public string Type { get; set; }
        public string url { get; set; }

        //protected override void Serialize(IDictionary<string, object> json);
    }
}
