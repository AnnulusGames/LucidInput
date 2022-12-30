using UnityEngine;

namespace AnnulusGames.LucidTools.InputSystem
{
    internal class Mouse : InputDevice
    {
        public Mouse()
        {
            position = InputControlUtil.CreateMousePositionControl();
            delta = new Vector2Control(() => deltaPosition);
            scrollDelta = InputControlUtil.CreateMouseScrollControl();

            buttons = new ButtonControl[5];
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = InputControlUtil.CreateMouseButtonControl(i);
            }

            anyButton = new ButtonControl(
                () =>
                {
                    if (!isConnected) return false;
                    foreach (ButtonControl button in buttons)
                    {
                        if (button.GetButtonDown()) return true;
                    }
                    return false;
                },
                () =>
                {
                    if (!isConnected) return false;
                    foreach (ButtonControl button in buttons)
                    {
                        if (button.GetButton()) return true;
                    }
                    return false;
                },
                () =>
                {
                    if (!isConnected) return false;
                    foreach (ButtonControl button in buttons)
                    {
                        if (button.GetButtonUp()) return true;
                    }
                    return false;
                }
            );

            Reset();
        }

        public Vector2Control position { get; private set; }
        public Vector2Control delta { get; private set; }
        public Vector2Control scrollDelta { get; private set; }

        public ButtonControl leftButton { get { return buttons[0]; } }
        public ButtonControl rightButton { get { return buttons[1]; } }
        public ButtonControl middleButton { get { return buttons[2]; } }
        public ButtonControl backButton { get { return buttons[3]; } }
        public ButtonControl forwardButton { get { return buttons[4]; } }

        public ButtonControl anyButton { get; private set; }

        public Vector2 clickStartPosition { get; private set; }

        private ButtonControl[] buttons;
        private Vector2 deltaPosition;
        private Vector2 prevPosition;

        public ButtonControl GetMouseButtonControl(int button)
        {
            return buttons[button];
        }

        public override bool isConnected
        {
            get
            {
                switch (LucidInput.activeInputHandling)
                {
                    case InputHandlingMode.InputManager: return true;
#if USE_INPUTSYSTEM
                    case InputHandlingMode.InputSystem: return UnityEngine.InputSystem.Mouse.current != null;
#endif
                }
                return false;
            }
        }

        public override void Reset()
        {
            position.Reset();
            delta.Reset();
            scrollDelta.Reset();
            foreach (ButtonControl button in buttons) button.Reset();
            anyButton.Reset();
        }

        public override void Update()
        {
            position.Update();
            delta.Update();
            scrollDelta.Update();
            foreach (ButtonControl button in buttons) button.Update();
            anyButton.Update();

            if (anyButton.GetButtonDown()) clickStartPosition = position.GetValue();

            deltaPosition = position.GetValue() - prevPosition;
            prevPosition = position.GetValue();
        }

    }

}