using System;

namespace AnnulusGames.LucidTools.InputSystem
{
    internal class FloatControl : InputControl
    {
        public FloatControl(Func<float> getValue)
        {
            this.getValue = getValue;
        }

        private Func<float> getValue;

        public virtual float GetValue()
        {
            return getValue();
        }

        public override void Reset() { }
        public override void Update() { }
    }

}