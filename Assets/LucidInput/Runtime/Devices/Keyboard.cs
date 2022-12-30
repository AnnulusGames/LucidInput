using System;
using System.Collections.Generic;

namespace AnnulusGames.LucidTools.InputSystem
{
    internal class Keyboard : InputDevice
    {
        public Keyboard()
        {
            keys = new Dictionary<int, ButtonControl>();

            foreach (Key key in Enum.GetValues(typeof(Key)))
            {
                keys.Add((int)key, InputControlUtil.CreateKeyButtonControl(key));
            }

            anyKey = new ButtonControl(
                () =>
                {
                    if (!isConnected) return false;
                    foreach (ButtonControl keyButton in keys.Values)
                    {
                        if (keyButton.GetButtonDown()) return true;
                    }
                    return false;
                },
                () =>
                {
                    if (!isConnected) return false;
                    foreach (ButtonControl keyButton in keys.Values)
                    {
                        if (keyButton.GetButton()) return true;
                    }
                    return false;
                },
                () =>
                {
                    if (!isConnected) return false;
                    foreach (ButtonControl keyButton in keys.Values)
                    {
                        if (keyButton.GetButtonUp()) return true;
                    }
                    return false;
                }
            );
        }

        private Dictionary<int, ButtonControl> keys;

        public ButtonControl this[Key key]
        {
            get
            {
                return keys[(int)key];
            }
        }

        public ButtonControl anyKey { get; private set; }

        public override bool isConnected
        {
            get
            {
                switch (LucidInput.activeInputHandling)
                {
                    case InputHandlingMode.InputManager: return true;
#if USE_INPUTSYSTEM
                    case InputHandlingMode.InputSystem: return UnityEngine.InputSystem.Keyboard.current != null;
#endif
                }
                return false;
            }
        }

        public override void Reset()
        {
            foreach (ButtonControl buttonControl in keys.Values)
            {
                buttonControl.Reset();
            }
            anyKey.Reset();
        }

        public override void Update()
        {
            foreach (ButtonControl buttonControl in keys.Values)
            {
                buttonControl.Update();
            }
            anyKey.Update();
        }
    }
}
