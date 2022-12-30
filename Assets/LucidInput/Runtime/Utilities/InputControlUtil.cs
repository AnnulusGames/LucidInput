using System.Linq;
using UnityEngine;
using IMTouchPhase = UnityEngine.TouchPhase;
#if USE_INPUTSYSTEM
using UnityEngine.InputSystem.EnhancedTouch;
using ISTouch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using ISKeyboard = UnityEngine.InputSystem.Keyboard;
using ISGamepad = UnityEngine.InputSystem.Gamepad;
using ISMouse = UnityEngine.InputSystem.Mouse;
using ISTouchscreen = UnityEngine.InputSystem.Touchscreen;
using ISTouchPhase = UnityEngine.InputSystem.TouchPhase;
#endif

namespace AnnulusGames.LucidTools.InputSystem
{
    internal static class InputControlUtil
    {
        public static bool IsDeviceConnected(DeviceType deviceType)
        {
            bool isConnected = false;
            switch (deviceType)
            {
                case DeviceType.Keyboard: isConnected = LucidInput.keyboard.isConnected; break;
                case DeviceType.Mouse: isConnected = LucidInput.mouse.isConnected; break;
                case DeviceType.Touchscreen: isConnected = LucidInput.touchscreen.isConnected; break;
                case DeviceType.Gamepad: isConnected = LucidInput.gamepad.isConnected; break;
            }

            return isConnected;
        }

        public static bool IsTouchActive(int fingerId)
        {
            return LucidInput.touchCount > fingerId;
        }

        public static bool GetButtonDownAll(params ButtonControl[] buttons)
        {
            float t = 0f;
            bool anyButtonWasPressedThisFrame = false;
            foreach (ButtonControl buttonControl in buttons)
            {
                if (!buttonControl.wasPressedEvenOnce) return false;
                if (buttonControl.GetButtonDown()) anyButtonWasPressedThisFrame = true;

                if (buttonControl.GetButton() || buttonControl.GetButtonDown())
                {
                    if (buttonControl.timePressed > t) t = buttonControl.timePressed;
                }
                else
                {
                    return false;
                }
            }
            return anyButtonWasPressedThisFrame && t <= LucidInput.simultaneousPressInterval;
        }

        public static bool GetButtonUpAll(params ButtonControl[] buttons)
        {
            float t = 0f;
            bool anyButtonWasReleasedThisFrame = false;
            foreach (ButtonControl buttonControl in buttons)
            {
                if (!buttonControl.wasPressedEvenOnce) return false;
                if (buttonControl.GetButtonUp()) anyButtonWasReleasedThisFrame = true;

                if (!buttonControl.GetButton() || buttonControl.GetButtonUp())
                {
                    if (buttonControl.timeUnpressed > t) t = buttonControl.timeUnpressed;
                }
                else
                {
                    return false;
                }
            }
            return anyButtonWasReleasedThisFrame && t <= LucidInput.simultaneousPressInterval;
        }

        public static ButtonControl CreateKeyButtonControl(Key key)
        {
            ButtonControl buttonControl = new ButtonControl(
                () =>
                {
                    if (!IsDeviceConnected(DeviceType.Keyboard)) return false;
                    if (key.IsNone()) return false;

                    switch (LucidInput.activeInputHandling)
                    {
                        case InputHandlingMode.InputManager: return Input.GetKeyDown(key.ToKeyCode());
#if USE_INPUTSYSTEM
                        case InputHandlingMode.InputSystem: return ISKeyboard.current[key.ToISKey()].wasPressedThisFrame;
#endif
                    }
                    return false;
                },
                () =>
                {
                    if (!IsDeviceConnected(DeviceType.Keyboard)) return false;
                    if (key.IsNone()) return false;

                    switch (LucidInput.activeInputHandling)
                    {
                        case InputHandlingMode.InputManager: return Input.GetKey(key.ToKeyCode());
#if USE_INPUTSYSTEM
                        case InputHandlingMode.InputSystem: return ISKeyboard.current[key.ToISKey()].isPressed;
#endif
                    }
                    return false;
                },
                () =>
                {
                    if (!IsDeviceConnected(DeviceType.Keyboard)) return false;
                    if (key.IsNone()) return false;

                    switch (LucidInput.activeInputHandling)
                    {
                        case InputHandlingMode.InputManager: return Input.GetKeyUp(key.ToKeyCode());
#if USE_INPUTSYSTEM
                        case InputHandlingMode.InputSystem: return ISKeyboard.current[key.ToISKey()].wasReleasedThisFrame;
#endif
                    }
                    return false;
                }
            );

            return buttonControl;
        }
        public static ButtonControl CreateGamepadButtonControl(GamepadButton button)
        {
            ButtonControl buttonControl = new ButtonControl(
                () =>
                {
                    if (!IsDeviceConnected(DeviceType.Gamepad)) return false;

                    switch (LucidInput.activeInputHandling)
                    {
                        case InputHandlingMode.InputManager:
                            return false;
#if USE_INPUTSYSTEM
                        case InputHandlingMode.InputSystem:
                            return ISGamepad.current[button.ToISGamepadButton()].wasPressedThisFrame;
#endif
                    }
                    return false;
                },
                () =>
                {
                    if (!IsDeviceConnected(DeviceType.Gamepad)) return false;

                    switch (LucidInput.activeInputHandling)
                    {
                        case InputHandlingMode.InputManager:
                            return false;
#if USE_INPUTSYSTEM
                        case InputHandlingMode.InputSystem:
                            return ISGamepad.current[button.ToISGamepadButton()].isPressed;
#endif
                    }
                    return false;
                },
                () =>
                {
                    if (!IsDeviceConnected(DeviceType.Gamepad)) return false;

                    switch (LucidInput.activeInputHandling)
                    {
                        case InputHandlingMode.InputManager:
                            return false;
#if USE_INPUTSYSTEM
                        case InputHandlingMode.InputSystem:
                            return ISGamepad.current[button.ToISGamepadButton()].wasReleasedThisFrame;
#endif
                    }
                    return false;
                }
            );

            return buttonControl;
        }

