using Common.Web.Controls;

namespace Common.Web
{
    public class EffectsBuilder
    {
        protected Effects Effects { get; set; }
        //protected IList<string> Container { get; }

        public EffectsBuilder(Effects effects)
        {
            Effects = effects;
        }

        public EffectsBuilder Fade(FadeDirection direction)
        {
            return this;
        }
        public EffectsBuilder Zoom(ZoomDirection direction)
        {
            return this;
        }
        public EffectsBuilder SlideIn(SlideDirection direction)
        {
            return this;
        }
        public EffectsBuilder Expand()
        {
            return this;
        }
        public EffectsBuilder Expand(ExpandDirection direction)
        {
            return this;
        }
        public EffectsBuilder Duration(int value)
        {
            return this;
        }
        public EffectsBuilder Reverse(bool value)
        {
            return this;
        }
        public EffectsBuilder Duration(AnimationDuration value)
        {
            return this;
        }

    }
}
