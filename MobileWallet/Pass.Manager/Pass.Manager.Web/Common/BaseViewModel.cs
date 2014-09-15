
namespace Pass.Manager.Web.Common
{
    public abstract class BaseViewModel : IViewModel
    {
        public abstract string DisplayName { get; }
        public abstract int EntityId { get; }
        public bool IsNew
        {
            get { return EntityId > 0; }
        }
    }
}