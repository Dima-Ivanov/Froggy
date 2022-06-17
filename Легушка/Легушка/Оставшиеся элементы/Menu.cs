using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Легушка
{
    [Serializable]
    class Menu
    {
        [NonSerialized]
        Button play;
        [NonSerialized]
        Button best;
        [NonSerialized]
        Button store;
        [NonSerialized]
        Button info;
        [NonSerialized]
        Button exit;
        [NonSerialized]
        Button back;
        [NonSerialized]
        Button deletebase;
        [NonSerialized]
        Button t;
        public MenuMessage msg;
        [NonSerialized]
        public Form1 form;
        [NonSerialized]
        Image background;
        [NonSerialized]
        Image buttonimage;
        [NonSerialized]
        Image deleteimage;
        [NonSerialized]
        Image infoimage;
        public LeaderBoard leaders;
        public Shop shop;

        public Menu(MenuMessage msg, Form1 form, string[] filebuf)
        {
            this.msg = msg;
            this.form = form;
            leaders = new LeaderBoard(form, filebuf);
            shop = new Shop(msg, form, filebuf);

            background = Properties.Resources.background;
            buttonimage = Properties.Resources.button;
            deleteimage = Properties.Resources.trashbin;
            infoimage = Properties.Resources.info;

            play = new Button();
            play.BackgroundImage = buttonimage;
            play.FlatAppearance.BorderSize = 0;
            play.FlatStyle = FlatStyle.Flat;
            play.BackColor = Color.Transparent;
            play.FlatAppearance.MouseDownBackColor = Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            play.FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            play.Text = "Новая игра";
            play.Font = new Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            play.Location = new Point(138, 200);
            play.Size = new Size(300, 50);
            play.TabIndex = 0;
            play.UseVisualStyleBackColor = true;
            play.Click += new System.EventHandler(play_click);

            best = new Button();
            best.BackgroundImage = buttonimage;
            best.FlatAppearance.BorderSize = 0;
            best.FlatStyle = FlatStyle.Flat;
            best.BackColor = Color.Transparent;
            best.FlatAppearance.MouseDownBackColor = Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            best.FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            best.Text = "Лидеры";
            best.Font = new Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            best.Location = new Point(138, 300);
            best.Size = new Size(300, 50);
            best.TabIndex = 0;
            best.UseVisualStyleBackColor = true;
            best.Click += new System.EventHandler(best_click);

            store = new Button();
            store.BackgroundImage = buttonimage;
            store.FlatAppearance.BorderSize = 0;
            store.FlatStyle = FlatStyle.Flat;
            store.BackColor = System.Drawing.Color.Transparent;
            store.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            store.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            store.Text = "Магазин";
            store.Font = new Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            store.Location = new Point(138, 400);
            store.Size = new Size(300, 50);
            store.TabIndex = 0;
            store.UseVisualStyleBackColor = true;
            store.Click += new System.EventHandler(store_click);

            info = new Button();
            info.BackgroundImage = buttonimage;
            info.FlatAppearance.BorderSize = 0;
            info.FlatStyle = FlatStyle.Flat;
            info.BackColor = System.Drawing.Color.Transparent;
            info.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            info.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            info.Text = "Инфо";
            info.Font = new Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            info.Location = new Point(138, 500);
            info.Size = new Size(300, 50);
            info.TabIndex = 0;
            info.UseVisualStyleBackColor = true;
            info.Click += new System.EventHandler(info_click);

            exit = new Button();
            exit.BackgroundImage = buttonimage;
            exit.FlatAppearance.BorderSize = 0;
            exit.FlatStyle = FlatStyle.Flat;
            exit.BackColor = System.Drawing.Color.Transparent;
            exit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            exit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            exit.Text = "Выход";
            exit.Font = new Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            exit.Location = new Point(138, 600);
            exit.Size = new Size(300, 50);
            exit.TabIndex = 0;
            exit.UseVisualStyleBackColor = true;
            exit.Click += new System.EventHandler(exit_click);

            back = new Button();
            back.BackgroundImage = Image.FromFile("img/arrow.png");
            back.FlatAppearance.BorderSize = 0;
            back.FlatStyle = FlatStyle.Flat;
            back.BackColor = System.Drawing.Color.Transparent;
            back.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            back.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            back.Text = "";
            back.Font = new Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            back.Location = new Point(20, 697);
            back.Size = new Size(50, 50);
            back.TabIndex = 0;
            back.UseVisualStyleBackColor = true;
            back.Click += new System.EventHandler(back_click);

            deletebase = new Button();
            deletebase.BackgroundImage = deleteimage;
            deletebase.FlatAppearance.BorderSize = 0;
            deletebase.FlatStyle = FlatStyle.Flat;
            deletebase.BackColor = Color.Transparent;
            deletebase.FlatAppearance.MouseDownBackColor = Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            deletebase.FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            deletebase.Text = "";
            deletebase.Font = new Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            deletebase.Location = new Point(512, 704);
            deletebase.Size = new Size(32, 32);
            deletebase.TabIndex = 0;
            deletebase.UseVisualStyleBackColor = true;
            deletebase.Click += new System.EventHandler(deletebase_click);

            t = new Button();
            t.Size = new Size(0, 0);
            t.Location = new Point(-100, -100);

            form.Controls.Add(play);
            form.Controls.Add(best);
            form.Controls.Add(store);
            form.Controls.Add(info);
            form.Controls.Add(exit);
            form.Controls.Add(back);
            form.Controls.Add(deletebase);
            form.Controls.Add(t);

            back.Hide();
        }

        

        public string[] GetLeaders()
        {
            return leaders.GetInfo();
        }

        public string[] GetCosts()
        {
            return shop.GetCosts();
        }

        public void Draw()
        {
            form.BackgroundImage = background;
            play.Show();
            best.Show();
            store.Show();
            info.Show();
            exit.Show();
            deletebase.Show();
            back.Hide();
        }

        public void SetFocus()
        {
            t.Focus();
        }

        public void Hide()
        {
            play.Hide();
            best.Hide();
            store.Hide();
            info.Hide();
            exit.Hide();
            deletebase.Hide();
            back.Focus();
            form.Refresh();
        }
        private void play_click(object sender, EventArgs e)
        {
            msg.activegame = false;
            msg.menuchoice = 1;
            Hide();
        }

        private void best_click(object sender, EventArgs e)
        {
            back.Show();
            Hide();
            leaders.Draw();
            msg.menuchoice = 2;
        }

        private void store_click(object sender, EventArgs e)
        {
            back.Show();
            Hide();
            shop.Middle();
            shop.DrawButtons();
            msg.menuchoice = 3;
        }

        private void info_click(object sender, EventArgs e)
        {
            msg.menuchoice = 4;
            Hide();
            form.CreateGraphics().DrawImage(infoimage, 0, 0);
            back.Show();
        }

        private void exit_click(object sender, EventArgs e)
        {
            msg.menuchoice = 5;
        }
        private void deletebase_click(object sender, EventArgs e)
        {
            msg.menuchoice = 6;
        }
        private void back_click(object sender, EventArgs e)
        {
            msg.menuchoice = 0;
            shop.Hide();
            Draw();
        }
    }
}
