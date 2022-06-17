using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Легушка
{
    [Serializable]
    class LeaderBoard
    {
        protected string[] names;
        protected int[] scores;
        [NonSerialized]
        protected Graphics g;

        public LeaderBoard(Form1 form, string[] filebuf)
        {
            names = new string[10];
            scores = new int[10];
            Array.Fill<int>(scores, 0);

            for (int i = 9; i < 28; i+=2)
            {
                names[(i - 9) / 2] = filebuf[i];
                scores[(i - 8) / 2] = Convert.ToInt32(filebuf[i + 1]);
            }

            this.g = form.CreateGraphics();
        }

        public bool Check(int score)
        {
            if (score > scores[9]) return true;
            return false;
        }

        public void DeleteBase()
        {
            for (int i = 0; i < 10;i++)
            {
                names[i] = "";
                scores[i] = 0;
            }
        }

        public string[] GetInfo()
        {
            string[] s = new string[20];
            for (int i = 0; i < 20; i += 2)
            {
                s[i] = names[i / 2];
                s[i + 1] = Convert.ToString(scores[i / 2]);
            }
            return s;
        }

        public void Add(string name, int score)
        {
            for (int i = 0; i < 10; i++)
            {
                if (score > scores[i] || i == 9)
                {
                    for (int j = 9; j > i; j--)
                    {
                        scores[j] = scores[j - 1];
                        names[j] = names[j - 1];
                    }
                    if (name.Length == 0) name = "аноним";
                    names[i] = name;
                    scores[i] = score;
                    break;
                }
            }
        }

        public void Draw()
        {
            g.DrawImage(Properties.Resources.leaders, 0, 0);
            for (int i = 0; i < 10; i++)
            {
                if (scores[i] != 0)
                {
                    g.DrawString(names[i], new Font(new FontFamily("Arial"), 20, FontStyle.Bold), new SolidBrush(Color.Black), new Point(120, 200 + i * 50));
                    g.DrawString(scores[i].ToString(), new Font(new FontFamily("Arial"), 20, FontStyle.Bold), new SolidBrush(Color.Black), new Point(400, 200 + i * 50));
                }
            }
        }
    }
}
