
namespace Pass.Manager.Web.Common
{
    public abstract class BaseViewModel : IViewModel
    {
        public virtual string DisplayName
        {
            get { return this.GetType().Name.Replace("ViewModel", string.Empty); }
        }
        public virtual int EntityId { get { return 0; } }
        public virtual bool IsNew
        {
            get { return EntityId <= 0; }
        }

        public virtual string RedirectUrl { get; set; }
    }
}