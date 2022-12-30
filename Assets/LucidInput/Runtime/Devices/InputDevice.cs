namespace AnnulusGames.LucidTools.InputSystem
{
    internal abstract class InputDevice
    {
        public abstract bool isConnected { get; }

        public abstract void Reset();
        public abstract void Update();
    }

    internal enum DeviceType
    {
        Keyboard,
        Mouse,
        Touchscreen,
        Gamepad
    }
}
