using UnityEngine;
using IMTouchPhase = UnityEngine.TouchPhase;
#if USE_INPUTSYSTEM
using ISKey = UnityEngine.InputSystem.Key;
using ISGamepadButton = UnityEngine.InputSystem.LowLevel.GamepadButton;
using ISTouchPhase = UnityEngine.InputSystem.TouchPhase;
#endif

namespace AnnulusGames.LucidTools.InputSystem
{
    public static class LucidInputExtensions
    {

        public static KeyCode ToKeyCode(this Key key)
        {
            switch (key)
            {
                default:

                    return KeyCode.None;
                case Key.None: return KeyCode.None;
                case Key.BackSpace: return KeyCode.Backspace;
                case Key.Delete: return KeyCode.Delete;
                case Key.Tab: return KeyCode.Tab;
                case Key.Clear: return KeyCode.Clear;
                case Key.Return: return KeyCode.Return;
                case Key.Pause: return KeyCode.Pause;
                case Key.Escape: return KeyCode.Escape;
                case Key.Space: return KeyCode.Space;
                case Key.Keypad0: return KeyCode.Keypad0;
                case Key.Keypad1: return KeyCode.Keypad1;
                case Key.Keypad2: return KeyCode.Keypad2;
                case Key.Keypad3: return KeyCode.Keypad3;
                case Key.Keypad4: return KeyCode.Keypad4;
                case Key.Keypad5: return KeyCode.Keypad5;
                case Key.Keypad6: return KeyCode.Keypad6;
                case Key.Keypad7: return KeyCode.Keypad7;
                case Key.Keypad8: return KeyCode.Keypad8;
                case Key.Keypad9: return KeyCode.Keypad9;
                case Key.KeypadPeriod: return KeyCode.KeypadPeriod;
                case Key.KeypadDivide: return KeyCode.KeypadDivide;
                case Key.KeypadMultiply: return KeyCode.KeypadMultiply;
                case Key.KeypadMinus: return KeyCode.KeypadMinus;
                case Key.KeypadPlus: return KeyCode.KeypadPlus;
                case Key.KeypadEnter: return KeyCode.KeypadEnter;
                case Key.KeypadEquals: return KeyCode.KeypadEquals;
                case Key.UpArrow: return KeyCode.UpArrow;
                case Key.DownArrow: return KeyCode.DownArrow;
                case Key.RightArrow: return KeyCode.RightArrow;
                case Key.LeftArrow: return KeyCode.LeftArrow;
                case Key.Insert: return KeyCode.Insert;
                case Key.Home: return KeyCode.Home;
                case Key.End: return KeyCode.End;
                case Key.PageUp: return KeyCode.PageUp;
                case Key.PageDown: return KeyCode.PageDown;
                case Key.F1: return KeyCode.F1;
                case Key.F2: return KeyCode.F2;
                case Key.F3: return KeyCode.F3;
                case Key.F4: return KeyCode.F4;
                case Key.F5: return KeyCode.F5;
                case Key.F6: return KeyCode.F6;
                case Key.F7: return KeyCode.F7;
                case Key.F8: return KeyCode.F8;
                case Key.F9: return KeyCode.F9;
                case Key.F10: return KeyCode.F10;
                case Key.F11: return KeyCode.F11;
                case Key.F12: return KeyCode.F12;
                case Key.F13: return KeyCode.F13;
                case Key.F14: return KeyCode.F14;
                case Key.F15: return KeyCode.F15;
                case Key.Alpha0: return KeyCode.Alpha0;
                case Key.Alpha1: return KeyCode.Alpha1;
                case Key.Alpha2: return KeyCode.Alpha2;
                case Key.Alpha3: return KeyCode.Alpha3;
                case Key.Alpha4: return KeyCode.Alpha4;
                case Key.Alpha5: return KeyCode.Alpha5;
                case Key.Alpha6: return KeyCode.Alpha6;
                case Key.Alpha7: return KeyCode.Alpha7;
                case Key.Alpha8: return KeyCode.Alpha8;
                case Key.Alpha9: return KeyCode.Alpha9;
                case Key.Exclaim: return KeyCode.Exclaim;
                case Key.DoubleQuote: return KeyCode.DoubleQuote;
                case Key.Hash: return KeyCode.Hash;
                case Key.Dollar: return KeyCode.Dollar;
                case Key.Percent: return KeyCode.Percent;
                case Key.Ampersand: return KeyCode.Ampersand;
                case Key.Quote: return KeyCode.Quote;
                case Key.LeftParen: return KeyCode.LeftParen;
                case Key.RightParen: return KeyCode.RightParen;
                case Key.Asterisk: return KeyCode.Asterisk;
                case Key.Plus: return KeyCode.Plus;
                case Key.Comma: return KeyCode.Comma;
                case Key.Minus: return KeyCode.Minus;
                case Key.Period: return KeyCode.Period;
                case Key.Slash: return KeyCode.Slash;
                case Key.Colon: return KeyCode.Colon;
                case Key.Semicolon: return KeyCode.Semicolon;
                case Key.Less: return KeyCode.Less;
                case Key.Equals: return KeyCode.Equals;
                case Key.Greater: return KeyCode.Greater;
                case Key.Question: return KeyCode.Question;
                case Key.At: return KeyCode.At;
                case Key.LeftBracket: return KeyCode.LeftBracket;
                case Key.Backslash: return KeyCode.Backslash;
                case Key.RightBracket: return KeyCode.RightBracket;
                case Key.Caret: return KeyCode.Caret;
                case Key.Underscore: return KeyCode.Underscore;
                case Key.BackQuote: return KeyCode.BackQuote;
                case Key.A: return KeyCode.A;
                case Key.B: return KeyCode.B;
                case Key.C: return KeyCode.C;
                case Key.D: return KeyCode.D;
                case Key.E: return KeyCode.E;
                case Key.F: return KeyCode.F;
                case Key.G: return KeyCode.G;
                case Key.H: return KeyCode.H;
                case Key.I: return KeyCode.I;
                case Key.J: return KeyCode.J;
                case Key.K: return KeyCode.K;
                case Key.L: return KeyCode.L;
                case Key.M: return KeyCode.M;
                case Key.N: return KeyCode.N;
                case Key.O: return KeyCode.O;
                case Key.P: return KeyCode.P;
                case Key.Q: return KeyCode.Q;
                case Key.R: return KeyCode.R;
                case Key.S: return KeyCode.S;
                case Key.T: return KeyCode.T;
                case Key.U: return KeyCode.U;
                case Key.V: return KeyCode.V;
                case Key.W: return KeyCode.W;
                case Key.X: return KeyCode.X;
                case Key.Y: return KeyCode.Y;
                case Key.Z: return KeyCode.Z;
                case Key.LeftCurlyBracket: return KeyCode.LeftCurlyBracket;
                case Key.Pipe: return KeyCode.Pipe;
                case Key.Tilde: return KeyCode.Tilde;
                case Key.NumLock: return KeyCode.Numlock;
                case Key.CapsLock: return KeyCode.CapsLock;
                case Key.ScrollLock: return KeyCode.ScrollLock;
                case Key.RightShift: return KeyCode.RightShift;
                case Key.LeftShift: return KeyCode.LeftShift;
                case Key.RightControl: return KeyCode.RightControl;
                case Key.LeftControl: return KeyCode.LeftControl;
                case Key.RightAlt: return KeyCode.RightAlt;
                case Key.LeftAlt: return KeyCode.LeftAlt;
                case Key.LeftCommand: return KeyCode.LeftCommand;
                case Key.RightCommand: return KeyCode.RightCommand;
                case Key.AltGr: return KeyCode.AltGr;
                case Key.Help: return KeyCode.Help;
                case Key.Print: return KeyCode.Print;
                case Key.SysReq: return KeyCode.SysReq;
                case Key.Break: return KeyCode.Break;
                case Key.Menu: return KeyCode.Menu;
            }
        }

#if USE_INPUTSYSTEM

