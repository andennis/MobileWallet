namespace Common.Web.Controls.DropDownList
{
    public class DropDownListItemBuilder
    {
        private readonly DropDownListItem _item;
        public DropDownListItemBuilder(DropDownListItem item)
        {
            _item = item;
        }

        public DropDownListItemBuilder Text(string value)
        {
            _item.Text = value;
            return this;            
        }
        public DropDownListItemBuilder Value(string value)
        {
            _item.Value = value;
            return this;            
        }
        public DropDownListItemBuilder Selected(bool value)
        {
            _item.Selected = value;
            return this;
        }

    }
}
