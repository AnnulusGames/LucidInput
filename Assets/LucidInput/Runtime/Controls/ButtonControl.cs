using System;
using UnityEngine;

namespace AnnulusGames.LucidTools.InputSystem
{
    internal class ButtonControl : InputControl
    {
        public ButtonControl(Func<bool> getButtonDown, Func<bool> getButton, Func<bool> getButtonUp)
        {
            this.getButtonDown = getButtonDown;
            this.getButton = getButton;
            this.getButtonUp = getButtonUp;

            Reset();
        }

        private Func<bool> getButtonDown;
        private Func<bool> getButton;
        private Func<bool> getButtonUp;

        public virtual bool GetButtonDown()
        {
            return getButtonDown();
        }

        public virtual bool GetButton()
        {
            return getButton();
        }

        public virtual bool GetButtonUp()
        {
            return getButtonUp();
        }

        public bool GetButtonHold(float time)
        {
            return getButton() && time <= timePressed;
        }

        public bool GetButtonHoldEnded(float time)
        {
            return getButtonUp() && time <= timePressed;
        }

        public bool GetButtonTap(int count)
        {
            return getButtonUp() && timePressed <= LucidInput.tapTime && tapCount == count;
        }

        public virtual int tapCount { get; private set; }
        public virtual float timePressed { get; private set; }
        public virtual float timeUnpressed { get; private set; }

        public bool wasPressedEvenOnce { get; private set; }
        private float timeLastUpdate;

        public override void Reset()
        {
            tapCount = 0;
            timePressed = 0f;
            timeUnpressed = 0f;

            wasPressedEvenOnce = false;
            timeLastUpdate = Time.realtimeSinceStartup;
        }

        public override void Update()
        {
            float deltaTime = Time.realtimeSinceStartup - timeLastUpdate;

            if (GetButtonDown())
            {
                wasPressedEvenOnce = true;

                timePressed = 0;
                timeUnpressed = 0;
            }
            else if (GetButton())
            {
                timePressed += deltaTime;
            }
            else if (GetButtonUp())
            {
                timePressed = 0;
                tapCount++;
            }
            else if (wasPressedEvenOnce)
            {
                timeUnpressed += deltaTime;
                if (timeUnpressed > LucidInput.tapInterval)
                {
                    tapCount = 0;
                }
            }

            timeLastUpdate = Time.realtimeSinceStartup;
        }
    }

}