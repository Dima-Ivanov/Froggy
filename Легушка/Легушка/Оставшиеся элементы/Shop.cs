using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Легушка
{
    [Serializable]
    class Shop
    {
        [NonSerialized]
        List<Image> images2;
        [NonSerialized]
        List<Image> images4;
        [NonSerialized]
        Button goleft;
        [NonSerialized]
        Button goright;
        [NonSerialized]
        Button choose;
        int l, m, r;
        [NonSerialized]
        Form1 form;
        MenuMessage msg;
        [NonSerialized]
        Graphics g;
        int[] costs;
        bool[] bought;
        [NonSerialized]
        Image back;

        [NonSerialized]
        Bitmap screen;
        [NonSerialized]
        Graphics gscreen;

        public Shop(MenuMessage msg, Form1 form, string[] costs)
        {
            this.form = form;
            this.msg = msg;

            screen = new Bitmap(576, 768);
            gscreen = Graphics.FromImage(screen);

            this.costs = new int[4];
            this.bought = new bool[4];
            for (int i = 0; i < 4; i++) this.costs[i] = Convert.ToInt32(costs[i + 1]);
            for (int i = 0; i < 4; i++) this.bought[i] = Convert.ToBoolean(Convert.ToInt32(costs[i + 5]));
            images2 = new List<Image>();
            images4 = new List<Image>();
            images2.Add(Properties.Resources.frogt2);
            images2.Add(Properties.Resources.redfrogt2);
            images2.Add(Properties.Resources.bluefrogt2);
            images2.Add(Properties.Resources.kingt2);
            images4.Add(Properties.Resources.frogt4);
            images4.Add(Properties.Resources.redfrogt4);
            images4.Add(Properties.Resources.bluefrogt4);
            images4.Add(Properties.Resources.kingt4);

            back = Properties.Resources.shop;

            g = form.CreateGraphics();

            Middle();

            goleft = new Button();
            goleft.BackgroundImage = Properties.Resources.arrow;
            goleft.FlatAppearance.BorderSize = 0;
            goleft.FlatStyle = FlatStyle.Flat;
            goleft.BackColor = System.Drawing.Color.Transparent;
            goleft.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            goleft.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            goleft.Text = "";
            goleft.Font = new Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            goleft.Location = new Point(119, 500);
            goleft.Size = new Size(50, 50);
            goleft.TabIndex = 0;
            goleft.UseVisualStyleBackColor = true;
            goleft.Click += new System.EventHandler(goleft_click);

            goright = new Button();
            goright.BackgroundImage = Properties.Resources.arrowreverse;
            goright.FlatAppearance.BorderSize = 0;
            goright.FlatStyle = FlatStyle.Flat;
            goright.BackColor = System.Drawing.Color.Transparent;
            goright.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            goright.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            goright.Text = "";
            goright.Font = new Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            goright.Location = new Point(407, 500);
            goright.Size = new Size(50, 50);
            goright.TabIndex = 0;
            goright.UseVisualStyleBackColor = true;
            goright.Click += new System.EventHandler(goright_click);

            choose = new Button();
            choose.BackgroundImage = Properties.Resources.button;
            choose.FlatAppearance.BorderSize = 0;
            choose.FlatStyle = FlatStyle.Flat;
            choose.BackColor = Color.Transparent;
            choose.FlatAppearance.MouseDownBackColor = Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            choose.FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            choose.Text = "Выбрать";
            choose.Font = new Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            choose.Location = new Point(138, 500);
            choose.Size = new Size(300, 50);
            choose.TabIndex = 0;
            choose.UseVisualStyleBackColor = true;
            choose.Click += new System.EventHandler(choose_click);

            form.Controls.Add(choose);
            form.Controls.Add(goleft);
            form.Controls.Add(goright);
            Hide();
        }

        public string[] GetCosts()
        {
            string[] s = new string[costs.Length * 2];
            for (int i = 0; i < costs.Length; i++)
            {
                s[i] = Convert.ToString(costs[i]);
                s[i + 4] = Convert.ToString(Convert.ToInt32(bought[i]));
            }
            return s;
        }
        public void DeleteBase()
        {
            for (int i = 1; i < bought.Length; i++)
            {
                bought[i] = false;
            }
        }
        private void choose_click(object sender, EventArgs e)
        {
            if (bought[m] == true)
            {
                msg.skinchoice = m;
                msg.menuchoice = 0;
                Hide();
            }
            else if (bought[m] == false && msg.coins >= costs[m])
            {
                bought[m] = true;
                msg.coins -= costs[m];
                choose.Text = "Выбрать";
                form.Refresh();
                Draw();
            }
        }

        private void goright_click(object sender, EventArgs e)
        {
            l++; m++; r++;
            if (l == images2.Count) l = 0;
            else if (m == images2.Count) m = 0;
            else if (r == images2.Count) r = 0;
            if (bought[m] == true) choose.Text = "Выбрать";
            else choose.Text = costs[m].ToString() + " монет";
            Draw();
        }

        private void goleft_click(object sender, EventArgs e)
        {
            l--; m--; r--;
            if (l < 0) l = images2.Count - 1;
            else if (m < 0) m = images2.Count - 1;
            else if (r < 0) r = images2.Count - 1;
            if (bought[m] == true) choose.Text = "Выбрать";
            else choose.Text = costs[m].ToString() + " монет";
            Draw();
        }

        public void Middle()
        {
            m = msg.skinchoice;
            l = m - 1;
            r = m + 1;
            if (l < 0) l = images2.Count - 1;
            if (r == images2.Count) r = 0;
        }

        public void DrawButtons()
        {
            choose.Text = "Выбрать";
            goleft.Show();
            goright.Show();
            choose.Show();
            Draw();
        }

        public void Draw()
        {
            gscreen.DrawImage(back, 0, 0);
            gscreen.DrawImage(images2[l], new Point(16, 328));
            gscreen.DrawImage(images4[m], new Point(160, 200));
            gscreen.DrawImage(images2[r], new Point(432, 328));
            gscreen.DrawString("Монет: " + msg.coins.ToString(), new Font(new FontFamily("Arial"), 20, FontStyle.Bold), new SolidBrush(Color.Black), new Point(400, 707));
            g.DrawImage(screen, 0, 0);
        }

        public void Hide()
        {
            choose.Hide();
            goleft.Hide();
            goright.Hide();
            form.Refresh();
        }
    }
}
