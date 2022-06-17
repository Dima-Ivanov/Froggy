using System;
using System.Drawing;

namespace Легушка
{
    [Serializable]
    class Water : Block
    {
        bool direction;
        [NonSerialized]
        Random r;
        public Water(int x, int y, Image img, Bitmap screen, bool direction) : base(x, y, img, screen)
        {
            r = new Random();
            this.direction = direction;
        }
        public override void Collision(Message msg)
        {
            msg.key = "DIE";
        }
        public override void Refresh(Image img, Bitmap screen)
        {
            this.img = img;
            graphics = Graphics.FromImage(screen);
            r = new Random();
        }
        public override int PlaceMoving(ref bool direction)
        {
            if (cd > 0) cd--;
            else if (r.Next(100) < 5)
            {
                direction = this.direction;
                cd = 64;
                return 1;
            }
            return 0;
        }
    }
}
