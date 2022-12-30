using System;

namespace AnnulusGames.LucidTools.InputSystem
{
    internal class IntControl : InputControl
    {
        public IntControl(Func<int> getValue)
        {
            this.getValue = getValue;
        }

        private Func<int> getValue;

        public virtual int GetValue()
        {
            return getValue();
        }

        public override void Reset() { }
        public override void Update() { }
    }

}