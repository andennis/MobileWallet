using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Common.Web
{
    public static class FormControlExtensions
    {
        public static MvcHtmlString TextBoxForExt<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            return html.TextBoxFor(expression, htmlAttributes);
        }

        public static MvcHtmlString TextBoxFormForExt<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null, string label = null)
        {
            var builder1 = new TagBuilder("div");
            builder1.AddCssClass("col-sm-9 col-md-10");
            builder1.InnerHtml = html.TextBoxFor(expression, new {@class = "form-control"}).ToHtmlString();

            var builder2 = new TagBuilder("div");
            builder2.AddCssClass("form-group");
            builder2.InnerHtml = html.LabelFor(expression, label, new { @class = "control-label col-sm-3 col-md-2" }).ToHtmlString() + builder1;

            return new MvcHtmlString(builder2.ToString());
        }

        public static MvcHtmlString LabelForExt<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string labelText, object htmlAttributes = null)
        {
            return html.LabelFor(expression, labelText, htmlAttributes);
        }

        public static MvcHtmlString HiddenForExt<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            return html.HiddenFor(expression);

        }

        public static MvcHtmlString PasswordForExt<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            return html.PasswordFor(expression, htmlAttributes);
        }

        public static MvcHtmlString CheckBoxForEx<TModel>(this HtmlHelper<TModel> html, Expression<Func<TModel, bool>> expression, object htmlAttributes = null)
        {
            return html.CheckBoxFor(expression, htmlAttributes);
        }
    }
}
