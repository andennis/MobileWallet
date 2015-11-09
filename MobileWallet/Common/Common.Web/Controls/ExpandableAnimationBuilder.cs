using System;
using Common.Web.Controls.PanelBar;

namespace Common.Web.Controls
{
    public class ExpandableAnimationBuilder
    {
        private readonly EffectsBuilder _effectsBuilderExpand;
        private readonly EffectsBuilder _effectsBuilderCollapse;

        public ExpandableAnimationBuilder(ExpandableAnimation animation)
        {
            Animation = animation;
            _effectsBuilderExpand = new EffectsBuilder(Animation.Expand);
            _effectsBuilderCollapse = new EffectsBuilder(Animation.Collapse);
        }
        public void Enable(bool enable)
        {
            Animation.Enabled = enable;
        }
        public ExpandableAnimationBuilder Expand(Action<EffectsBuilder> effectsAction)
        {
            effectsAction(_effectsBuilderExpand);
            return this;
        }
        public ExpandableAnimationBuilder Collapse(Action<EffectsBuilder> effectsAction)
        {
            effectsAction(_effectsBuilderCollapse);
            return this;
        }

        protected ExpandableAnimation Animation { get; private set; }

    }
}
