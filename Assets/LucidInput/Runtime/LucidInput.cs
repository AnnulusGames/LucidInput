using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if USE_INPUTSYSTEM
using UnityEngine.InputSystem.EnhancedTouch;
#endif

namespace AnnulusGames.LucidTools.InputSystem
{
    public static class LucidInput
    {
        public static InputHandlingMode activeInputHandling
        {
            get
            {
                return _activeinputHandling;
            }
            set
            {
                _activeinputHandling = value;
#if USE_INPUTSYSTEM
                if (value == InputHandlingMode.InputManager) EnhancedTouchSupport.Disable();
                else EnhancedTouchSupport.Enable();
#endif
            }
        }

#if USE_INPUTSYSTEM
        private static InputHandlingMode _activeinputHandling = InputHandlingMode.InputSystem;
#else
        private static InputHandlingMode _activeinputHandling = InputHandlingMode.InputManager;
#endif

        public static bool simulateMouseWithTouches
        {
            get
            {
                return _simulateMouseWithTouches;
            }
            set
            {
                _simulateMouseWithTouches = value;
                Input.simulateMouseWithTouches = value;
            }
        }

        private static bool _simulateMouseWithTouches;

        public static bool logEnabled = true;

        public static float tapTime = 0.2f;
        public static float tapInterval = 0.2f;

        public static float flickTime = 0.25f;
        public static float flickMinDistance = 80;

        public static float simultaneousPressInterval = 0.1f;

        internal static Keyboard keyboard;
        internal static Mouse mouse;
        internal static Gamepad gamepad;
        internal static Touchscreen touchscreen;

        internal static Dictionary<Axis, List<AxisControl>> axes;

        private static List<ButtonControl> buttonControlCache = new List<ButtonControl>();

        internal static void Initialize()
        {
            keyboard = new Keyboard();
            mouse = new Mouse();
            gamepad = new Gamepad();
            touchscreen = new Touchscreen();

            axes = new Dictionary<Axis, List<AxisControl>>();
            {
                List<AxisControl> axisControls = new List<AxisControl>();
                axisControls.Add(new AxisCompositeControl(keyboard[Key.D], keyboard[Key.A]));
                axisControls.Add(new AxisCompositeControl(keyboard[Key.RightArrow], keyboard[Key.LeftArrow]));
#if USE_INPUTSYSTEM
                axisControls.Add(gamepad.leftStick.x);
#endif
                axes.Add(Axis.Horizontal, axisControls);
            }
            {
                List<AxisControl> axisControls = new List<AxisControl>();
                axisControls.Add(new AxisCompositeControl(keyboard[Key.W], keyboard[Key.S]));
                axisControls.Add(new AxisCompositeControl(keyboard[Key.UpArrow], keyboard[Key.DownArrow]));
#if USE_INPUTSYSTEM
                axisControls.Add(gamepad.leftStick.y);
#endif
                axes.Add(Axis.Vertical, axisControls);
            }
            {
                List<AxisControl> axisControls = new List<AxisControl>();
                axisControls.Add(new AxisControl(() => mouse.delta.GetValue().x * 0.01f));
                axes.Add(Axis.MouseX, axisControls);
            }
            {
                List<AxisControl> axisControls = new List<AxisControl>();
                axisControls.Add(new AxisControl(() => mouse.delta.GetValue().y * 0.01f));
                axes.Add(Axis.MouseY, axisControls);
            }
            {
                List<AxisControl> axisControls = new List<AxisControl>();
                axisControls.Add(new AxisControl(() => mouse.scrollDelta.GetValue().y * 0.1f));
                axes.Add(Axis.MouseScrollWheel, axisControls);
            }
        }

        internal static void Update()
        {
            keyboard.Update();
            mouse.Update();
            gamepad.Update();
            touchscreen.Update();

            foreach (List<AxisControl> controls in axes.Values)
            {
                foreach (AxisControl control in controls)
                {
                    control.Update();
                }
            }
        }

        public static bool anyKeyDown
        {
            get
            {
                switch (activeInputHandling)
                {
                    case InputHandlingMode.InputManager: return Input.anyKeyDown;
#if USE_INPUTSYSTEM
                    case InputHandlingMode.InputSystem:
                        if (keyboard.anyKey.GetButtonDown()) return true;
                        if (mouse.anyButton.GetButtonDown()) return true;
                        if (gamepad.anyButton.GetButtonDown()) return true;
                        if (touchscreen.anyButton.GetButtonDown()) return true;
                        break;
#endif
                }

                return false;
            }
        }

