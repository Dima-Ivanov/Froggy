using System;
using System.Drawing;
using System.Windows.Forms;

namespace Легушка
{
    [Serializable]
    class Coin : Item
    {
        [NonSerialized]
        Timer t;
        [NonSerialized]
        ImageList imgl;
        int index = 0;
        public Coin(int x, int y, Image img, Bitmap screen, ImageList imgl) : base(x, y, img, screen)
        {
            this.imgl = imgl;
            t = new Timer();
            t.Tick += new EventHandler(t_tick);
            t.Interval = 200;
            t.Start();
        }
        public override void Refresh(Image img, Bitmap screen)
        {
            imgl = new ImageList();
            imgl.ImageSize = new Size(64, 64);
            imgl.Images.Add(Properties.Resources.coin);
            imgl.Images.Add(Properties.Resources.coin1);
            imgl.Images.Add(Properties.Resources.coin2);
            imgl.Images.Add(Properties.Resources.coin1);
            this.img = imgl.Images[index];
            this.graphics = Graphics.FromImage(screen);
            t = new Timer();
            t.Tick += new EventHandler(t_tick);
            t.Interval = 200;
            t.Start();
        }

        protected void t_tick(object sender, EventArgs eArgs)
        {
            index++;
            index %= imgl.Images.Count;
            img = imgl.Images[index];
        }

        public override void Collision(Message msg)
        {
            msg.key = "COIN";
        }
    }
}
