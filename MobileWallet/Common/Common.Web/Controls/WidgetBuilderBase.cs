using System.Collections.Generic;
using System.Web;
using Common.Extensions;

namespace Common.Web.Controls
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

        public virtual TBuilder HtmlAttributes(object attributes)
        {
            if (attributes == null)
                return (TBuilder) this;

            return HtmlAttributes(attributes.ObjectPropertiesToDictionary());
        }

        public virtual TBuilder HtmlAttributes(IDictionary<string, object> attributes)
        {
            _component.HtmlAttributes.AddRange(attributes);
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