        public static bool anyKey
        {
            get
            {
                switch (activeInputHandling)
                {
                    case InputHandlingMode.InputManager: return Input.anyKey;
#if USE_INPUTSYSTEM
                    case InputHandlingMode.InputSystem:
                        if (keyboard.anyKey.GetButton()) return true;
                        if (mouse.anyButton.GetButton()) return true;
                        if (gamepad.anyButton.GetButton()) return true;
                        if (touchscreen.anyButton.GetButton()) return true;
                        break;
#endif
                }

                return false;
            }
        }

        #region Keyboard

        public static bool GetKeyDown(Key key)
        {
            return keyboard[key].GetButtonDown();
        }

        public static bool GetKeyDownAny(Key key1, Key key2)
        {
            return keyboard[key1].GetButtonDown() ||
                keyboard[key2].GetButtonDown();
        }

        public static bool GetKeyDownAny(Key key1, Key key2, Key key3)
        {
            return keyboard[key1].GetButtonDown() ||
                keyboard[key2].GetButtonDown() ||
                keyboard[key3].GetButtonDown();
        }

        public static bool GetKeyDownAny(params Key[] keys)
        {
            foreach (Key key in keys) if (keyboard[key].GetButtonDown()) return true;
            return false;
        }

        public static bool GetKeyDownAll(params Key[] keys)
        {
            buttonControlCache.Clear();
            foreach (Key key in keys)
            {
                buttonControlCache.Add(keyboard[key]);
            }
            return InputControlUtil.GetButtonDownAll(buttonControlCache.ToArray());
        }

        public static bool GetKey(Key key)
        {
            return keyboard[key].GetButton();
        }

        public static bool GetKeyAny(Key key1, Key key2)
        {
            return keyboard[key1].GetButton() ||
                keyboard[key2].GetButton();
        }

        public static bool GetKeyAny(Key key1, Key key2, Key key3)
        {
            return keyboard[key1].GetButton() ||
                keyboard[key2].GetButton() ||
                keyboard[key3].GetButton();
        }

        public static bool GetKeyAny(params Key[] keys)
        {
            foreach (Key key in keys) if (keyboard[key].GetButton()) return true;
            return false;
        }

        public static bool GetKeyAll(params Key[] keys)
        {
            foreach (Key key in keys) if (!keyboard[key].GetButton()) return false;
            return true;
        }

        public static bool GetKeyUp(Key key)
        {
            return keyboard[key].GetButtonUp();
        }

        public static bool GetKeyUpAny(params Key[] keys)
        {
            foreach (Key key in keys) if (keyboard[key].GetButtonUp()) return true;
            return false;
        }
        public static bool GetKeyUpAny(Key key1, Key key2)
        {
            return keyboard[key1].GetButtonUp() ||
                keyboard[key2].GetButtonUp();
        }

        public static bool GetKeyUpAny(Key key1, Key key2, Key key3)
        {
            return keyboard[key1].GetButtonUp() ||
                keyboard[key2].GetButtonUp() ||
                keyboard[key3].GetButtonUp();
        }

        public static bool GetKeyUpAll(params Key[] keys)
        {
            buttonControlCache.Clear();
            foreach (Key key in keys)
            {
                buttonControlCache.Add(keyboard[key]);
            }
            return InputControlUtil.GetButtonUpAll(buttonControlCache.ToArray());
        }

        public static bool GetKeyHold(Key key, float time)
        {
            return keyboard[key].GetButtonHold(time);
        }

        public static bool GetKeyHoldEnded(Key key, float time)
        {
            return keyboard[key].GetButtonHoldEnded(time);
        }

        public static float GetKeyHoldTime(Key key)
        {
            return keyboard[key].timePressed;
        }

        public static bool GetKeyTap(Key key)
        {
            return GetKeyMultiTap(key, 1);
        }

        public static bool GetKeyDoubleTap(Key key)
        {
            return GetKeyMultiTap(key, 2);
        }

        public static bool GetKeyMultiTap(Key key, int count)
        {
            return keyboard[key].GetButtonTap(count);
        }

        public static int GetKeyTapCount(Key key)
        {
            return keyboard[key].tapCount;
        }

        #endregion

        #region Mouse

        public static Vector2 mousePosition
        {
            get
            {
                if (simulateMouseWithTouches && touchCount > 0)
                {
                    return multiTouchPosition;
                }
                else
                {
                    return mouse.position.GetValue();
                }
            }
        }

