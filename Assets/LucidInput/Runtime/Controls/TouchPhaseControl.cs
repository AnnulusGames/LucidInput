using System;

namespace AnnulusGames.LucidTools.InputSystem
{
    internal sealed class TouchPhaseControl : InputControl
    {
        public TouchPhaseControl(Func<TouchPhase> getValue)
        {
            this.getValue = getValue;
        }

        private Func<TouchPhase> getValue;

        public TouchPhase GetValue()
        {
            return getValue();
        }

        public override void Reset() { }
        public override void Update() { }
    }

}