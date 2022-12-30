using System.Collections.Generic;
using UnityEngine;

namespace AnnulusGames.LucidTools.InputSystem
{
    internal class Touchscreen : InputDevice
    {
        public Touchscreen()
        {
            touches = new Touch[10];
            for (int i = 0; i < touches.Length; i++)
            {
                touches[i] = new Touch(new TouchControl(i));
            }
            touchCount = InputControlUtil.CreateTouchCountControl();

            anyButton = new ButtonControl(
                () =>
                {
                    if (!isConnected) return false;
                    foreach (Touch touch in touches)
                    {
                        if (touch.wasPressedThisFrame) return true;
                    }
                    return false;
                },
                () =>
                {
                    if (!isConnected) return false;
                    foreach (Touch touch in touches)
                    {
                        if (touch.isPressed) return true;
                    }
                    return false;
                },
                () =>
                {
                    if (!isConnected) return false;
                    foreach (Touch touch in touches)
                    {
                        if (touch.wasReleasedThisFrame) return true;
                    }
                    return false;
                }
            );
        }

        public IReadOnlyList<Touch> Touches { get { return touches; } }
        public IntControl touchCount { get; private set; }
        public ButtonControl anyButton { get; private set; }

        public Vector2 multiTouchPosition { get; private set; }
        public Vector2 multiTouchDelta { get; private set; }

        private Vector2 prevMultiTouchPosition;
        private Touch[] touches;

        public TouchControl this[int fingerId]
        {
            get
            {
                return touches[fingerId].touchControl;
            }
        }

        public override bool isConnected
        {
            get
            {
                switch (LucidInput.activeInputHandling)
                {
                    case InputHandlingMode.InputManager: return Input.touchSupported;
#if USE_INPUTSYSTEM
                    case InputHandlingMode.InputSystem: return UnityEngine.InputSystem.Touchscreen.current != null;
#endif
                }
                return false;
            }
        }

        public override void Reset()
        {
            foreach (Touch touch in touches)
            {
                touch.touchControl.Reset();
            }
            touchCount.Reset();
            anyButton.Reset();

            multiTouchPosition = Vector2.zero;
            multiTouchDelta = Vector2.zero;
        }

        public override void Update()
        {
            foreach (Touch touch in touches)
            {
                touch.touchControl.Update();
            }
            touchCount.Update();
            anyButton.Update();

            Vector2 position = Vector2.zero;
            int count = touchCount.GetValue();

            for (int i = 0; i < count; i++)
            {
                position += touches[i].position;
            }

            multiTouchPosition = count == 0 ? prevMultiTouchPosition : position / count;
            multiTouchDelta = multiTouchPosition - prevMultiTouchPosition;
            prevMultiTouchPosition = multiTouchPosition;
        }
    }

}