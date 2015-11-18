namespace Common.Web
{
    public class ActionInfo
    {
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

        public override string ToString()
        {
            if (Area == null)
                return string.Format("{0}_{1}", Controller, Action);

            return string.Format("{0}_{1}_{2}", Area, Controller, Action);
        }
    }
}