        public static Vector2Control CreateMousePositionControl()
        {
            Vector2Control vector2Control = new Vector2Control(() =>
            {
                if (!IsDeviceConnected(DeviceType.Mouse)) return Vector2.zero;

                switch (LucidInput.activeInputHandling)
                {
                    case InputHandlingMode.InputManager: return Input.mousePosition;
#if USE_INPUTSYSTEM
                    case InputHandlingMode.InputSystem: return ISMouse.current.position.ReadValue();
#endif
                }
                return Vector2.zero;
            });

            return vector2Control;
        }

        public static ButtonControl CreateMouseButtonControl(int button)
        {
            ButtonControl buttonControl = new ButtonControl(
                () =>
                {
                    if (!IsDeviceConnected(DeviceType.Mouse)) return false;

                    switch (LucidInput.activeInputHandling)
                    {
                        case InputHandlingMode.InputManager: return Input.GetMouseButtonDown(button);
#if USE_INPUTSYSTEM
                        case InputHandlingMode.InputSystem:
                            switch (button)
                            {
                                case 0: return ISMouse.current.leftButton.wasPressedThisFrame;
                                case 1: return ISMouse.current.rightButton.wasPressedThisFrame;
                                case 2: return ISMouse.current.middleButton.wasPressedThisFrame;
                                case 3: return ISMouse.current.backButton.wasPressedThisFrame;
                                case 4: return ISMouse.current.forwardButton.wasPressedThisFrame;
                            }
                            return false;
#endif
                    }
                    return false;
                },
                () =>
                {
                    if (!IsDeviceConnected(DeviceType.Mouse)) return false;

                    switch (LucidInput.activeInputHandling)
                    {
                        case InputHandlingMode.InputManager: return Input.GetMouseButton(button);
#if USE_INPUTSYSTEM
                        case InputHandlingMode.InputSystem:
                            switch (button)
                            {
                                case 0: return ISMouse.current.leftButton.isPressed;
                                case 1: return ISMouse.current.rightButton.isPressed;
                                case 2: return ISMouse.current.middleButton.isPressed;
                                case 3: return ISMouse.current.backButton.isPressed;
                                case 4: return ISMouse.current.forwardButton.isPressed;
                            }
                            return false;
#endif
                    }
                    return false;
                },
                () =>
                {
                    if (!IsDeviceConnected(DeviceType.Mouse)) return false;

                    switch (LucidInput.activeInputHandling)
                    {
                        case InputHandlingMode.InputManager: return Input.GetMouseButtonUp(button);
#if USE_INPUTSYSTEM
                        case InputHandlingMode.InputSystem:
                            switch (button)
                            {
                                case 0: return ISMouse.current.leftButton.wasReleasedThisFrame;
                                case 1: return ISMouse.current.rightButton.wasReleasedThisFrame;
                                case 2: return ISMouse.current.middleButton.wasReleasedThisFrame;
                                case 3: return ISMouse.current.backButton.wasReleasedThisFrame;
                                case 4: return ISMouse.current.forwardButton.wasReleasedThisFrame;
                            }
                            return false;
#endif
                    }
                    return false;
                }
            );

            return buttonControl;
        }

