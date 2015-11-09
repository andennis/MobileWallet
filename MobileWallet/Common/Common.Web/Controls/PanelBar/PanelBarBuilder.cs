using System;

namespace Common.Web.Controls.PanelBar
{
    public class PanelBarBuilder : WidgetBuilderBase<PanelBar, PanelBarBuilder>
    {
        private readonly PanelBarItemFactory _panelBarItemFactory;
        private PanelBarEventBuilder _eventBuilder;
        private ExpandableAnimationBuilder _expandableAnimationBuilder;
        private SecurityTrimmingBuilder _securityTrimmingBuilder;

        public PanelBarBuilder(PanelBar component)
            :base(component)
        {
            _panelBarItemFactory = new PanelBarItemFactory(component);
        }

        private PanelBarEventBuilder EventBuilder
        {
            get { return _eventBuilder ?? (_eventBuilder = new PanelBarEventBuilder(_component.Events)); }
        }
        private ExpandableAnimationBuilder ExpandableAnimationBuilder
        {
            get
            {
                return _expandableAnimationBuilder ?? (_expandableAnimationBuilder = new ExpandableAnimationBuilder(_component.Animation));
            }
        }
        private SecurityTrimmingBuilder SecurityTrimmingBuilder
        {
            get
            {
                return _securityTrimmingBuilder ?? (_securityTrimmingBuilder = new SecurityTrimmingBuilder(_component.SecurityTrimming));
            }
        }
        

        public PanelBarBuilder Items(Action<PanelBarItemFactory> addAction)
        {
            addAction(_panelBarItemFactory);
            return this;
        }
        public PanelBarBuilder Events(Action<PanelBarEventBuilder> clientEventsAction)
        {
            clientEventsAction(EventBuilder);
            return this;
        }
        /*
        public PanelBarBuilder BindTo(string viewDataKey, Action<PanelBarItem, SiteMapNode> siteMapAction)
        {
            return this;
        }
        public PanelBarBuilder BindTo(string viewDataKey)
        {
            return this;
        }
        public PanelBarBuilder BindTo<T>(IEnumerable<T> dataSource, Action<PanelBarItem, T> itemDataBound)
        {
            return this;
        }
        */
        /*
        public PanelBarBuilder BindTo(IEnumerable dataSource, Action<NavigationBindingFactory<PanelBarItem>> factoryAction)
        {
            return this;
        }
        */
        public PanelBarBuilder Animation(bool enable)
        {
            _component.Animation.Enabled = enable;
            return this;
        }
        
        public PanelBarBuilder Animation(Action<ExpandableAnimationBuilder> animationAction)
        {
            animationAction(ExpandableAnimationBuilder);
            return this;
        }
        /*
        public PanelBarBuilder ItemAction(Action<PanelBarItem> action)
        {
            return this;
        }
        */
        public PanelBarBuilder HighlightPath(bool value)
        {
            _component.HighlightPath = value;
            return this;
        }
        public PanelBarBuilder ExpandAll(bool value)
        {
            _component.ExpandAll = value;
            return this;
        }
        public PanelBarBuilder ExpandMode(PanelBarExpandMode value)
        {
            _component.ExpandMode = value;
            return this;
        }
        public PanelBarBuilder SelectedIndex(int index)
        {
            _component.SelectedIndex = index;
            return this;
        }
        public PanelBarBuilder SecurityTrimming(bool value)
        {
            _component.SecurityTrimming.Enabled = value;
            return this;
        }
        public PanelBarBuilder SecurityTrimming(Action<SecurityTrimmingBuilder> securityTrimmingAction)
        {
            securityTrimmingAction(SecurityTrimmingBuilder);
            return this;
        }

    }
}