        public static ISKey ToISKey(this Key key)
        {
            switch (key)
            {
                default:

                    return ISKey.None;
                case Key.None: return ISKey.None;
                case Key.BackSpace: return ISKey.Backspace;
                case Key.Delete: return ISKey.Delete;
                case Key.Tab: return ISKey.Tab;
                case Key.Return: return ISKey.Enter;
                case Key.Pause: return ISKey.Pause;
                case Key.Escape: return ISKey.Escape;
                case Key.Space: return ISKey.Space;
                case Key.Keypad0: return ISKey.Numpad0;
                case Key.Keypad1: return ISKey.Numpad1;
                case Key.Keypad2: return ISKey.Numpad2;
                case Key.Keypad3: return ISKey.Numpad3;
                case Key.Keypad4: return ISKey.Numpad4;
                case Key.Keypad5: return ISKey.Numpad5;
                case Key.Keypad6: return ISKey.Numpad6;
                case Key.Keypad7: return ISKey.Numpad7;
                case Key.Keypad8: return ISKey.Numpad8;
                case Key.Keypad9: return ISKey.Numpad9;
                case Key.KeypadPeriod: return ISKey.NumpadPeriod;
                case Key.KeypadDivide: return ISKey.NumpadDivide;
                case Key.KeypadMultiply: return ISKey.NumpadMultiply;
                case Key.KeypadMinus: return ISKey.NumpadMinus;
                case Key.KeypadPlus: return ISKey.NumpadPlus;
                case Key.KeypadEnter: return ISKey.NumpadEnter;
                case Key.KeypadEquals: return ISKey.NumpadEquals;
                case Key.UpArrow: return ISKey.UpArrow;
                case Key.DownArrow: return ISKey.DownArrow;
                case Key.RightArrow: return ISKey.RightArrow;
                case Key.LeftArrow: return ISKey.LeftArrow;
                case Key.Insert: return ISKey.Insert;
                case Key.Home: return ISKey.Home;
                case Key.End: return ISKey.End;
                case Key.PageUp: return ISKey.PageUp;
                case Key.PageDown: return ISKey.PageDown;
                case Key.F1: return ISKey.F1;
                case Key.F2: return ISKey.F2;
                case Key.F3: return ISKey.F3;
                case Key.F4: return ISKey.F4;
                case Key.F5: return ISKey.F5;
                case Key.F6: return ISKey.F6;
                case Key.F7: return ISKey.F7;
                case Key.F8: return ISKey.F8;
                case Key.F9: return ISKey.F9;
                case Key.F10: return ISKey.F10;
                case Key.F11: return ISKey.F11;
                case Key.F12: return ISKey.F12;
                case Key.Alpha0: return ISKey.Digit0;
                case Key.Alpha1: return ISKey.Digit1;
                case Key.Alpha2: return ISKey.Digit2;
                case Key.Alpha3: return ISKey.Digit3;
                case Key.Alpha4: return ISKey.Digit4;
                case Key.Alpha5: return ISKey.Digit5;
                case Key.Alpha6: return ISKey.Digit6;
                case Key.Alpha7: return ISKey.Digit7;
                case Key.Alpha8: return ISKey.Digit8;
                case Key.Alpha9: return ISKey.Digit9;
                case Key.Quote: return ISKey.Quote;
                case Key.Comma: return ISKey.Comma;
                case Key.Minus: return ISKey.Minus;
                case Key.Period: return ISKey.Period;
                case Key.Slash: return ISKey.Slash;
                case Key.Semicolon: return ISKey.Semicolon;
                case Key.Equals: return ISKey.Equals;
                case Key.LeftBracket: return ISKey.LeftBracket;
                case Key.Backslash: return ISKey.Backslash;
                case Key.RightBracket: return ISKey.RightBracket;
                case Key.BackQuote: return ISKey.Backquote;
                case Key.A: return ISKey.A;
                case Key.B: return ISKey.B;
                case Key.C: return ISKey.C;
                case Key.D: return ISKey.D;
                case Key.E: return ISKey.E;
                case Key.F: return ISKey.F;
                case Key.G: return ISKey.G;
                case Key.H: return ISKey.H;
                case Key.I: return ISKey.I;
                case Key.J: return ISKey.J;
                case Key.K: return ISKey.K;
                case Key.L: return ISKey.L;
                case Key.M: return ISKey.M;
                case Key.N: return ISKey.N;
                case Key.O: return ISKey.O;
                case Key.P: return ISKey.P;
                case Key.Q: return ISKey.Q;
                case Key.R: return ISKey.R;
                case Key.S: return ISKey.S;
                case Key.T: return ISKey.T;
                case Key.U: return ISKey.U;
                case Key.V: return ISKey.V;
                case Key.W: return ISKey.W;
                case Key.X: return ISKey.X;
                case Key.Y: return ISKey.Y;
                case Key.Z: return ISKey.Z;
                case Key.NumLock: return ISKey.NumLock;
                case Key.CapsLock: return ISKey.CapsLock;
                case Key.ScrollLock: return ISKey.ScrollLock;
                case Key.RightShift: return ISKey.RightShift;
                case Key.LeftShift: return ISKey.LeftShift;
                case Key.RightControl: return ISKey.RightCtrl;
                case Key.LeftControl: return ISKey.LeftCtrl;
                case Key.RightAlt: return ISKey.RightAlt;
                case Key.LeftAlt: return ISKey.LeftAlt;
                case Key.LeftCommand: return ISKey.LeftCommand;
                case Key.RightCommand: return ISKey.RightCommand;
                case Key.AltGr: return ISKey.AltGr;
            }
        }

