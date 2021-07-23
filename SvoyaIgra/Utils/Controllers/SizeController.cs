using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SvoyaIgra.Utils.Controllers
{
    public class SizeController
    {
        private readonly int baseWidth;

        private readonly int baseHeight;

        private readonly List<Component> controls;

        public enum CorrectType
        {
            All,
            WidthOnly,
            HeightOnly,
        }

        public SizeController(Size size)
        {
            baseWidth = size.Width;
            baseHeight = size.Height;

            controls = new List<Component>();
        }

        public void AddControl(Control control, CorrectType correctType = CorrectType.All, bool correctPosition = true)
        {
            bool correctWidth = correctType != CorrectType.HeightOnly;
            bool correctHeight = correctType != CorrectType.WidthOnly;

            controls.Add(new Component(control, correctWidth, correctHeight, correctPosition));
        }

        public void ResizeAll(Size size)
        {
            var dx = (float)size.Width / baseWidth;
            var dy = (float)size.Height / baseHeight;

            foreach (var component in controls)
            {
                component.Resize(dx, dy);
            }
        }

    }
}
