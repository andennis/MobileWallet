
namespace Common.Web.FileUpload
{
    public class UploadAsyncSettingsBuilder
    {
        private readonly UploadAsyncSettings _asyncSettings;

        public UploadAsyncSettingsBuilder(UploadAsyncSettings asyncSettings)
        {
            _asyncSettings = asyncSettings;
        }

        public UploadAsyncSettingsBuilder AutoUpload(bool value)
        {
            _asyncSettings.AutoUpload = value;
            return this;
        }

        public UploadAsyncSettingsBuilder Save(string actionName, string controllerName, object routeValues)
        {
            _asyncSettings.Save = new Navigatable()
            {
                ActionName = actionName, 
                ControllerName = controllerName, 
                RouteValues = routeValues
            };
            return this;
        }

        /*
        public UploadAsyncSettingsBuilder Save<TController>(Expression<Action<TController>> controllerAction)
            where TController : Controller
        {
            return this;
        }
        */

        public UploadAsyncSettingsBuilder SaveField(string fieldName)
        {
            _asyncSettings.SaveField = fieldName;
            return this;
        }

        /*
        public UploadAsyncSettingsBuilder SaveUrl(string url)
        {
            return this;
        }
        */

        public UploadAsyncSettingsBuilder Remove(string actionName, string controllerName, object routeValues)
        {
            _asyncSettings.Save = new Navigatable()
            {
                ActionName = actionName,
                ControllerName = controllerName,
                RouteValues = routeValues
            };
            return this;
        }

        /*
        public UploadAsyncSettingsBuilder Remove<TController>(Expression<Action<TController>> controllerAction)
            where TController : Controller
        {
            return this;
        }

        public UploadAsyncSettingsBuilder RemoveUrl(string url)
        {
            return this;
        }
        */

        public UploadAsyncSettingsBuilder RemoveField(string fieldName)
        {
            _asyncSettings.RemoveField = fieldName;
            return this;
        }
    }
}
