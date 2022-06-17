using System;
using System.Drawing;

namespace Легушка
{
    [Serializable]
    class Earth : Block
    {
        public Earth(int x, int y, Image img, Bitmap screen) : base(x, y, img, screen) { }
        public override void Collision(Message msg)
        {
            msg.key = "E";
        }

        public override void Refresh(Image img, Bitmap screen)
        {
            this.img = img;
            graphics = Graphics.FromImage(screen);
        }

        public override int PlaceMoving(ref bool direction)
        {
            return 0;
        }
    }
}
