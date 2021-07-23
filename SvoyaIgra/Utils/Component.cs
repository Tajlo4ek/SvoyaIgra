using System;
using System.Windows.Forms;

namespace SvoyaIgra.Utils
{
    public class Component
    {
        private readonly Control control;

        private readonly int baseWidth;

        private readonly int baseHeight;

        private readonly System.Drawing.Point basePositon;

        private readonly bool correctWidth;
        private readonly bool correctHeight;
        private readonly bool correctPosition;

        public Component(Control control, bool correctWidth, bool correctHeight, bool correctPosition)
        {
            this.control = control;
            baseWidth = control.Size.Width;
            baseHeight = control.Size.Height;

            this.correctWidth = correctWidth;
            this.correctHeight = correctHeight;
            this.correctPosition = correctPosition;

            basePositon = new System.Drawing.Point(control.Left, control.Top);
        }

        public void Resize(float dw, float dh)
        {
            int newWidth = correctWidth ? (int)Math.Round(dw * baseWidth) : control.Size.Width;
            int newHeight = correctHeight ? (int)Math.Round(dh * baseHeight) : control.Size.Height;

            control.Size = new System.Drawing.Size(newWidth, newHeight);

            if (correctPosition)
            {
                control.Left = (int)(basePositon.X * dw);
                control.Top = (int)(basePositon.Y * dh);
            }
        }

    }
}
