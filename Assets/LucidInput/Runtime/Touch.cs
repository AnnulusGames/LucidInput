using UnityEngine;

namespace AnnulusGames.LucidTools.InputSystem
{
    public sealed class Touch
    {
        internal Touch(TouchControl touchControl)
        {
            this.touchControl = touchControl;
        }

        internal TouchControl touchControl  { get; private set; }

        public int fingerId
        {
            get
            {
                return touchControl.fingerId;
            }
        }

        public TouchPhase phase
        {
            get
            {
                return touchControl.phase.GetValue();
            }
        }

        public Vector2 startPosition
        {
            get
            {
                return touchControl.startPosition.GetValue();
            }
        }

        public Vector2 position
        {
            get
            {
                return touchControl.position.GetValue();
            }
        }

        public Vector2 deltaPosition
        {
            get
            {
                return touchControl.deltaPosition.GetValue();
            }
        }

        public float deltaTime
        {
            get
            {
                return touchControl.deltaTime.GetValue();
            }
        }

        public float pressure
        {
            get
            {
                return touchControl.pressure.GetValue();
            }
        }

        public Vector2 radius
        {
            get
            {
                return touchControl.radius.GetValue();
            }
        }

        public bool wasPressedThisFrame
        {
            get
            {
                return touchControl.press.GetButtonDown();
            }
        }

        public bool isPressed
        {
            get
            {
                return touchControl.press.GetButton();
            }
        }

        public bool wasReleasedThisFrame
        {
            get
            {
                return touchControl.press.GetButtonUp();
            }
        }
    }
}
