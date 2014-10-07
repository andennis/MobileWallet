﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Common.Extensions;
using Common.Web.FileUpload;

namespace Common.Web
{
    public static class FormControlExtensions
    {
        private static readonly IDictionary<string, object> _initControlAttributes  = new Dictionary<string, object>()
                                                                                  {
                                                                                      {"class", "form-control"}
                                                                                  };
        private static readonly IDictionary<string, object> _initFormAttributes = new Dictionary<string, object>()
                                                                              {
                                                                                  {"class", "form-horizontal"},
                                                                                  {"role", "form" }
                                                                              };

        public static MvcHtmlString LabelForExt<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string labelText, object htmlAttributes = null)
        {
            return html.LabelFor(expression, labelText, htmlAttributes);
        }

        public static MvcHtmlString HiddenForExt<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            return html.HiddenFor(expression);
        }

        #region BeginForm
        public static MvcForm BeginFormExt<TController>(this HtmlHelper html, Expression<Func<TController, ActionResult>> action) where TController : Controller
        {
            ActionInfo actionInfo = GetActionInfo(action);
            return html.BeginFormExt(actionInfo.Action, actionInfo.Controller);
        }
        public static MvcForm BeginFormExt(this HtmlHelper html, string actionName = null, string controllerName = null)
        {
            return html.BeginForm(actionName, controllerName, FormMethod.Post, _initFormAttributes);
        }
        #endregion

        #region FormAction
        public static MvcHtmlString FormActionSubmit<TModel>(this HtmlHelper<TModel> html, string name, string caption, string actionUrl = null)
        {
            var tb = new TagBuilder("input");
            tb.Attributes.Add("id", name);
            tb.Attributes.Add("type", "submit");
            tb.Attributes.Add("value", caption);
            tb.AddCssClass("btn btn-default btn-primary");
            if (actionUrl != null)
                tb.Attributes.Add("data-action", actionUrl);

            return new MvcHtmlString(tb.ToString());
        }
        public static MvcHtmlString FormActionButton<TModel>(this HtmlHelper<TModel> html, string name, string caption, string actionUrl)
        {
            var tb = new TagBuilder("input");
            tb.Attributes.Add("id", name);
            tb.Attributes.Add("type", "button");
            tb.Attributes.Add("value", caption);
            tb.AddCssClass("btn btn-default");
            tb.Attributes.Add("data-action", actionUrl);

            return new MvcHtmlString(tb.ToString());
        }
        #endregion

        #region Partial

        public static MvcHtmlString PartialEx(this HtmlHelper html, string partialViewName, object model = null, ViewDataDictionary viewData = null)
        {
            return html.Partial(partialViewName, model, viewData);
        }

        #endregion

        #region TextBox
        public static MvcHtmlString TextBlockForExt<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, string format = null/*, object htmlAttributes = null*/)
        {
            return html.TextBlockFor(expression, format);
        }
        public static MvcHtmlString TextBlockFormForExt<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, string labelText = null, string format = null)
        {
            return html.LabelWithControl(expression, labelText, null, () => html.TextBlockFor(expression, format));
        }

        private static MvcHtmlString TextBlockFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, string format = null)
        {
            if (typeof(TProperty) == typeof(DateTime))
                if (string.IsNullOrEmpty(format))
                    format = "d";

            string dataFmt = (!string.IsNullOrEmpty(format) ? string.Format("{{0:{0}}}", format) : null);
            var attributes = _initControlAttributes.Union(new Dictionary<string, object>() { { "readonly", "readonly" } }).ToDictionary(key => key.Key, val => val.Value);
            return html.TextBoxFor(expression, dataFmt, attributes);
        }

        #endregion

        #region TextBox
        public static MvcHtmlString TextBoxForExt<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            return html.TextBoxFor(expression, htmlAttributes);
        }
        public static MvcHtmlString TextBoxFormForExt<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, string labelText = null)
        {
            return html.LabelWithControl(expression, labelText, null, () => html.TextBoxFor(expression, _initControlAttributes));
        }
        #endregion

        #region DropDownList
        public static MvcHtmlString DropDownListForExt<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> listItems,  object htmlAttributes = null)
        {
            return html.DropDownListFor(expression, listItems, htmlAttributes);
        }
        public static MvcHtmlString DropDownListFormForExt<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> listItems, 
            string labelText = null, string optionLabel = null)
        {
            return html.LabelWithControl(expression, labelText, null, () => html.DropDownListFor(expression, listItems, optionLabel, _initControlAttributes));
        }

        public static MvcHtmlString DropDownListFormForExt<TModel, TEnumProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TEnumProperty>> expression,
            string labelText = null, string optionLabel = null)
            where TEnumProperty : struct 
        {
            SelectList listItems = EnumHelper.ToSelectList<TEnumProperty>();
            return html.DropDownListFormForExt(expression, listItems, labelText, optionLabel);
        }
        
        #endregion

        #region TextArea
        public static MvcHtmlString TextAreaForExt<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            return html.TextAreaFor(expression, htmlAttributes);
        }
        public static MvcHtmlString TextAreaFormForExt<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, string labelText = null)
        {
            return html.LabelWithControl(expression, labelText, null, () => html.TextAreaFor(expression, _initControlAttributes));
        }
        #endregion

        #region Password
        public static MvcHtmlString PasswordForExt<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            return html.PasswordFor(expression, htmlAttributes);
        }

        public static MvcHtmlString PasswordFormForExt<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, string labelText = null)
        {
            return html.LabelWithControl(expression, labelText, null, () => html.PasswordFor(expression, _initControlAttributes));
        }
        #endregion

        #region CheckBox
        public static MvcHtmlString CheckBoxForEx<TModel>(this HtmlHelper<TModel> html, Expression<Func<TModel, bool>> expression, object htmlAttributes = null)
        {
            return html.CheckBoxFor(expression, htmlAttributes);
        }

        public static MvcHtmlString CheckBoxFormForEx<TModel>(this HtmlHelper<TModel> html, Expression<Func<TModel, bool>> expression, string labelText = null)
        {
            return html.LabelWithControl(expression, labelText, null, () => html.CheckBoxFor(expression, new {@class = "checkbox"}));
        }
        #endregion

        #region ActionLink
        public static MvcHtmlString ActionLinkExt<TController>(this HtmlHelper html, Expression<Func<TController, ActionResult>> action, 
            string linkText, object routeValues = null, object htmlAttributes = null)
            where TController : Controller
        {
            ActionInfo actionInfo = GetActionInfo(action);
            return html.ActionLinkExt(linkText, actionInfo.Action, actionInfo.Controller, routeValues, htmlAttributes);
        }
        public static MvcHtmlString ActionLinkExt(this HtmlHelper html, string linkText, string actionName, string controllerName = null, object routeValues = null, object htmlAttributes = null)
        {
            return html.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes);
        }
        #endregion

        #region FileUpload
        public static MvcHtmlString FileUploadEx(this HtmlHelper html, string name, string saveUrl, string removeUrl)
        {
            string htmlStr = html.FileUpload().Name(name).SaveAction(saveUrl).RemoveAction(removeUrl).AutoUpload(true).ToHtmlString();
            return new MvcHtmlString(htmlStr);
        }
        #endregion

        private class ActionInfo
        {
            public string Controller { get; set; }
            public string Action { get; set; }
        }

        private static ActionInfo GetActionInfo<TController>(Expression<Func<TController, ActionResult>> action)
        {
            string controllerName = typeof(TController).Name;
            controllerName = controllerName.Substring(0, controllerName.Length - 10/*Controller*/);
            return new ActionInfo()
                   {
                       Controller = controllerName,
                       Action = action.GetMethodOrPropertyName()
                   };
        }

        private static MvcHtmlString LabelWithControl<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, 
            string labelText, object labelHtmlAttributes, Func<MvcHtmlString> funcControl)
        {
            IDictionary<string, object> labelAttrs = new Dictionary<string, object>()
                                                     {
                                                         {"class", "control-label col-sm-3 col-md-2"}
                                                     };
            if (labelHtmlAttributes != null)
            {
                foreach (var attr in labelHtmlAttributes.ObjectPropertiesToDictionary())
                {
                    if (!attr.Key.Equals("class", StringComparison.InvariantCultureIgnoreCase))
                        labelAttrs["class"] += " " + attr.Value;
                    else
                        labelAttrs.Add(attr);
                }
                
            }

            var formGroupDiv = new TagBuilder("div");
            formGroupDiv.AddCssClass("form-group");
            formGroupDiv.InnerHtml = html.LabelFor(expression, labelText, labelAttrs).ToHtmlString();

            var controlDiv = new TagBuilder("div");
            controlDiv.AddCssClass("col-sm-9 col-md-10");
            controlDiv.InnerHtml = funcControl().ToHtmlString();

            formGroupDiv.InnerHtml += controlDiv.ToString();

            return new MvcHtmlString(formGroupDiv.ToString());
        }

    }

}
