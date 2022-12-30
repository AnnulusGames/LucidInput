using System;

namespace AnnulusGames.LucidTools.InputSystem
{
    internal class AxisControl : InputControl
    {
        public AxisControl(Func<float> getValue)
        {
            this.getValue = getValue;
        }

        private Func<float> getValue;

        public virtual float GetValue()
        {
            return getValue();
        }

        public float GetValueRaw()
        {
            float value = GetValue();
            return value == 0f ? 0f : (value > 0f ? 1f : -1f);
        }

        public override void Reset() { }
        public override void Update() { }
    }
}
