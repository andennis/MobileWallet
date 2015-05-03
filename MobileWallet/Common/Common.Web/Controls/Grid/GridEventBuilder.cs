namespace Common.Web.Controls.Grid
{
    public class GridEventBuilder
    {
        internal string DataReadingHandler { get; set; }
        internal string DataBindingHandler { get; set; }
        internal string DataBoundHandler { get; set; }

        public GridEventBuilder DataReading(string handler)
        {
            DataReadingHandler = handler;
            return this;
        }
        public GridEventBuilder DataBinding(string handler)
        {
            DataBindingHandler = handler;
            return this;
        }
        public GridEventBuilder DataBound(string handler)
        {
            DataBoundHandler = handler;
            return this;
        }
    }
}