        public static ISGamepadButton ToISGamepadButton(this GamepadButton gamepadButton)
        {
            switch (gamepadButton)
            {
                default: throw new System.Exception();
                case GamepadButton.Start: return ISGamepadButton.Start;
                case GamepadButton.Select: return ISGamepadButton.Select;
                case GamepadButton.North: return ISGamepadButton.North;
                case GamepadButton.South: return ISGamepadButton.South;
                case GamepadButton.West: return ISGamepadButton.West;
                case GamepadButton.East: return ISGamepadButton.East;
                case GamepadButton.A: return ISGamepadButton.A;
                case GamepadButton.B: return ISGamepadButton.B;
                case GamepadButton.X: return ISGamepadButton.X;
                case GamepadButton.Y: return ISGamepadButton.Y;
                case GamepadButton.Triangle: return ISGamepadButton.Triangle;
                case GamepadButton.Circle: return ISGamepadButton.Circle;
                case GamepadButton.Cross: return ISGamepadButton.Cross;
                case GamepadButton.Square: return ISGamepadButton.Square;
                case GamepadButton.LeftShoulder: return ISGamepadButton.LeftShoulder;
                case GamepadButton.RightShoulder: return ISGamepadButton.RightShoulder;
                case GamepadButton.LeftTrigger: return ISGamepadButton.LeftTrigger;
                case GamepadButton.RightTrigger: return ISGamepadButton.RightTrigger;
                case GamepadButton.LeftStickButton: return ISGamepadButton.LeftStick;
                case GamepadButton.RightStickButton: return ISGamepadButton.RightStick;
                case GamepadButton.DpadUp: return ISGamepadButton.DpadUp;
                case GamepadButton.DpadDown: return ISGamepadButton.DpadDown;
                case GamepadButton.DpadLeft: return ISGamepadButton.DpadLeft;
                case GamepadButton.DpadRight: return ISGamepadButton.DpadRight;
            }
        }

