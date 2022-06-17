using System;
using System.Drawing;

namespace Легушка
{
    [Serializable]
    class Road : Block
    {
        bool direction;
        [NonSerialized]
        Random r;
        public Road(int x, int y, Image img, Bitmap screen, bool direction) : base(x, y, img, screen)
        {
            r = new Random();
            this.direction = direction;
        }

        public override void Refresh(Image img, Bitmap screen)
        {
            this.img = img;
            graphics = Graphics.FromImage(screen);
            r = new Random();
        }

        public override void Collision(Message msg)
        {
            msg.key = "R";
        }

        public override int PlaceMoving(ref bool direction)
        {
            if (cd > 0) cd--;
            else if (r.Next(100) < 2)
            {
                direction = this.direction;
                cd = 64;
                if (r.Next(100) > 30) return 2;
                return 3;
            }
            return 0;
        }
    }
}