        public static Vector2 mousePositionDelta
        {
            get
            {
                if (simulateMouseWithTouches && touchCount > 0)
                {
                    return multiTouchDelta;
                }
                else
                {
                    return mouse.delta.GetValue();
                }
            }
        }

        public static Vector2 mouseScrollDelta
        {
            get
            {
                return mouse.scrollDelta.GetValue();
            }
        }

        public static bool GetMouseButtonDown(int button)
        {
            if (mouse.GetMouseButtonControl(button).GetButtonDown()) return true;
            else if (simulateMouseWithTouches && touchCount > button) return GetTouchButtonDown(button);
            else return false;
        }

        public static bool GetMouseButton(int button)
        {
            if (mouse.GetMouseButtonControl(button).GetButton()) return true;
            else if (simulateMouseWithTouches && touchCount > button) return GetTouchButton(button);
            else return false;
        }

        public static bool GetMouseButtonUp(int button)
        {
            if (mouse.GetMouseButtonControl(button).GetButtonUp()) return true;
            else if (simulateMouseWithTouches && touchCount > button) return GetTouchButtonUp(button);
            else return false;
        }

        public static bool GetMouseButtonHold(int button, float time)
        {
            if (mouse.GetMouseButtonControl(button).GetButtonHold(time)) return true;
            else if (simulateMouseWithTouches && touchCount > button) return GetTouchButtonHold(button, time);
            else return false;
        }

        public static bool GetMouseButtonHoldEnded(int button, float time)
        {
            if (mouse.GetMouseButtonControl(button).GetButtonHoldEnded(time)) return true;
            else if (simulateMouseWithTouches && touchCount > button) return GetTouchButtonHoldEnded(button, time);
            else return false;
        }

        public static float GetMouseButtonHoldTime(int button)
        {
            if (mouse.GetMouseButtonControl(button).timePressed > 0f) return mouse.GetMouseButtonControl(button).timePressed;
            else if (simulateMouseWithTouches && touchCount > button) return GetTouchButtonHoldTime(button);
            else return 0f;
        }

        public static bool GetMouseButtonTap(int button)
        {
            return GetMouseButtonMultiTap(button, 1);
        }

        public static bool GetMouseButtonDoubleTap(int button)
        {
            return GetMouseButtonMultiTap(button, 2);
        }

        public static bool GetMouseButtonMultiTap(int button, int count)
        {
            if (mouse.GetMouseButtonControl(button).GetButtonTap(count)) return true;
            else if (simulateMouseWithTouches && touchCount > button) return GetTouchButtonMultiTap(button, count);
            else return false;
        }

        public static int GetMouseButtonTapCount(int button)
        {
            if (mouse.GetMouseButtonControl(button).tapCount > 0) return mouse.GetMouseButtonControl(button).tapCount;
            else if (simulateMouseWithTouches && touchCount > button) return GetTouchButtonTapCount(button);
            else return 0;
        }

        public static bool GetMouseFlick(int button)
        {
            return GetMouseFlick(button, Direction.Up) || GetMouseFlick(button, Direction.Down) ||
                GetMouseFlick(button, Direction.Left) || GetMouseFlick(button, Direction.Right);
        }

        public static bool GetMouseFlick(int button, Direction direction)
        {
            if (mouse.GetMouseButtonControl(button).GetButtonUp() && mouse.GetMouseButtonControl(button).timePressed <= flickTime)
            {
                Vector2 delta = mousePosition - mouse.clickStartPosition;

                switch (direction)
                {
                    case Direction.Up: return delta.y >= flickMinDistance;
                    case Direction.Down: return delta.y <= -flickMinDistance;
                    case Direction.Left: return delta.x <= -flickMinDistance;
                    case Direction.Right: return delta.x >= flickMinDistance;
                }
            }

            if (simulateMouseWithTouches && touchCount > button)
            {
                return GetTouchFlick(button, direction);
            }

            return false;
        }

        #endregion

        #region Axis

        public static float GetAxis(Axis axis)
        {
            float result = 0f;
            foreach (var pair in axes)
            {
                if (pair.Key != axis) continue;

                foreach (var axisInput in pair.Value)
                {
                    if (Math.Abs(result) < Math.Abs(axisInput.GetValue()))
                    {
                        result = axisInput.GetValue();
                    }
                }
            }

            return result;
        }

        public static float GetAxisRaw(Axis axis)
        {
            float result = 0f;
            foreach (var pair in axes)
            {
                if (pair.Key != axis) continue;

                foreach (var axisInput in pair.Value)
                {
                    if (Math.Abs(result) < Math.Abs(axisInput.GetValue()))
                    {
                        result = axisInput.GetValueRaw();
                    }
                }
            }
            return result;
        }

