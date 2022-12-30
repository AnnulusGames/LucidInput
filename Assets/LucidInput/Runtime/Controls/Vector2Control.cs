using System;
using UnityEngine;

namespace AnnulusGames.LucidTools.InputSystem
{
    internal class Vector2Control : InputControl
    {
        public Vector2Control(Func<Vector2> getValue)
        {
            this.getValue = getValue;

            x = new AxisControl(() => GetValue().x);
            y = new AxisControl(() => GetValue().y);
        }

        private Func<Vector2> getValue;

        public Vector2 GetValue()
        {
            return getValue();
        }

        public AxisControl x { get; private set; }
        public AxisControl y { get; private set; }

        public override void Reset()
        {
            x.Reset();
            y.Reset();
        }

        public override void Update()
        {
            x.Update();
            y.Update();
        }
    }
}
