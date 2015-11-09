
namespace Common.Web.Controls.PanelBar
{
    public class SecurityTrimmingBuilder
    {
        public SecurityTrimmingBuilder(SecurityTrimming securityTrimmingInfo)
        {
            SecurityTrimmingInfo = securityTrimmingInfo;
        }

        public void Enabled(bool enable)
        {
            SecurityTrimmingInfo.Enabled = enable;
        }
        public void HideParent(bool hideParent)
        {
            SecurityTrimmingInfo.HideParent = hideParent;
        }

        protected SecurityTrimming SecurityTrimmingInfo { get; set; }

    }
}