        #endregion

        #region Touch

        public static int touchCount
        {
            get
            {
                return touchscreen.touchCount.GetValue();
            }
        }

        public static Vector2 multiTouchPosition
        {
            get
            {
                return touchscreen.multiTouchPosition;
            }
        }

        public static Vector2 multiTouchDelta
        {
            get
            {
                return touchscreen.multiTouchDelta;
            }
        }


        public static Touch GetTouch(int fingerId)
        {
            if (touchCount > fingerId)
            {
                return touchscreen.Touches[fingerId];
            }
            else
            {
                return null;
            }
        }

        public static bool GetTouchButtonDown(int fingerId)
        {
            return GetTouch(fingerId).wasPressedThisFrame;
        }

        public static bool GetTouchButton(int fingerId)
        {
            return GetTouch(fingerId).isPressed;
        }

        public static bool GetTouchButtonUp(int fingerId)
        {
            return GetTouch(fingerId).wasReleasedThisFrame;
        }

        public static bool GetTouchButtonHold(int fingerId, float time)
        {
            return GetTouch(fingerId).touchControl.press.GetButtonHold(time);
        }

        public static bool GetTouchButtonHoldEnded(int fingerId, float time)
        {
            return GetTouch(fingerId).touchControl.press.GetButtonHoldEnded(time);
        }

        public static float GetTouchButtonHoldTime(int fingerId)
        {
            return GetTouch(fingerId).touchControl.press.timePressed;
        }

        public static bool GetTouchButtonTap(int fingerId)
        {
            return GetTouchButtonMultiTap(fingerId, 1);
        }

        public static bool GetTouchButtonDoubleTap(int button)
        {
            return GetTouchButtonMultiTap(button, 2);
        }

        public static bool GetTouchButtonMultiTap(int fingerId, int count)
        {
            return GetTouch(fingerId).touchControl.press.GetButtonTap(count);
        }

        public static int GetTouchButtonTapCount(int fingerId)
        {
            return GetTouch(fingerId).touchControl.press.tapCount;
        }

        public static bool GetTouchFlick(int fingerId, Direction direction)
        {
            if (touchCount <= fingerId) return false;

            if (GetTouch(fingerId).touchControl.press.GetButtonUp() && GetTouch(fingerId).touchControl.press.timePressed <= flickTime)
            {
                Touch touch = GetTouch(fingerId);
                Vector3 delta = touch.position - touch.startPosition;

                switch (direction)
                {
                    case Direction.Up: return delta.y >= flickMinDistance;
                    case Direction.Down: return delta.y <= -flickMinDistance;
                    case Direction.Left: return delta.x <= -flickMinDistance;
                    case Direction.Right: return delta.x >= flickMinDistance;
                }
            }

            return false;
        }

        public static Vector2 GetTouchPosition(int fingerId)
        {
            return GetTouch(fingerId).position;
        }

        public static float pinchDelta
        {
            get
            {
                if (touchCount < 2) return 0;

                Touch touch0 = GetTouch(0);
                Touch touch1 = GetTouch(1);

                Vector2 pos0 = touch0.position;
                Vector2 pos1 = touch1.position;

                Vector2 delta0 = touch0.deltaPosition;
                Vector2 delta1 = touch1.deltaPosition;

                Vector2 prevPos0 = pos0 - delta0;
                Vector2 prevPos1 = pos1 - delta1;

                return Vector3.Distance(pos0, pos1) - Vector3.Distance(prevPos0, prevPos1);
            }
        }

        #endregion

        #region Gamepad

        public static bool anyGamepadButtonDown
        {
            get
            {
                DebugUtil.LogWarningIfGamepadIsNotSupported();
                return gamepad.anyButton.GetButtonDown();
            }
        }

        public static bool anyGamepadButton
        {
            get
            {
                DebugUtil.LogWarningIfGamepadIsNotSupported();
                return gamepad.anyButton.GetButton();
            }
        }

        public static bool GetGamepadButtonDown(GamepadButton button)
        {
            DebugUtil.LogWarningIfGamepadIsNotSupported();
            return gamepad[button].GetButtonDown();
        }

        public static bool GetGamepadButtonDownAny(GamepadButton button1, GamepadButton button2)
        {
            DebugUtil.LogWarningIfGamepadIsNotSupported();
            return gamepad[button1].GetButtonDown() ||
                gamepad[button2].GetButtonDown();
        }

