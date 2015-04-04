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
            //Effects.Container.Add(direction == FadeDirection.In ? "fadeIn" : "fadeOut");
            return this;
        }
        public EffectsBuilder Zoom(ZoomDirection direction)
        {
            //Effects.Container.Add(direction == ZoomDirection.In ? "zoom:in" : "zoom:out");
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