        public static Vector2Control CreateMouseScrollControl()
        {
            Vector2Control vector2Control = new Vector2Control(() =>
            {
                if (!IsDeviceConnected(DeviceType.Mouse)) return Vector2.zero;

                switch (LucidInput.activeInputHandling)
                {
                    case InputHandlingMode.InputManager: return Input.mouseScrollDelta;
#if USE_INPUTSYSTEM
                    case InputHandlingMode.InputSystem: return ISMouse.current.scroll.ReadValue();
#endif
                }
                return Vector2.zero;
            });

            return vector2Control;
        }

        public static Vector2Control CreateGamepadStickControl(LR stick)
        {
            Vector2Control vector2Control = new Vector2Control(() =>
            {
                if (!IsDeviceConnected(DeviceType.Gamepad)) return Vector2.zero;

                switch (LucidInput.activeInputHandling)
                {
                    case InputHandlingMode.InputManager: return Vector2.zero;
#if USE_INPUTSYSTEM
                    case InputHandlingMode.InputSystem:
                        switch (stick)
                        {
                            case LR.Left: return ISGamepad.current.leftStick.ReadValue();
                            case LR.Right: return ISGamepad.current.rightStick.ReadValue();
                        }
                        break;
#endif
                }
                return Vector2.zero;
            });

            return vector2Control;
        }

        public static AxisControl CreateGamepadTriggerControl(LR stick)
        {
            AxisControl axisControl = new AxisControl(() =>
            {
                if (!IsDeviceConnected(DeviceType.Gamepad)) return 0f;

                switch (LucidInput.activeInputHandling)
                {
                    case InputHandlingMode.InputManager: return 0f;
#if USE_INPUTSYSTEM
                    case InputHandlingMode.InputSystem:
                        switch (stick)
                        {
                            case LR.Left: return ISGamepad.current.leftTrigger.ReadValue();
                            case LR.Right: return ISGamepad.current.rightTrigger.ReadValue();
                        }
                        break;
#endif
                }
                return 0f;
            });

            return axisControl;
        }

        public static Vector2Control CreateTouchPositionControl(int fingerId)
        {
            Vector2Control vector2Control = new Vector2Control(() =>
            {
                if (!IsTouchActive(fingerId)) return Vector2.zero;

                switch (LucidInput.activeInputHandling)
                {
                    case InputHandlingMode.InputManager: return Input.GetTouch(fingerId).position;
#if USE_INPUTSYSTEM
                    case InputHandlingMode.InputSystem: return ISTouch.activeTouches[fingerId].screenPosition;
#endif
                }
                return Vector2.zero;
            });
            return vector2Control;
        }

        public static Vector2Control CreateTouchDeltaPositionControl(int fingerId)
        {
            Vector2Control vector2Control = new Vector2Control(() =>
            {
                if (!IsTouchActive(fingerId)) return Vector2.zero;

                switch (LucidInput.activeInputHandling)
                {
                    case InputHandlingMode.InputManager: return Input.GetTouch(fingerId).deltaPosition;
#if USE_INPUTSYSTEM
                    case InputHandlingMode.InputSystem: return ISTouch.activeTouches[fingerId].delta;
#endif
                }
                return Vector2.zero;
            });
            return vector2Control;
        }


//         public static Vector2Control CreateTouchStartPositionControl(int fingerId)
//         {
//             Vector2Control vector2Control = new Vector2Control(() =>
//             {
//                 if (!IsTouchActive(fingerId)) return Vector2.zero;

//                 switch (LucidInput.activeInputHandling)
//                 {
//                     case InputHandlingMode.InputManager: return Input.GetTouch(fingerId).rawPosition;
// #if USE_INPUTSYSTEM
//                     case InputHandlingMode.InputSystem: return ISTouch.activeTouches[fingerId].startScreenPosition;
// #endif
//                 }
//                 return Vector2.zero;
//             });
//             return vector2Control;
//         }

