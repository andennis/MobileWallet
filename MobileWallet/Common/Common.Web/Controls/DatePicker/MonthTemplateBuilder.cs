
namespace Common.Web.Controls.DatePicker
{
    public class MonthTemplateBuilder
    {
        public MonthTemplateBuilder(MonthTemplate monthTemplate)
        {
            MonthTemplate = monthTemplate;
        }

        public MonthTemplateBuilder ContentId(string id)
        {
            MonthTemplate.ContentId = id;
            return this;
        }
        public MonthTemplateBuilder EmptyId(string id)
        {
            MonthTemplate.EmptyId = id;
            return this;
        }
        public MonthTemplateBuilder Content(string content)
        {
            MonthTemplate.Content = content;
            return this;
        }
        public MonthTemplateBuilder Empty(string empty)
        {
            MonthTemplate.Empty = empty;
            return this;
        }
        protected MonthTemplate MonthTemplate { get; private set; }
    }

}