        internal static TouchPhase ToLucidTouchPhase(this ISTouchPhase phase)
        {
            switch (phase)
            {
                default:
                case ISTouchPhase.None: return TouchPhase.None;
                case ISTouchPhase.Began: return TouchPhase.Began;
                case ISTouchPhase.Moved: return TouchPhase.Moved;
                case ISTouchPhase.Ended: return TouchPhase.Ended;
                case ISTouchPhase.Canceled: return TouchPhase.Canceled;
                case ISTouchPhase.Stationary: return TouchPhase.Stationary;
            }
        }

#endif

        internal static TouchPhase ToLucidTouchPhase(this IMTouchPhase phase)
        {
            switch (phase)
            {
                default: return TouchPhase.None;
                case IMTouchPhase.Began: return TouchPhase.Began;
                case IMTouchPhase.Moved: return TouchPhase.Moved;
                case IMTouchPhase.Ended: return TouchPhase.Ended;
                case IMTouchPhase.Canceled: return TouchPhase.Canceled;
                case IMTouchPhase.Stationary: return TouchPhase.Stationary;
            }
        }

        internal static bool IsNone(this Key key)
        {
            bool isNone = false;
            switch (LucidInput.activeInputHandling)
            {
                case InputHandlingMode.InputManager:
                    if (key.ToKeyCode() == KeyCode.None) isNone = true;
                    break;
#if USE_INPUTSYSTEM
                case InputHandlingMode.InputSystem:
                    if (key.ToISKey() == ISKey.None) isNone = true;
                    break;
#endif
            }
            return isNone;
        }
    }
}
