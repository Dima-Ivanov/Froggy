using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Легушка
{
    [Serializable]
    class Game
    {
        protected Message msg;
        protected Map map;
        protected Frog frog;
        [NonSerialized]
        protected Form1 form;
        [NonSerialized]
        protected Bitmap screen;
        protected bool dead;
        protected int score;
        [NonSerialized]
        protected Timer t, t2;
        protected Menu menu;
        protected MenuMessage menumsg;
        protected int coordinate;
        [NonSerialized]
        protected TextBox input;
        [NonSerialized]
        protected List<ImageList> img;
        [NonSerialized]
        protected Graphics gscreen, gform;
        [NonSerialized]
        protected Image plateimage;
        protected Image topimage;
        protected string[] filebuf;
        
        public Game(Form1 form)
        {
            menumsg = new MenuMessage();

            filebuf = File.ReadAllLines("data.txt");
            menumsg.coins = Convert.ToInt32(filebuf[0]);
            menumsg.skinchoice = Convert.ToInt32(filebuf[filebuf.Length - 1]);

            gform = form.CreateGraphics();
            screen = new Bitmap(576, 768);
            gscreen = Graphics.FromImage(screen);
            plateimage = Properties.Resources.plate;
            topimage = Properties.Resources.top;
            this.form = form;
            msg = new Message();
            msg.Shrink();
            map = new Map(form, screen);
            img = new List<ImageList>();
            img.Add(new ImageList());
            img[0].ImageSize = new Size(64, 64);
            img[0].Images.Add(Properties.Resources.frogt);
            img[0].Images.Add(Properties.Resources.frogr);
            img[0].Images.Add(Properties.Resources.frogb);
            img[0].Images.Add(Properties.Resources.frogl);
            img[0].Images.Add(Properties.Resources.frogjumpt);
            img[0].Images.Add(Properties.Resources.frogjumpr);
            img[0].Images.Add(Properties.Resources.frogjumpb);
            img[0].Images.Add(Properties.Resources.frogjumpl);
            img[0].Images.Add(Properties.Resources.frogdeadt);
            img[0].Images.Add(Properties.Resources.frogdeadr);
            img[0].Images.Add(Properties.Resources.frogdeadb);
            img[0].Images.Add(Properties.Resources.frogdeadl);

            img.Add(new ImageList());
            img[1].ImageSize = new Size(64, 64);
            img[1].Images.Add(Properties.Resources.redfrogt);
            img[1].Images.Add(Properties.Resources.redfrogr);
            img[1].Images.Add(Properties.Resources.redfrogb);
            img[1].Images.Add(Properties.Resources.redfrogl);
            img[1].Images.Add(Properties.Resources.redfrogjumpt);
            img[1].Images.Add(Properties.Resources.redfrogjumpr);
            img[1].Images.Add(Properties.Resources.redfrogjumpb);
            img[1].Images.Add(Properties.Resources.redfrogjumpl);
            img[1].Images.Add(Properties.Resources.redfrogdeadt);
            img[1].Images.Add(Properties.Resources.redfrogdeadr);
            img[1].Images.Add(Properties.Resources.redfrogdeadb);
            img[1].Images.Add(Properties.Resources.redfrogdeadl);

            img.Add(new ImageList());
            img[2].ImageSize = new Size(64, 64);
            img[2].Images.Add(Properties.Resources.bluefrogt);
            img[2].Images.Add(Properties.Resources.bluefrogr);
            img[2].Images.Add(Properties.Resources.bluefrogb);
            img[2].Images.Add(Properties.Resources.bluefrogl);
            img[2].Images.Add(Properties.Resources.bluefrogjumpt);
            img[2].Images.Add(Properties.Resources.bluefrogjumpr);
            img[2].Images.Add(Properties.Resources.bluefrogjumpb);
            img[2].Images.Add(Properties.Resources.bluefrogjumpl);
            img[2].Images.Add(Properties.Resources.bluefrogdeadt);
            img[2].Images.Add(Properties.Resources.bluefrogdeadr);
            img[2].Images.Add(Properties.Resources.bluefrogdeadb);
            img[2].Images.Add(Properties.Resources.bluefrogdeadl);

            img.Add(new ImageList());
            img[3].ImageSize = new Size(64, 64);
            img[3].Images.Add(Properties.Resources.kingt);
            img[3].Images.Add(Properties.Resources.kingr);
            img[3].Images.Add(Properties.Resources.kingb);
            img[3].Images.Add(Properties.Resources.kingl);
            img[3].Images.Add(Properties.Resources.kingjumpt);
            img[3].Images.Add(Properties.Resources.kingjumpr);
            img[3].Images.Add(Properties.Resources.kingjumpb);
            img[3].Images.Add(Properties.Resources.kingjumpl);
            img[3].Images.Add(Properties.Resources.kingdeadt);
            img[3].Images.Add(Properties.Resources.kingdeadr);
            img[3].Images.Add(Properties.Resources.kingdeadb);
            img[3].Images.Add(Properties.Resources.kingdeadl);

            frog = new Frog(256, 384, img[menumsg.skinchoice], screen);
            dead = false;
            score = 0;
            t = new Timer();
            t.Interval = 16;
            t.Tick += new EventHandler(t_tick);
            t.Start();
            t2 = new Timer();
            t2.Interval = 100;
            t2.Tick += new EventHandler(t2_tick);
            this.form.KeyDown += new System.Windows.Forms.KeyEventHandler(KeyDown);
            menu = new Menu(menumsg, form, filebuf);

            input = new TextBox();
            input.BorderStyle = BorderStyle.None;
            input.TextAlign = HorizontalAlignment.Center;
            input.MaxLength = 9;
            input.Text = null;
            input.Font = new Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            input.Location = new Point(148, 300);
            input.Size = new Size(280, 70);
            input.TabIndex = 0;
            input.Hide();
            form.Controls.Add(input);

            this.form.FormClosed += Form_FormClosed;
            DeSerialization();
        }

        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            filebuf[0] = Convert.ToString(menumsg.coins);
            string[] costs = menu.GetCosts();
            string[] leaders = menu.GetLeaders();
            for (int i = 1; i < 9; i++) filebuf[i] = costs[i - 1];
            for (int i = 9; i < 29; i++) filebuf[i] = leaders[i - 9];
            filebuf[29] = Convert.ToString(menumsg.skinchoice);
            File.WriteAllLines("data.txt", filebuf);
            if (score != 0)
            {
                Serialization();
            }
            else
            {
                File.Delete("game.dat");
            }
        }

        public void Serialization()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("game.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, this);
            }
        }
        public void DeSerialization()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            if (System.IO.File.Exists("game.dat"))
            {
                Game gameHelper;
                using (FileStream fs = new FileStream("game.dat", FileMode.Open))
                {
                    if (fs.Length != 0)
                    {
                        gameHelper = (Game)formatter.Deserialize(fs);
                        this.dead = gameHelper.dead;
                        if (this.dead)
                        {
                            this.input.Show();
                        }
                        this.score = gameHelper.score;
                        this.map = gameHelper.map;
                        this.map.Refresh(form, screen);
                        this.frog = gameHelper.frog;
                        this.menumsg.skinchoice = gameHelper.menumsg.skinchoice;
                        this.menu.msg.skinchoice = gameHelper.menu.msg.skinchoice;
                        frog.Refresh(img[menumsg.skinchoice], screen);
                        this.menumsg.menuchoice = 1;
                        this.menumsg.activegame = true;
                        this.menu.msg.menuchoice = 1;
                        this.menu.msg.activegame = true;
                        this.menu.Hide();
                    }
                }
            }
        }

        protected void t_tick(object sender, EventArgs eArgs)
        {
            Play();
        }

        protected void t2_tick(object sender, EventArgs eArgs)
        {
            t2.Stop();
        }

        protected void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W && !t2.Enabled)
            {
                Event("KeyUp");
            }
            else if (e.KeyCode == Keys.A && !t2.Enabled)
            {
                Event("KeyLeft");
            }
            else if (e.KeyCode == Keys.S && !t2.Enabled)
            {
                Event("KeyDown");
            }
            else if (e.KeyCode == Keys.D && !t2.Enabled)
            {
                Event("KeyRight");
            }
            else if (e.KeyCode == Keys.Space && !t2.Enabled)
            {
                Event("Space");
            }
            else if (e.KeyCode == Keys.Enter && !t2.Enabled)
            {
                Event("Enter");
            }
            else if (e.KeyCode == Keys.Escape && !t2.Enabled)
            {
                Event("Escape");
            }
            t2.Start();
        }

        protected void Draw()
        {
            if (menumsg.menuchoice == 0)
            {
                menu.Draw();
            }
            else if (menumsg.menuchoice == 1)
            {
                map.Draw();
                frog.Draw();
                if (dead) map.DrawMoving();
                gscreen.DrawImage(topimage, 0, 0);
                gscreen.DrawString("Счет: " + score.ToString(), new Font(new FontFamily("Arial"), 20, FontStyle.Bold), new SolidBrush(Color.Black), new Point(0, 0));
                coordinate = 0;
                for (int i = menumsg.coins.ToString().Length; i < 5; i++) coordinate++;
                gscreen.DrawString("Монет: " + menumsg.coins.ToString(), new Font(new FontFamily("Arial"), 20, FontStyle.Bold), new SolidBrush(Color.Black), new Point(392 + coordinate * 15, 0));

                if (input.Visible)
                {
                    gscreen.DrawImage(plateimage, new Point(128, 210));
                    gscreen.DrawString("Новый рекорд!", new Font(new FontFamily("Arial"), 20, FontStyle.Bold), new SolidBrush(Color.Black), new Point(180, 220));
                    gscreen.DrawString("Введите своё имя:", new Font(new FontFamily("Arial"), 20, FontStyle.Bold), new SolidBrush(Color.Black), new Point(160, 250));
                }

                gform.DrawImage(screen, new Point(0, 0));
            }
            else if (menumsg.menuchoice == 6)
            {
                gscreen.DrawImage(plateimage, new Point(128, 210));
                gscreen.DrawString("Все данные удалятся!", new Font(new FontFamily("Arial"), 20, FontStyle.Bold), new SolidBrush(Color.Black), new Point(134, 220));
                gscreen.DrawString("Введите пароль:", new Font(new FontFamily("Arial"), 20, FontStyle.Bold), new SolidBrush(Color.Black), new Point(165, 250));
                gform.DrawImage(screen, new Point(0, 0));
            }
        }

        protected void Collision()
        {
            frog.GetCoordinates(msg);
            map.Collision(msg, ref menumsg.coins);
            if (msg.key == "DIE")
            {
                dead = true;
                if (!input.Visible && menu.leaders.Check(score))
                {
                    input.Text = null;
                    input.Show();
                    input.Focus();
                }
            }
            frog.Event(msg);
        }

        protected void Shrink()
        {
            dead = false;
            map.Shrink();
            frog.Shrink(img[menumsg.skinchoice]);
            score = 0;
        }

        protected void Play()
        {
            if (menumsg.menuchoice == 1 && menumsg.activegame == true)
            {
                map.Move();
                Collision();
            }
            else if (menumsg.menuchoice == 1 && menumsg.activegame == false)
            {
                menumsg.activegame = true;
                Shrink();
            }
            Draw();

            if (menumsg.menuchoice == 6 && !input.Visible)
            {
                menu.Hide();
                input.Text = null;
                input.Show();
                input.Focus();
                form.Refresh();
            }
            if (menumsg.menuchoice == 5)
            {
                form.Close();
            }
        }

        protected void Event(string str)
        {
            if (dead == false && (str == "KeyUp" || str == "KeyDown" || str == "KeyLeft" || str == "KeyRight"))
            {
                frog.GetCoordinates(msg);
                msg.key = str;
                map.CheckForMove(ref msg);
                if (msg.key != "STOP")
                {
                    frog.GetCoordinates(msg);
                    if (msg.roundy == 6 && str == "KeyUp")
                    {
                        score++;
                        map.Update();
                        frog.Round();
                        frog.Direction();
                    }
                    else
                    {
                        msg.key = str;
                        frog.Event(msg);
                    }
                }
            }
            else if (menumsg.menuchoice == 6 && input.Visible && str == "Enter")
            {
                if (input.Text == "stimsly")
                {
                    menumsg.coins = 0;
                    filebuf[0] = Convert.ToString(menumsg.coins);
                    menu.leaders.DeleteBase();
                    menu.shop.DeleteBase();
                    filebuf[0] = Convert.ToString(menumsg.coins);
                    string[] costs = menu.GetCosts();
                    string[] leaders = menu.GetLeaders();
                    for (int i = 1; i < 9; i++) filebuf[i] = costs[i - 1];
                    for (int i = 9; i < 28; i++) filebuf[i] = leaders[i - 9];
                    filebuf[28] = "0";
                    File.WriteAllLines("data.txt", filebuf);
                    menumsg.menuchoice = 0;
                    form.Refresh();
                    menumsg.activegame = false;
                    menumsg.skinchoice = 0;
                    input.Hide();
                    menu.SetFocus();
                    Shrink();
                }
                else
                {
                    input.Text = "Неверно!";
                }
            }
            else if (menumsg.menuchoice == 6 && input.Visible && str == "Escape")
            {
                input.Hide();
                menu.SetFocus();
                if (menumsg.activegame == true) menumsg.menuchoice = 1;
                else
                {
                    menumsg.menuchoice = 0;
                }
                form.Refresh();
            }
            else if (!input.Visible && dead == true && str == "Space")
            {
                Shrink();
            }
            else if (input.Visible && str == "Enter")
            {
                menu.leaders.Add(input.Text, score);
                input.Hide();
                menu.SetFocus();
                Shrink();
            }
            else if (str == "Escape" && menumsg.menuchoice == 0 && menumsg.activegame == true && !t2.Enabled)
            {
                menu.Hide();
                menumsg.menuchoice = 1;
            }
            else if (str == "Escape" && menumsg.menuchoice == 1 && !t2.Enabled)
            {
                menumsg.menuchoice = 0;
                form.Refresh();
                menumsg.activegame = true;
            }
            else if (str == "Escape" && menumsg.menuchoice == 0 && menumsg.activegame == false && !t2.Enabled)
            {
                menumsg.menuchoice = 5;
            }
        }
    }
}
