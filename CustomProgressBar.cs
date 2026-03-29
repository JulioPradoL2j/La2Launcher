using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace La2Laucher
{
    public class CustomProgressBar : ProgressBar
    {
        public Color StartColor { get; set; } = Color.Orange;
        public Color EndColor { get; set; } = Color.DarkOrange;
        public Color BackColorBar { get; set; } = Color.FromArgb(25, 25, 25);

        public CustomProgressBar()
        {
            if (!LicenseManager.UsageMode.Equals(LicenseUsageMode.Designtime))
            {
                SetStyle(ControlStyles.UserPaint |
                         ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.OptimizedDoubleBuffer, true);
            }

            Maximum = 100;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect = ClientRectangle;

            using (SolidBrush bg = new SolidBrush(BackColorBar))
                e.Graphics.FillRectangle(bg, rect);

            int safeMax = Maximum <= 0 ? 1 : Maximum;
            int safeVal = Math.Max(0, Math.Min(Value, safeMax));

            int width = (int)(rect.Width * ((double)safeVal / safeMax));

            if (width > 0)
            {
                Rectangle progressRect = new Rectangle(0, 0, width, rect.Height);

                using (LinearGradientBrush brush = new LinearGradientBrush(
                    progressRect,
                    StartColor,
                    EndColor,
                    LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, progressRect);
                }
            }
        }

        public void SafeSetValue(int value)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => SafeSetValue(value)));
                return;
            }

            Value = Math.Max(0, Math.Min(value, Maximum));
        }
    }
}
