using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SvoyaIgra.Utils.Controllers
{
    public class ImageController
    {
        private readonly PictureBox pictureBox;

        private readonly Timer timer;

        private Image imgForDraw;

        private float procY;
        private int maxY;

        private Action onEndCallback;

        public delegate Image OnResizeDelegate();
        private OnResizeDelegate onResize;

        private float speed = 0;

        private float time;
        private bool isInfinity;

        private enum Type
        {
            None,
            Show,
            Scroll,
        }

        private Type type;

        public ImageController(PictureBox pictureBox, Timer timer, Action onEnd, OnResizeDelegate onResize)
        {
            this.pictureBox = pictureBox;
            this.timer = timer;

            this.onResize += onResize;

            pictureBox.Paint += new PaintEventHandler(RePaint);
            timer.Tick += new EventHandler(Update);

            onEndCallback += onEnd;

            timer.Interval = 10;
        }

        public void ShowMoveToUpImage(Image img)
        {
            Stop();

            imgForDraw = img;
            type = Type.Scroll;

            procY = 0;
            maxY = pictureBox.Height + imgForDraw.Height;
            CalcSpeed();

            timer.Start();

            isInfinity = false;
        }

        public void ShowImg(Image img, float time)
        {
            Stop();

            imgForDraw = img;
            procY = 0;
            type = Type.Show;

            pictureBox.Refresh();

            if (time != -1)
            {
                isInfinity = false;
                this.time = (int)(time * 1000);
                timer.Start();
            }
            else
            {
                isInfinity = true;
            }
        }

        private void Update(object sender, EventArgs e)
        {
            if (type == Type.Scroll)
            {
                pictureBox.Refresh();
                procY += speed;
                if (procY >= 1)
                {
                    timer.Stop();
                    type = Type.None;
                    onEndCallback();
                }
            }
            else if (type == Type.Show)
            {
                if (!isInfinity)
                {
                    if (time < 0)
                    {
                        timer.Stop();
                        type = Type.None;
                        onEndCallback();
                    }
                    else
                    {
                        time -= timer.Interval;
                    }
                }
            }
        }

        public void Pause()
        {
            timer.Stop();
        }

        public void Start()
        {
            if (!isInfinity && type != Type.None)
                timer.Start();
        }

        private void CalcSpeed()
        {
            float countScreen = (float)imgForDraw.Height / pictureBox.Height;
            if (countScreen < 1)
                countScreen = 1;

            speed = 0.005f / countScreen;
        }

        private void RePaint(object sender, PaintEventArgs e)
        {
            if (imgForDraw != null)
            {
                if (type == Type.Scroll)
                {
                    e.Graphics.DrawImage(imgForDraw, 0, pictureBox.Height - procY * maxY);
                }
                else if (type == Type.Show)
                {
                    if (pictureBox.Height > imgForDraw.Height)
                    {
                        e.Graphics.DrawImage(imgForDraw, 0, (pictureBox.Height - imgForDraw.Height) / 2);
                    }
                    else
                    {
                        e.Graphics.DrawImage(imgForDraw, 0, 0, pictureBox.Width, pictureBox.Height);
                    }
                }
            }
        }

        public void Resize()
        {
            imgForDraw = onResize();

            if (imgForDraw != null)
            {
                maxY = pictureBox.Height + imgForDraw.Height;
                CalcSpeed();
            }
            pictureBox.Refresh();
        }

        public void Stop()
        {
            timer.Stop();
            imgForDraw = null;
        }

        public void Dispose()
        {
            Stop();
            pictureBox.Paint -= RePaint;
            timer.Dispose();
            onEndCallback = null;
            onResize = null;
        }

    }
}
