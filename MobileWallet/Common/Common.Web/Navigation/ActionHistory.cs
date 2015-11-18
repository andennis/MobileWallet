using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Common.Web.Navigation
{
    public class ActionHistory
    {
        private const string ActionHistoryKey = "ActionHistory";
        private readonly ControllerContext _controllerContext;
        private IList<ActionHistoryItem> _actionHistoryItems;

        private IList<ActionHistoryItem> ActionHistoryItems
        {
            get
            {
                if (_actionHistoryItems != null)
                    return _actionHistoryItems;

                _actionHistoryItems = _controllerContext.HttpContext.Session[ActionHistoryKey] as IList<ActionHistoryItem>;
                if (_actionHistoryItems == null)
                {
                    _actionHistoryItems = new List<ActionHistoryItem>();
                    _controllerContext.HttpContext.Session[ActionHistoryKey] = _actionHistoryItems;
                }
                return _actionHistoryItems;
            }
            set
            {
                _actionHistoryItems = value;
                _controllerContext.HttpContext.Session[ActionHistoryKey] = value;
            }
        }

        public ActionHistory(ControllerContext controllerContext)
        {
            _controllerContext = controllerContext;
        }

        public ActionHistoryItem GoToCurrentAction()
        {
            ActionInfo ai = GetCurrentActionInfo();
            for (int i = ActionHistoryItems.Count - 1; i >= 0; i--)
            {
                if (ActionHistoryItems[i].Name != ai.ToString())
                    continue;

                for (int j = i + 1; j < ActionHistoryItems.Count; j++)
                    ActionHistoryItems.RemoveAt(i + 1);

                return ActionHistoryItems[i];
            }

            Uri uri = _controllerContext.HttpContext.Request.UrlReferrer;
            var item = new ActionHistoryItem()
                        {
                            Name = ai.ToString(),
                            PreviousUrl = uri != null ? uri.ToString() : null
                        };

            ActionHistoryItems.Add(item);
            return item;
        }

        public ActionHistoryItem CurrentAction
        {
            get
            {
                if (!ActionHistoryItems.Any())
                    return null;

                return ActionHistoryItems.Last();
            }
        }

        public void ResetHistory()
        {
            ActionHistoryItems = null;
        }

        private ActionInfo GetCurrentActionInfo()
        {
            return new ActionInfo()
            {
                Area = _controllerContext.RouteData.Values["area"] as string,
                Controller = _controllerContext.RouteData.Values["controller"] as string,
                Action = _controllerContext.RouteData.Values["action"] as string,
            };

        }

    }
}
