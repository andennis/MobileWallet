using System;

namespace Common.Web.Controls.DatePicker
{
    public class PopupAnimationBuilder
    {
        private readonly PopupAnimation _animation;
        private readonly EffectsBuilder _effectsBuilderOpen;
        private readonly EffectsBuilder _effectsBuilderClose;

        public PopupAnimationBuilder(PopupAnimation animation)
        {
            _animation = animation;
            _effectsBuilderOpen = new EffectsBuilder(_animation.Open);
            _effectsBuilderClose = new EffectsBuilder(_animation.Close);
        }

        public void Enable(bool enable)
        {
            _animation.Enabled = enable;
        }

        public PopupAnimationBuilder Open(Action<EffectsBuilder> effectsAction)
        {
            effectsAction(_effectsBuilderOpen);
            return this;
        }
        public PopupAnimationBuilder Close(Action<EffectsBuilder> effectsAction)
        {
            effectsAction(_effectsBuilderClose);
            return this;
        }

        //protected PopupAnimation Animation { get { return _animation; } }

    }

}
