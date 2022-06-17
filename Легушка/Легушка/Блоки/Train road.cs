using System;
using System.Drawing;

namespace Легушка
{
    [Serializable]
    class Train_road : Block
    {
        bool direction;
        [NonSerialized]
        Image redroad;
        int waitfortrain = 0;
        [NonSerialized]
        Random r;
        public Train_road(int x, int y, Image img, Bitmap screen, bool direction, Image redroad) : base(x, y, img, screen)
        {
            r = new Random();
            this.redroad = redroad;
            this.direction = direction;
        }
        public override void Collision(Message msg)
        {
            msg.key = "T";
        }

        public override void Refresh(Image img, Bitmap screen)
        {
            this.img = img;
            graphics = Graphics.FromImage(screen);
            r = new Random();
            redroad = Properties.Resources.redtrainroad;
        }

        public override void Draw()
        {
            if (waitfortrain > 0)
            {
                waitfortrain--;
                if (direction == false)
                {
                    redroad.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    graphics.DrawImage(redroad, new Point(x * 64, y * 64));
                    redroad.RotateFlip(RotateFlipType.Rotate180FlipNone);
                }
                else graphics.DrawImage(redroad, new Point(x * 64, y * 64));
            }
            else graphics.DrawImage(img, new Point(x * 64, y * 64));
        }

        public override int PlaceMoving(ref bool direction)
        {
            if (cd > 0) cd--;
            else if (r.Next(100) < 1)
            {
                direction = this.direction;
                cd = 256;
                waitfortrain = 120;
                return 4;
            }
            return 0;
        }
    }
}
