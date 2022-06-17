using System;
using System.Drawing;

namespace Легушка
{
    [Serializable]
    abstract class Block
    {
        protected int x, y;
        protected int cd;
        [NonSerialized]
        protected Image img;
        [NonSerialized]
        protected Graphics graphics;
        public Block(int x, int y, Image img, Bitmap screen)
        {
            cd = 0;
            this.x = x;
            this.y = y;
            this.img = img;
            graphics = Graphics.FromImage(screen);
        }

        public void Update() { y++; }

        public virtual void Draw()
        {
            graphics.DrawImage(img, new Point(x * 64, y * 64));
        }

        public abstract void Refresh(Image img, Bitmap screen);
        public abstract int PlaceMoving(ref bool direction);
        public abstract void Collision(Message msg);
    }
}
