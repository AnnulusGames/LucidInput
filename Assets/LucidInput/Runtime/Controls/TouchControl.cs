using UnityEngine;

namespace AnnulusGames.LucidTools.InputSystem
{
    internal sealed class TouchControl : InputControl
    {
        public TouchControl(int fingerId)
        {
            this.fingerId = fingerId;
            position = InputControlUtil.CreateTouchPositionControl(fingerId);
            startPosition = new Vector2Control(() => _startPosition);
            deltaPosition = InputControlUtil.CreateTouchDeltaPositionControl(fingerId);

            radius = InputControlUtil.CreateTouchRadiusControl(fingerId);
            press = InputControlUtil.CreateTouchButtonControl(fingerId);
            phase = InputControlUtil.CreateTouchPhaseControl(fingerId);
            tapCount = InputControlUtil.CreateTouchTapCountControl(fingerId);
            pressure = InputControlUtil.CreateTouchPressureControl(fingerId);
            deltaTime = InputControlUtil.CreateTouchDeltaTimeControl(fingerId);
        }

        public int fingerId { get; private set; }

        public Vector2Control position { get; private set; }
        public Vector2Control startPosition { get; private set; }
        public Vector2Control deltaPosition { get; private set; }

        public Vector2Control radius { get; private set; }
        public ButtonControl press { get; private set; }
        public TouchPhaseControl phase { get; private set; }
        public IntControl tapCount { get; private set; }
        public AxisControl pressure { get; private set; }
        public FloatControl deltaTime { get; private set; }

        private Vector2 _startPosition;

        public override void Reset()
        {
            position.Reset();
            startPosition.Reset();
            deltaPosition.Reset();
            radius.Reset();
            press.Reset();
            phase.Reset();
            tapCount.Reset();
            pressure.Reset();
            deltaTime.Reset();
        }

        public override void Update()
        {
            position.Update();
            deltaPosition.Update();
            radius.Update();
            press.Update();
            phase.Update();
            tapCount.Update();
            pressure.Update();
            deltaTime.Update();

            if (phase.GetValue() == TouchPhase.Began)
            {
                _startPosition = position.GetValue();
            }
            startPosition.Update();
        }
    }
}
