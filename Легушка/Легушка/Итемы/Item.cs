using System;
using System.Drawing;

namespace Легушка
{
    [Serializable]
    abstract class Item
    {
        protected int x, y;
        [NonSerialized]
        protected Image img;
        [NonSerialized]
        protected Graphics graphics;
        public Item(int x, int y, Image img, Bitmap screen)
        {
            this.x = x;
            this.y = y;
            this.img = img;
            graphics = Graphics.FromImage(screen);
        }

        virtual public void Update() { y++; }

        virtual public void Draw()
        {
            graphics.DrawImage(img, new Point(x * 64, y * 64));
        }

        public abstract void Collision(Message msg);
        public abstract void Refresh(Image img, Bitmap screen);
    }
}
