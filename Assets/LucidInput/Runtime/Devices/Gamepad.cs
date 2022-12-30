using System;
using System.Collections.Generic;

namespace AnnulusGames.LucidTools.InputSystem
{
    internal class Gamepad : InputDevice
    {
        public Gamepad()
        {
            buttons = new Dictionary<int, ButtonControl>();

            foreach (GamepadButton button in Enum.GetValues(typeof(GamepadButton)))
            {
                buttons.Add((int)button, InputControlUtil.CreateGamepadButtonControl(button));
            }

            leftStick = InputControlUtil.CreateGamepadStickControl(LR.Left);
            rightStick = InputControlUtil.CreateGamepadStickControl(LR.Right);
            leftTrigger = InputControlUtil.CreateGamepadTriggerControl(LR.Left);
            rightTrigger = InputControlUtil.CreateGamepadTriggerControl(LR.Right);

            anyButton = new ButtonControl(
                () =>
                {
                    if (!isConnected) return false;
                    foreach (ButtonControl button in buttons.Values)
                    {
                        if (button.GetButtonDown()) return true;
                    }
                    return false;
                },
                () =>
                {
                    if (!isConnected) return false;
                    foreach (ButtonControl button in buttons.Values)
                    {
                        if (button.GetButton()) return true;
                    }
                    return false;
                },
                () =>
                {
                    if (!isConnected) return false;
                    foreach (ButtonControl button in buttons.Values)
                    {
                        if (button.GetButtonUp()) return true;
                    }
                    return false;
                }
            );
        }

        public Vector2Control leftStick { get; private set; }
        public Vector2Control rightStick { get; private set; }
        public AxisControl leftTrigger { get; private set; }
        public AxisControl rightTrigger { get; private set; }

        public ButtonControl anyButton { get; private set; }

        private Dictionary<int, ButtonControl> buttons;

        public ButtonControl this[GamepadButton button]
        {
            get
            {
                return buttons[(int)button];
            }
        }

        public Vector2Control GetStick(LR stick)
        {
            switch (stick)
            {
                case LR.Left: return leftStick;
                case LR.Right: return rightStick;
            }
            return null;
        }

        public AxisControl GetTrigger(LR trigger)
        {
            switch (trigger)
            {
                case LR.Left: return leftTrigger;
                case LR.Right: return rightTrigger;
            }
            return null;
        }

        public override bool isConnected
        {
            get
            {
                switch (LucidInput.activeInputHandling)
                {
                    case InputHandlingMode.InputManager: return false;
#if USE_INPUTSYSTEM
                    case InputHandlingMode.InputSystem: return UnityEngine.InputSystem.Gamepad.current != null;
#endif
                }
                return false;
            }
        }

        public override void Reset()
        {
            foreach (ButtonControl buttonControl in buttons.Values)
            {
                buttonControl.Reset();
            }
            leftStick.Reset();
            rightStick.Reset();
            leftTrigger.Reset();
            rightTrigger.Reset();
            anyButton.Reset();
        }

        public override void Update()
        {
            foreach (ButtonControl buttonControl in buttons.Values)
            {
                buttonControl.Update();
            }
            leftStick.Update();
            rightStick.Update();
            leftTrigger.Update();
            rightTrigger.Update();
            anyButton.Update();
        }
    }

}