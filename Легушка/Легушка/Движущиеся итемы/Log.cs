using System;
using System.Drawing;

namespace Легушка
{
    [Serializable]
    class Log : Moving_Item
    {
        public Log(int x, int y, Image img, Bitmap screen, bool direction, int speed, int width) : base(x, y, img, screen, direction, speed, width) { }
        public override void Collision(Message msg)
        {
            rect.X = x + 10;
            rect.Y = y;
            rect.Width = width - 20;
            rect.Height = 64;
            rect.Intersect(msg.rect);
            if (!rect.IsEmpty)
            {
                if (direction == false) msg.key = "LOGL";
                else msg.key = "LOGR";
                msg.movingspeed = speed;
            }
        }
        public override void Refresh(Image img, Bitmap screen)
        {
            this.img = Properties.Resources.log;
            this.graphics = Graphics.FromImage(screen);
            this.rect = new Rectangle();
        }
        public override Rectangle GetRect() 
        {
            rect.X = x + 8;
            rect.Y = y;
            rect.Width = width - 16;
            rect.Height = 64;
            return rect;
        }
    }
}
