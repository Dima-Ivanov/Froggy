using System;
using System.Drawing;

namespace Легушка
{
    [Serializable]
    class Message
    {
        public string key;
        public int x, y;
        public int roundx, roundy;
        [NonSerialized]
        public Rectangle rect;
        public int movingspeed;
        public void Shrink()
        {
            key = "";
            x = -1;
            y = -1;
            roundx = -1;
            roundy = -1;
            movingspeed = 0;
        }
    }
}
