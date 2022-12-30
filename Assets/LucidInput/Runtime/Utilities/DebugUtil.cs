using UnityEngine;

namespace AnnulusGames.LucidTools.InputSystem
{
    internal static class DebugUtil
    {
        public static void LogWarning(object message)
        {
            if (LucidInput.logEnabled) Debug.LogWarning("[Lucid Input] " + message);
        }

        public static void LogWarningIfGamepadIsNotSupported()
        {
            if (LucidInput.activeInputHandling == InputHandlingMode.InputManager)
            {
                LogWarning("Gamepad input is not supported in Input Manager.");
            }
            else if (!LucidInput.gamepad.isConnected)
            {
                LogWarning("Gamepad is not connected.");
            }
        }
    }
}
