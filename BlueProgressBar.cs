using System;
using System.Drawing;

namespace La2Laucher
{
    public class BlueProgressBar : CustomProgressBar
    {
        private double lastSpeed = 0;

        public BlueProgressBar()
        {
            StartColor = Color.DodgerBlue;
            EndColor = Color.MidnightBlue;
        }

        public void UpdateSpeed(double speedBytes)
        {
            // Normalização inteligente
            double maxSpeed = 2 * 1024 * 1024; // 2MB/s

            double percent = speedBytes / maxSpeed;
            percent = Math.Min(1.0, percent);

            // Smooth animation
            lastSpeed = (lastSpeed * 0.7) + (percent * 0.3);

            SafeSetValue((int)(lastSpeed * 100));
        }
    }
}
