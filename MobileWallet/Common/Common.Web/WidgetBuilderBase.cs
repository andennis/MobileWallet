using System.Web;

namespace Common.Web
{
    public class WidgetBuilderBase<TViewComponent, TBuilder> : IHtmlString
        where TViewComponent : WidgetBase
        where TBuilder : WidgetBuilderBase<TViewComponent, TBuilder>
    {
        protected readonly TViewComponent _component;

        public WidgetBuilderBase(TViewComponent component)
        {
            _component = component;
        }

        public virtual TBuilder Name(string componentName)
        {
            _component.Name = componentName;
            return (TBuilder)this;
        }

        public virtual string Render()
        {
            return _component.Render();
        }

        public string ToHtmlString()
        {
            return Render();
        }
    }
}
