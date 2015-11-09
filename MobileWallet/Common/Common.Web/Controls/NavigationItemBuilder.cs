using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using Common.Extensions;

namespace Common.Web.Controls
{
    public abstract class NavigationItemBuilder<TItem, TBuilder>
        where TItem : NavigationItem<TItem>
        where TBuilder : NavigationItemBuilder<TItem, TBuilder>
    {
        protected readonly NavigationItem<TItem> _item;

        protected NavigationItemBuilder(NavigationItem<TItem> item/*, ViewContext viewContext*/)
        {
            _item = item;
            //ViewContext = viewContext;
        }

        public TBuilder HtmlAttributes(object attributes)
        {
            _item.HtmlAttributes.Clear();
            foreach (var attr in attributes.ObjectPropertiesToDictionary())
                _item.HtmlAttributes.Add(attr);
            
            return (TBuilder)this;
        }
        public TBuilder HtmlAttributes(IDictionary<string, object> attributes)
        {
            _item.HtmlAttributes.Clear();
            foreach (var attr in attributes)
                _item.HtmlAttributes.Add(attr);

            return (TBuilder)this;
        }
        public TBuilder LinkHtmlAttributes(object attributes)
        {
            _item.LinkHtmlAttributes.Clear();
            foreach (var attr in attributes.ObjectPropertiesToDictionary())
                _item.LinkHtmlAttributes.Add(attr);

            return (TBuilder)this;
        }
        public TBuilder LinkHtmlAttributes(IDictionary<string, object> attributes)
        {
            _item.LinkHtmlAttributes.Clear();
            foreach (var attr in attributes)
                _item.LinkHtmlAttributes.Add(attr);

            return (TBuilder)this;
        }
        public TBuilder Text(string value)
        {
            _item.Text = value;
            return (TBuilder)this;
        }
        public TBuilder Visible(bool value)
        {
            _item.Visible = value;
            return (TBuilder)this;
        }
        public TBuilder Enabled(bool value)
        {
            _item.Enabled = value;
            return (TBuilder)this;
        }
        public TBuilder Selected(bool value)
        {
            _item.Selected = value;
            return (TBuilder)this;
        }

        /*
        public TBuilder Route(string routeName, RouteValueDictionary routeValues)
        {
            return (TBuilder)this;
        }
        public TBuilder Route(string routeName, object routeValues)
        {
            return (TBuilder)this;
        }
        public TBuilder Route(string routeName)
        {
            return (TBuilder)this;
        }
        */

        public TBuilder Action(RouteValueDictionary routeValues)
        {
            _item.RouteValues = routeValues;
            return (TBuilder)this;
        }
        public TBuilder Action(string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            _item.ActionName = actionName;
            _item.ControllerName = controllerName;
            _item.RouteValues = routeValues;
            return (TBuilder)this;
        }
        public TBuilder Action(string actionName, string controllerName, object routeValues)
        {
            _item.ActionName = actionName;
            _item.ControllerName = controllerName;
            _item.RouteValues = new RouteValueDictionary(routeValues);
            return (TBuilder)this;
        }
        public TBuilder Action(string actionName, string controllerName)
        {
            _item.ActionName = actionName;
            _item.ControllerName = controllerName;
            return (TBuilder)this;
        }
        public TBuilder Url(string value)
        {
            _item.Url = value;
            return (TBuilder)this;
        }
        public TBuilder ImageUrl(string value)
        {
            _item.ImageUrl = value;
            return (TBuilder)this;
        }
        public TBuilder ImageHtmlAttributes(object attributes)
        {
            _item.ImageHtmlAttributes.Clear();
            foreach (var attr in attributes.ObjectPropertiesToDictionary())
                _item.ImageHtmlAttributes.Add(attr);

            return (TBuilder)this;
        }
        public TBuilder ImageHtmlAttributes(IDictionary<string, object> attributes)
        {
            _item.ImageHtmlAttributes.Clear();
            foreach (var attr in attributes)
                _item.ImageHtmlAttributes.Add(attr);
            return (TBuilder)this;
        }
        public TBuilder SpriteCssClasses(params string[] cssClasses)
        {
            throw new NotImplementedException();
            //return (TBuilder)this;
        }
        public TBuilder Content(Action value)
        {
            _item.Content = value;
            return (TBuilder)this;
        }
        /*
        public TBuilder Content(Func<object, object> value)
        {
            return (TBuilder)this;
        }
        public TBuilder Content(string value)
        {
            return (TBuilder)this;
        }
        */
        public TBuilder ContentHtmlAttributes(object attributes)
        {
            _item.ContentHtmlAttributes.Clear();
            foreach (var attr in attributes.ObjectPropertiesToDictionary())
                _item.ContentHtmlAttributes.Add(attr);

            return (TBuilder)this;
        }
        public TBuilder ContentHtmlAttributes(IDictionary<string, object> attributes)
        {
            _item.ContentHtmlAttributes.Clear();
            foreach (var attr in attributes)
                _item.ContentHtmlAttributes.Add(attr);

            return (TBuilder)this;
        }
        public TBuilder Action<TController>(Expression<Action<TController>> controllerAction) where TController : Controller
        {
            string cn = typeof (TController).Name;
            int cl = "Controller".Length;
            _item.ControllerName = cn.Remove(cn.Length - cl, cl);
            _item.ActionName = controllerAction.GetMethodName();
            return (TBuilder)this;
        }
        public TBuilder Encoded(bool isEncoded)
        {
            _item.Encoded = isEncoded;
            return (TBuilder)this;
        }
        //public ViewContext ViewContext { get; set; }
        protected TItem Item { get { return (TItem)_item; } }

    }
}
