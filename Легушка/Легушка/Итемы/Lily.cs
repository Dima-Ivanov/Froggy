using System;
using System.Drawing;

namespace Легушка
{
    [Serializable]
    class Lily : Item
    {
        public Lily(int x, int y, Image img, Bitmap screen) : base(x, y, img, screen) { }
        public override void Collision(Message msg)
        {
            msg.key = "";
        }
        public override void Refresh(Image img, Bitmap screen)
        {
            this.img = img;
            this.graphics = Graphics.FromImage(screen);
        }
    }
}
