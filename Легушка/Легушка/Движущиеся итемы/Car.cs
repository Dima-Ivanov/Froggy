using System;
using System.Drawing;

namespace Легушка
{
    [Serializable]
    class Car : Moving_Item
    {
        public Car(int x, int y, Image img, Bitmap screen, bool direction, int speed, int width) : base(x, y, img, screen, direction, speed, width) { }
        public override void Collision(Message msg)
        {
            rect.X = x + 15;
            rect.Y = y;
            rect.Width = width - 30;
            rect.Height = 64;
            rect.Intersect(msg.rect);
            if (!rect.IsEmpty) msg.key = "DIE";
        }
        public override void Refresh(Image img, Bitmap screen)
        {
            this.img = Properties.Resources.car;
            this.graphics = Graphics.FromImage(screen);
            this.rect = new Rectangle();
        }
        public override Rectangle GetRect() 
        {
            rect.X = x;
            rect.Y = y;
            rect.Width = width;
            rect.Height = 64;
            return rect;
        }
    }
}