        public static bool GetGamepadButtonDownAny(GamepadButton button1, GamepadButton button2, GamepadButton button3)
        {
            DebugUtil.LogWarningIfGamepadIsNotSupported();
            return gamepad[button1].GetButtonDown() ||
                gamepad[button2].GetButtonDown() ||
                gamepad[button3].GetButtonDown();
        }

        public static bool GetGamepadButtonDownAny(params GamepadButton[] buttons)
        {
            DebugUtil.LogWarningIfGamepadIsNotSupported();
            foreach (GamepadButton button in buttons) if (gamepad[button].GetButtonDown()) return true;
            return false;
        }

        public static bool GetGamepadButton(GamepadButton button)
        {
            DebugUtil.LogWarningIfGamepadIsNotSupported();
            return gamepad[button].GetButton();
        }

        public static bool GetGamepadButtonAny(GamepadButton button1, GamepadButton button2)
        {
            DebugUtil.LogWarningIfGamepadIsNotSupported();
            return gamepad[button1].GetButton() ||
                gamepad[button2].GetButton();
        }

        public static bool GetGamepadButtonAny(GamepadButton button1, GamepadButton button2, GamepadButton button3)
        {
            DebugUtil.LogWarningIfGamepadIsNotSupported();
            return gamepad[button1].GetButton() ||
                gamepad[button2].GetButton() ||
                gamepad[button3].GetButton();
        }

        public static bool GetGamepadButtonAny(params GamepadButton[] buttons)
        {
            DebugUtil.LogWarningIfGamepadIsNotSupported();
            foreach (GamepadButton button in buttons) if (gamepad[button].GetButton()) return true;
            return false;
        }

        public static bool GetGamepadButtonUp(GamepadButton button)
        {
            DebugUtil.LogWarningIfGamepadIsNotSupported();
            return gamepad[button].GetButtonUp();
        }

        public static bool GetGamepadButtonUpAny(GamepadButton button1, GamepadButton button2)
        {
            DebugUtil.LogWarningIfGamepadIsNotSupported();
            return gamepad[button1].GetButtonUp() ||
                gamepad[button2].GetButtonUp();
        }

        public static bool GetGamepadButtonUpAny(GamepadButton button1, GamepadButton button2, GamepadButton button3)
        {
            DebugUtil.LogWarningIfGamepadIsNotSupported();
            return gamepad[button1].GetButtonUp() ||
                gamepad[button2].GetButtonUp() ||
                gamepad[button3].GetButtonUp();
        }

        public static bool GetGamepadButtonUpAny(params GamepadButton[] buttons)
        {
            DebugUtil.LogWarningIfGamepadIsNotSupported();
            foreach (GamepadButton button in buttons) if (gamepad[button].GetButtonUp()) return true;
            return false;
        }

        public static bool GetGamepadButtonHold(GamepadButton button, float time)
        {
            DebugUtil.LogWarningIfGamepadIsNotSupported();
            return gamepad[button].GetButtonHold(time);
        }

        public static bool GetGamepadButtonHoldEnded(GamepadButton button, float time)
        {
            DebugUtil.LogWarningIfGamepadIsNotSupported();
            return gamepad[button].GetButtonHoldEnded(time);
        }

        public static float GetGamepadButtonHoldTime(GamepadButton button)
        {
            DebugUtil.LogWarningIfGamepadIsNotSupported();
            return gamepad[button].timePressed;
        }

        public static bool GetGamepadButtonTap(GamepadButton button)
        {
            DebugUtil.LogWarningIfGamepadIsNotSupported();
            return GetGamepadButtonMultiTap(button, 1);
        }

        public static bool GetGamepadButtonDoubleTap(GamepadButton button)
        {
            DebugUtil.LogWarningIfGamepadIsNotSupported();
            return GetGamepadButtonMultiTap(button, 2);
        }

        public static bool GetGamepadButtonMultiTap(GamepadButton button, int count)
        {
            DebugUtil.LogWarningIfGamepadIsNotSupported();
            return gamepad[button].GetButtonTap(count);
        }

        public static int GetGamepadButtonTapCount(GamepadButton button)
        {
            DebugUtil.LogWarningIfGamepadIsNotSupported();
            return gamepad[button].tapCount;
        }

        public static Vector2 GetGamepadStick(LR stick)
        {
            DebugUtil.LogWarningIfGamepadIsNotSupported();
            return gamepad.GetStick(stick).GetValue();
        }

        public static float GetGamepadTrigger(LR trigger)
        {
            DebugUtil.LogWarningIfGamepadIsNotSupported();
            return gamepad.GetTrigger(trigger).GetValue();
        }

        #endregion

    }

}