        public static ButtonControl CreateTouchButtonControl(int fingerId)
        {
            ButtonControl buttonControl = new ButtonControl
            (
                () =>
                {
                    if (!IsTouchActive(fingerId)) return false;

                    switch (LucidInput.activeInputHandling)
                    {
                        case InputHandlingMode.InputManager: return Input.GetTouch(fingerId).phase == IMTouchPhase.Began;
#if USE_INPUTSYSTEM
                        case InputHandlingMode.InputSystem: return ISTouch.activeTouches[fingerId].phase == ISTouchPhase.Began;
#endif
                    }
                    return false;
                },
                () =>
                {
                    if (!IsTouchActive(fingerId)) return false;

                    switch (LucidInput.activeInputHandling)
                    {
                        case InputHandlingMode.InputManager: return Input.GetTouch(fingerId).phase == IMTouchPhase.Moved || Input.GetTouch(fingerId).phase == IMTouchPhase.Stationary;
#if USE_INPUTSYSTEM
                        case InputHandlingMode.InputSystem: return ISTouch.activeTouches[fingerId].phase == ISTouchPhase.Moved || ISTouch.activeTouches[fingerId].phase == ISTouchPhase.Stationary;
#endif
                    }
                    return false;
                },
                () =>
                {
                    if (!IsTouchActive(fingerId)) return false;

                    switch (LucidInput.activeInputHandling)
                    {
                        case InputHandlingMode.InputManager: return Input.GetTouch(fingerId).phase == IMTouchPhase.Ended;
#if USE_INPUTSYSTEM
                        case InputHandlingMode.InputSystem: return ISTouch.activeTouches[fingerId].phase == ISTouchPhase.Ended;
#endif
                    }
                    return false;
                }
            );
            return buttonControl;
        }


        public static TouchPhaseControl CreateTouchPhaseControl(int fingerId)
        {
            TouchPhaseControl touchPhaseControl = new TouchPhaseControl(() =>
            {
                if (!IsTouchActive(fingerId)) return TouchPhase.None;

                switch (LucidInput.activeInputHandling)
                {
                    case InputHandlingMode.InputManager: return Input.GetTouch(fingerId).phase.ToLucidTouchPhase();
#if USE_INPUTSYSTEM
                    case InputHandlingMode.InputSystem: return ISTouch.activeTouches[fingerId].phase.ToLucidTouchPhase();
#endif
                }
                return TouchPhase.None;
            });
            return touchPhaseControl;
        }

        public static AxisControl CreateTouchPressureControl(int fingerId)
        {
            AxisControl axisControl = new AxisControl(() =>
            {
                if (!IsTouchActive(fingerId)) return 0f;

                switch (LucidInput.activeInputHandling)
                {
                    case InputHandlingMode.InputManager: return Input.GetTouch(fingerId).pressure;
#if USE_INPUTSYSTEM
                    case InputHandlingMode.InputSystem: return ISTouch.activeTouches[fingerId].pressure;
#endif
                }
                return 0f;
            });
            return axisControl;
        }

        public static IntControl CreateTouchTapCountControl(int fingerId)
        {
            IntControl intControl = new IntControl(() =>
            {
                if (!IsTouchActive(fingerId)) return 0;

                switch (LucidInput.activeInputHandling)
                {
                    case InputHandlingMode.InputManager: return Input.GetTouch(fingerId).tapCount;
#if USE_INPUTSYSTEM
                    case InputHandlingMode.InputSystem: return ISTouch.activeTouches[fingerId].tapCount;
#endif
                }
                return 0;
            });
            return intControl;
        }

        public static Vector2Control CreateTouchRadiusControl(int fingerId)
        {
            Vector2Control vector2Control = new Vector2Control(() =>
            {
                if (!IsTouchActive(fingerId)) return Vector2.zero;

                switch (LucidInput.activeInputHandling)
                {
                    case InputHandlingMode.InputManager: return new Vector2(Input.GetTouch(fingerId).radius, Input.GetTouch(fingerId).radius);
#if USE_INPUTSYSTEM
                    case InputHandlingMode.InputSystem: return ISTouch.activeTouches[fingerId].radius;
#endif
                }
                return Vector2.zero;
            });
            return vector2Control;
        }

        public static FloatControl CreateTouchDeltaTimeControl(int fingerId)
        {
            FloatControl floatControl = new FloatControl(() =>
            {
                if (!IsTouchActive(fingerId)) return 0f;

                switch (LucidInput.activeInputHandling)
                {
                    case InputHandlingMode.InputManager: return Input.GetTouch(fingerId).deltaTime;
#if USE_INPUTSYSTEM
                    case InputHandlingMode.InputSystem: return Time.realtimeSinceStartup - (float)ISTouch.activeTouches[fingerId].startTime;
#endif
                }
                return 0;
            });
            return floatControl;
        }

        public static IntControl CreateTouchCountControl()
        {
            IntControl intControl = new IntControl(() =>
            {
                switch (LucidInput.activeInputHandling)
                {
                    case InputHandlingMode.InputManager: return Input.touchCount;

#if USE_INPUTSYSTEM
                    case InputHandlingMode.InputSystem: return ISTouch.activeTouches.Count;
#endif
                }
                return 0;
            });
            return intControl;
        }
    }

}