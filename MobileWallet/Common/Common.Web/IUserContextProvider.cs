
namespace Common.Web
{
    public interface IUserContextProvider
    {
        string ContextId { get; }
        T GetCustomProperty<T>(string propName);
        void SetCustomProperty<T>(string propName, T propValue);
        void Clear();
    }
}