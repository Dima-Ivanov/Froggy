using System;
using System.Drawing;
using System.Windows.Forms;

namespace Легушка
{
    [Serializable]
    class Frog
    {
        protected int x, y;
        protected int direction; // 0 - вверх, 1 - вправо, 2 - вниз, 3 - влево
        [NonSerialized]
        protected ImageList imgl;
        [NonSerialized]
        protected Image img;
        [NonSerialized]
        protected Graphics graphics;
        protected int startx, starty;
        protected bool dead;
        [NonSerialized]
        Timer t;

        public Frog(int x, int y, ImageList imgl, Bitmap screen)
        {
            this.startx = x;
            this.starty = y;
            this.x = x;
            this.y = y;
            this.imgl = imgl;
            this.img = this.imgl.Images[0];
            dead = false;
            t = new Timer();
            t.Tick += new EventHandler(t_tick);
            t.Interval = 200;
            t.Stop();


            graphics = Graphics.FromImage(screen);
        }

        public void Refresh(ImageList imgl, Bitmap screen)
        {
            this.imgl = imgl;
            graphics = Graphics.FromImage(screen);
            this.img = this.imgl.Images[0];
            t = new Timer();
            t.Tick += new EventHandler(t_tick);
            t.Interval = 200;
            t.Stop();
        }

        protected void t_tick(object sender, EventArgs eArgs)
        {
            img = imgl.Images[direction];
            t.Stop();
        }

        public void Round()
        {
            this.x = (int)Math.Round((double)x / 64) * 64;
        }

        public void Shrink(ImageList imgl)
        {
            this.x = startx;
            this.y = starty;
            dead = false;
            this.imgl = imgl;
            img = imgl.Images[0];
        }

        public void Direction()
        {
            direction = 0;
            img = imgl.Images[direction + 4];
            t.Start();
        }

        public void Event(Message msg)
        {
            if (msg.key == "" || dead) return;

            msg.x = this.x;
            msg.y = this.y;
            if (msg.key == "KeyUp")
            {
                direction = 0;
                img = imgl.Images[direction + 4];
                t.Start();
                Round();
                y -= 64;
            }
            else if (msg.key == "KeyDown")
            {
                direction = 2;
                img = imgl.Images[direction + 4];
                t.Start();
                Round();
                y += 64;
            }
            else if (msg.key == "KeyLeft")
            {
                direction = 3;
                img = imgl.Images[direction + 4];
                t.Start();
                x -= 64;
            }
            else if (msg.key == "KeyRight")
            {
                direction = 1;
                img = imgl.Images[direction + 4];
                t.Start();
                x += 64;
            }
            else if (msg.key == "LOGR") x += msg.movingspeed;
            else if (msg.key == "LOGL") x -= msg.movingspeed;
            else if (msg.key == "DIE")
            {
                dead = true;
                t.Stop();
                img = imgl.Images[direction + 8];
            }
        }

        public void GetCoordinates(Message msg)
        {
            msg.x = this.x;
            msg.y = this.y;
            msg.roundx = (int)Math.Round((double)x / 64);
            msg.roundy = y / 64;
            msg.rect = new Rectangle(x + 16, y + 16, 32, 32);
        }

        public void Draw()
        {
            graphics.DrawImage(img, new Point(x, y));
        }
    }
}
