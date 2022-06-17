using System;
using System.Drawing;

namespace Легушка
{
    [Serializable]
    abstract class Moving_Item : Item
    {
        protected bool direction; // 0 - влево, 1 - вправо
        protected int speed;
        protected int width; // Измеряется в блоках
        [NonSerialized]
        protected Rectangle rect;
        public Moving_Item(int x, int y, Image img, Bitmap screen, bool direction, int speed, int width) : base(x, y, img, screen)
        {
            this.direction = direction;
            this.speed = speed;
            this.width = width;
        }

        public int GetSpeed() { return speed; }
        public void ChangeSpeed(int speed) { this.speed = speed; }
        public int GetY() { return y; }
        public int GetX() { return x; }
        public abstract Rectangle GetRect();

        override public void Draw()
        {
            if (direction == false)
            {
                img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                graphics.DrawImage(img, new Point(x, y));
                img.RotateFlip(RotateFlipType.Rotate180FlipNone);
            }
            else graphics.DrawImage(img, new Point(x, y));
        }

        public override void Update()
        {
            y += 64;
        }
        virtual public void Move()
        {
            if (direction == false) x -= speed;
            else x += speed;
        }
    }
}
