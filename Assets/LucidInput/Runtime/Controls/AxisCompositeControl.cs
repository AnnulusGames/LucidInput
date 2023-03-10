using System;
using UnityEngine;

namespace AnnulusGames.LucidTools.InputSystem
{
    internal sealed class AxisCompositeControl : AxisControl
    {
        public AxisCompositeControl(ButtonControl positive, ButtonControl negative) : base(null)
        {
            positiveButton = positive;
            negativeButton = negative;

            Reset();
        }

        private ButtonControl positiveButton;
        private ButtonControl negativeButton;

        private float valuePositive;
        private float valueNegative;
        private float currentValue;
        private float currentRawValue;

        private float timeLastUpdate;

        private const float AXIS_SENSITIVITY = 5f;

        public override void Reset()
        {
            base.Reset();
            positiveButton.Reset();
            negativeButton.Reset();

            valuePositive = 0;
            valueNegative = 0;
            currentValue = 0;
            timeLastUpdate = Time.realtimeSinceStartup;
        }

        public override void Update()
        {
            base.Update();
            positiveButton.Update();
            negativeButton.Update();

            UpdateValue();
            UpdateRawValue();

            timeLastUpdate = Time.realtimeSinceStartup;
        }

        private void UpdateValue()
        {
            float deltaTime = Time.realtimeSinceStartup - timeLastUpdate;
            valuePositive += ((positiveButton.GetButton() ? 1 : 0) * 2f - 1f) * deltaTime * AXIS_SENSITIVITY;
            valuePositive = Math.Clamp(valuePositive, 0f, 1f);
            valueNegative += ((negativeButton.GetButton() ? 1 : 0) * 2f - 1f) * deltaTime * AXIS_SENSITIVITY;
            valueNegative = Math.Clamp(valueNegative, 0f, 1f);

            if (currentValue >= 1) currentValue = valuePositive;
            else if (currentValue <= -1) currentValue = -valueNegative;
            else currentValue = valuePositive > valueNegative ? valuePositive : -valueNegative;
        }

        private void UpdateRawValue()
        {
            currentRawValue = 0;

            currentRawValue += positiveButton.GetButton() ? 1 : 0;
            currentRawValue -= negativeButton.GetButton() ? 1 : 0;
        }

        public override float GetValue()
        {
            return currentValue;
        }

        public override float GetValueRaw()
        {
            return currentRawValue;
        }
    }

}