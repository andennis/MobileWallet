using System.Web.Mvc;

namespace Common.Web.Controls
{
    public class ListBase : WidgetBase
    {
        private PopupAnimation _animation;

        public ListBase(ViewContext viewContext/*, ViewDataDictionary viewData*/)
            :base(viewContext)
        {
        }
        
        public PopupAnimation Animation { get { return _animation ?? (_animation = new PopupAnimation()); } }
        //public DataSource DataSource { get; }
        public string DataTextField { get; set; }
        public int? Delay { get; set; }
        public bool? Enabled { get; set; }
        public string Filter { get; set; }
        public bool? IgnoreCase { get; set; }
        public int? Height { get; set; }
        public string HeaderTemplate { get; set; }
        public string HeaderTemplateId { get; set; }
        public int? MinLength { get; set; }
        public bool? ValuePrimitive { get; set; }
    }
}
