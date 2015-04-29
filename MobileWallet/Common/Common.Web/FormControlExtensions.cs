﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Common.Extensions;
using Common.Web.Controls.DatePicker;

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
        public static MvcForm BeginFormExt<TController>(this HtmlHelper html, Expression<Action<TController>> action, object htmlAttributes = null) where TController : Controller
        {
            ActionInfo actionInfo = GetActionInfo(action);
            return html.BeginFormExt(actionInfo.Action, actionInfo.Controller, htmlAttributes);
        }
        public static MvcForm BeginFormExt(this HtmlHelper html, string actionName = null, string controllerName = null, object htmlAttributes = null)
        {
            IDictionary<string, object> attributes = _initFormAttributes.MergeHtmlAttributes(htmlAttributes)
                .MergeHtmlAttributes((object)html.ViewBag.HtmlFormAttributes);
            return html.BeginForm(actionName, controllerName, FormMethod.Post, attributes);
        }

        public static MvcForm BeginFormExt(this AjaxHelper ajaxHelper, string actionName = null, string controllerName = null, object routeValues = null,
            string updateTargetId = null, string onSuccess = null, object htmlAttributes = null)
        {
            IDictionary<string, object> attributes = _initFormAttributes.MergeHtmlAttributes(htmlAttributes)
                .MergeHtmlAttributes((object)ajaxHelper.ViewBag.HtmlFormAttributes);
            var ajaxOptions = new AjaxOptions() {UpdateTargetId = updateTargetId, OnSuccess = onSuccess};
            return ajaxHelper.BeginForm(actionName, controllerName, new RouteValueDictionary(routeValues), ajaxOptions, attributes);
        }
        #endregion

        #region FormAction
        public static MvcHtmlString FormActionSubmit<TModel>(this HtmlHelper<TModel> html, string name, string caption, string actionUrl = null)
        {
            var tb = new TagBuilder("input");
            tb.Attributes.Add("id", name);
            tb.Attributes.Add("type", "submit");
            tb.Attributes.Add("value", caption);
            tb.AddCssClass("btn btn-default");
            if (actionUrl != null)
                tb.Attributes.Add("data-form-action", actionUrl);

            return new MvcHtmlString(tb.ToString());
        }
        public static MvcHtmlString FormActionButton<TModel>(this HtmlHelper<TModel> html, string name, string caption, string actionUrl = null)
        {
            var tb = new TagBuilder("input");
            tb.Attributes.Add("id", name);
            tb.Attributes.Add("type", "button");
            tb.Attributes.Add("value", caption);
            tb.AddCssClass("btn btn-default");
            if (!string.IsNullOrEmpty(actionUrl))
                tb.Attributes.Add("data-form-action", actionUrl);

            return new MvcHtmlString(tb.ToString());
        }
        #endregion

        #region ActionLink
        public static MvcHtmlString ActionLinkExt<TController>(this HtmlHelper html, Expression<Action<TController>> action,
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

        #region ActionLinkAjax
        public static MvcHtmlString ActionLinkAjaxExt<TController>(this HtmlHelper html, Expression<Action<TController>> action, string linkText, string confirmMessage, 
            object routeValues = null, object htmlAttributes = null, AjaxActionOptions ajaxOptions = null)
            where TController : Controller
        {
            ActionInfo actionInfo = GetActionInfo(action);
            return html.ActionLinkAjaxExt(linkText, confirmMessage, actionInfo.Action, actionInfo.Controller, routeValues, htmlAttributes, ajaxOptions);
        }

        public static MvcHtmlString ActionLinkAjaxExt(this HtmlHelper html, string linkText, string confirmMessage, 
            string actionName, string controllerName = null, object routeValues = null, object htmlAttributes = null, AjaxActionOptions ajaxOptions = null)
        {
            var tb = new TagBuilder("a");

            if (htmlAttributes != null)
            {
                foreach (var attr in HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes))
                    tb.Attributes.Add(attr.Key, Convert.ToString(attr.Value));
            }

            tb.Attributes.Add("href", "javascript:void(0)");

            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            string url = urlHelper.Action(actionName, controllerName, routeValues);
            tb.Attributes.Add("data-ajax-action", url);

            if (string.IsNullOrEmpty(confirmMessage))
                confirmMessage = string.Format("Are you sure you want to {0}?", linkText.ToLower());
            tb.Attributes.Add("confirmMessage", confirmMessage);

            if (ajaxOptions != null)
            {
                if (!string.IsNullOrEmpty(ajaxOptions.OnSuccess))
                    tb.Attributes.Add("data-on-success", ajaxOptions.OnSuccess);
                if (!string.IsNullOrEmpty(ajaxOptions.OnFail))
                    tb.Attributes.Add("data-on-fail", ajaxOptions.OnFail);
            }

            tb.SetInnerText(linkText);

            return new MvcHtmlString(tb.ToString());
        }
        #endregion

        #region Partial

        public static MvcHtmlString PartialEx(this HtmlHelper html, string partialViewName, object model = null, ViewDataDictionary viewData = null)
        {
            return html.Partial(partialViewName, model, viewData);
        }

        #endregion

        #region TextBlock
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
            if (typeof (TProperty) == typeof (DateTime))
            {
                if (string.IsNullOrEmpty(format))
                    format = "d";
            }

            string dataFmt = (!string.IsNullOrEmpty(format) ? string.Format("{{0:{0}}}", format) : null);
            IDictionary<string,object> attributes = _initControlAttributes
                .Union(new Dictionary<string, object>()
                {
                    { "readonly", "readonly" }, 
                    { "data-format", format }
                })
                .ToDictionary(key => key.Key, val => val.Value);

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
        public static MvcHtmlString DropDownListForExt<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> listItems,
            string optionLabel = null, object htmlAttributes = null)
        {
            return html.DropDownListFor(expression, listItems, optionLabel, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString DropDownListForExt<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> listItems,  
            string optionLabel = null, IDictionary<string, object> htmlAttributes = null)
        {
            return html.DropDownListFor(expression, listItems, optionLabel, htmlAttributes);
        }
        public static MvcHtmlString DropDownListFormForExt<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> listItems, 
            string labelText = null, string optionLabel = null)
        {
            return html.LabelWithControl(expression, labelText, null, () => html.DropDownListForExt(expression, listItems, optionLabel, _initControlAttributes));
        }
        public static MvcHtmlString DropDownListExt(this HtmlHelper html, string name, IEnumerable<SelectListItem> listItems, string optionLabel = null, object htmlAttributes = null)
        {
            return html.DropDownList(name, listItems, optionLabel, htmlAttributes);
        }


        public static MvcHtmlString DropDownListFormForExt<TModel, TEnumProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TEnumProperty>> expression,
            string labelText = null, string optionLabel = null)
            where TEnumProperty : struct  
        {
            SelectList listItems = EnumHelper.ToSelectList<TEnumProperty>();
            return html.DropDownListFormForExt(expression, listItems, labelText, optionLabel);
        }
        public static MvcHtmlString DropDownListFormForExt<TModel, TEnumProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TEnumProperty?>> expression,
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

        #region FileUpload

        public static MvcHtmlString FileUploadFormForEx<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, string labelText = null, string fileExts = null)
        {
            return html.LabelWithControl(expression, labelText, null, () => html.FileUploadForEx(expression, fileExts, _initControlAttributes));
        }

        public static MvcHtmlString FileUploadForEx<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, string fileExts = null, object htmlAttributes = null)
        {
            string propName = expression.GetPropertyName();
            return html.FileUploadEx(propName, fileExts, htmlAttributes);
        }

        public static MvcHtmlString FileUploadEx(this HtmlHelper html, string name, string fileExts = null, object htmlAttributes = null)
        {
            var inputTag = new TagBuilder("input");
            inputTag.GenerateId(name);
            inputTag.Attributes.Add("name", name);
            inputTag.Attributes.Add("type", "file");
            if (!string.IsNullOrEmpty(fileExts))
                inputTag.Attributes.Add("accept", fileExts);

            inputTag.Attributes.AddHtmlAttributes(htmlAttributes);
            return new MvcHtmlString(inputTag.ToString());
        }
        #endregion

        #region DatePicker
        //TODO TProperty should be replaced by DateTime
        public static MvcHtmlString DatePickerForEx<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, DateTime? value = null,
            DateTime? minValue = null, DateTime? maxValue = null, string dateFormat = null, object htmlAttributes = null)
        {
            string propName = expression.GetPropertyName();
            return html.DatePickerEx(propName, value, minValue, maxValue, dateFormat, htmlAttributes);
        }
        public static MvcHtmlString DatePickerFormForExt<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, string labelText = null,
            DateTime? value = null, DateTime? minValue = null, DateTime? maxValue = null, string dateFormat = null, object htmlAttributes = null)
        {
            return html.LabelWithControl(expression, labelText, null, () => html.DatePickerForEx(expression, value, minValue, maxValue, dateFormat, htmlAttributes));
        }

        public static MvcHtmlString DatePickerEx(this HtmlHelper html, string name, DateTime? value = null, DateTime? minValue = null, DateTime? maxValue = null, 
            string dateFormat = null, object htmlAttributes = null)
        {
            DatePickerBuilder builder = html.Widget().DatePicker()
                .Name(name)
                .HtmlAttributes(_initControlAttributes.MergeHtmlAttributes(htmlAttributes));

            if (minValue.HasValue)
                builder.Min(minValue.Value);
            if (maxValue.HasValue)
                builder.Max(maxValue.Value);
            if (value.HasValue)
                builder.Value(value.Value);
            if (!string.IsNullOrEmpty(dateFormat))
                builder.Format(dateFormat);

            return new MvcHtmlString(builder.ToHtmlString());
        }
        #endregion

        private class ActionInfo
        {
            public string Controller { get; set; }
            public string Action { get; set; }
        }

        private static IDictionary<string, object> MergeHtmlAttributes(this IDictionary<string, object> dst, object src)
        {
            if (src != null)
                return dst.Union(HtmlHelper.AnonymousObjectToHtmlAttributes(src)).ToDictionary(key => key.Key, val => val.Value);

            return dst;
        }
        private static void AddHtmlAttributes(this IDictionary<string, string> dst, object src)
        {
            if (src == null)
                return;

            foreach (var attr in HtmlHelper.AnonymousObjectToHtmlAttributes(src))
            {
                string newVal = Convert.ToString(attr.Value);
                if (dst.ContainsKey(attr.Key))
                    dst[attr.Key] = newVal;
                else
                    dst.Add(attr.Key, newVal);
            }
        }

        private static ActionInfo GetActionInfo<TController>(Expression<Action<TController>> action)
        {
            string controllerName = typeof(TController).Name;
            controllerName = controllerName.Substring(0, controllerName.Length - 10/*Controller*/);
            return new ActionInfo()
                   {
                       Controller = controllerName,
                       Action = action.GetMethodName()
                   };
        }

        private static MvcHtmlString LabelWithControl<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, 
            string labelText, object labelHtmlAttributes, Func<MvcHtmlString> funcControl)
        {
            IDictionary<string, object> labelAttrs = new Dictionary<string, object>()
                                                     {
                                                         {"class", "control-label col-sm-4 col-md-3"}
                                                     };
            if (labelHtmlAttributes != null)
            {
                foreach (var attr in HtmlHelper.AnonymousObjectToHtmlAttributes(labelHtmlAttributes))
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
            controlDiv.AddCssClass("col-sm-8 col-md-9");
            controlDiv.InnerHtml = funcControl().ToHtmlString();

            formGroupDiv.InnerHtml += controlDiv.ToString();

            return new MvcHtmlString(formGroupDiv.ToString());
        }

    }

}
