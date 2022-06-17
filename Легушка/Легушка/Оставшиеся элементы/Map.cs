using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Легушка
{
    [Serializable]
    class Map
    {
        protected Block[,] blockmap;
        protected Item[,] itemmap;
        protected List<Moving_Item> moving_items;
        protected List<int[]> templates;

        [NonSerialized]
        Bitmap screen;
        int[,] startmap;
        [NonSerialized]
        Random rnd;
        protected bool direction;
        protected int car_type;

        [NonSerialized]
        Image earthimage;
        [NonSerialized]
        Image waterimage;
        [NonSerialized]
        Image treeimage;
        [NonSerialized]
        Image lilyimage;
        [NonSerialized]
        Image roadimage;
        [NonSerialized]
        Image trainroadimage;
        [NonSerialized]
        Image redtrainroadimage;
        [NonSerialized]
        Image carimage;
        [NonSerialized]
        Image logimage;
        [NonSerialized]
        Image truckimage;
        [NonSerialized]
        Image trainimage;
        [NonSerialized]
        ImageList coinlist;
        [NonSerialized]
        Rectangle r;
        Message msg;
        int index;

        public Map(Form1 form, Bitmap screen)
        {
            rnd = new Random();
            startmap = new int[12, 9]
            {
                { 0,0,0,0,0,0,0,0,0 },
                { 0,1,0,0,0,0,1,0,0 },
                { 0,1,0,0,1,0,0,0,0 },
                { 0,1,0,0,0,0,0,0,0 },
                { 0,0,1,0,0,1,0,1,0 },
                { 0,0,1,0,0,0,0,0,0 },
                { 0,1,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,1,0,0,0,1,0 },
                { 0,0,0,0,0,1,0,0,0 },
                { 0,1,0,1,0,0,0,1,0 },
                { 1,0,0,0,1,0,1,0,1 }
            };

            this.screen = screen;
            moving_items = new List<Moving_Item>();
            blockmap = new Block[startmap.GetLength(0), startmap.GetLength(1)];
            itemmap = new Item[startmap.GetLength(0), startmap.GetLength(1)];
            templates = new List<int[]>();

            earthimage = Properties.Resources.earth;
            treeimage = Properties.Resources.tree;
            waterimage = Properties.Resources.water;
            lilyimage = Properties.Resources.lily;
            roadimage = Properties.Resources.road;
            trainroadimage = Properties.Resources.trainroad;
            redtrainroadimage = Properties.Resources.redtrainroad;
            carimage = Properties.Resources.car;
            logimage = Properties.Resources.log;
            truckimage = Properties.Resources.truck;
            trainimage = Properties.Resources.train;
            coinlist = new ImageList();
            coinlist.ImageSize = new Size(64, 64);
            coinlist.Images.Add(Properties.Resources.coin);
            coinlist.Images.Add(Properties.Resources.coin1);
            coinlist.Images.Add(Properties.Resources.coin2);
            coinlist.Images.Add(Properties.Resources.coin1);
            r = new Rectangle();
            msg = new Message();

            templates.Add(new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            templates.Add(new int[9] { 6, 0, 0, 0, 0, 0, 0, 0, 0 });
            templates.Add(new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 6 });
            templates.Add(new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            templates.Add(new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 });

            templates.Add(new int[9] { 0, 1, 0, 0, 0, 1, 0, 0, 0 });
            templates.Add(new int[9] { 0, 0, 0, 1, 0, 0, 1, 0, 0 });
            templates.Add(new int[9] { 0, 0, 1, 0, 0, 0, 0, 1, 0 });
            templates.Add(new int[9] { 6, 1, 0, 0, 0, 1, 0, 0, 1 });
            templates.Add(new int[9] { 1, 0, 0, 1, 0, 0, 1, 0, 6 });
            templates.Add(new int[9] { 0, 0, 1, 0, 1, 0, 0, 1, 0 });

            templates.Add(new int[9] { 2, 2, 2, 2, 2, 2, 2, 2, 2 });
            templates.Add(new int[9] { 7, 2, 2, 2, 2, 2, 2, 2, 2 });
            templates.Add(new int[9] { 2, 2, 2, 2, 2, 2, 2, 2, 7 });
            templates.Add(new int[9] { 2, 2, 2, 2, 2, 2, 2, 2, 2 });
            templates.Add(new int[9] { 2, 2, 2, 2, 2, 2, 2, 2, 2 });

            templates.Add(new int[9] { 2, 3, 2, 3, 2, 3, 3, 2, 3 });
            templates.Add(new int[9] { 3, 3, 2, 3, 2, 2, 3, 2, 2 });
            templates.Add(new int[9] { 2, 3, 3, 3, 3, 2, 3, 3, 2 });

            templates.Add(new int[9] { 4, 4, 4, 4, 4, 4, 4, 4, 4 });
            templates.Add(new int[9] { 4, 8, 4, 4, 4, 4, 4, 4, 4 });
            templates.Add(new int[9] { 4, 4, 4, 4, 4, 4, 4, 8, 4 });
            templates.Add(new int[9] { 4, 4, 4, 4, 4, 4, 4, 4, 4 });
            templates.Add(new int[9] { 4, 4, 4, 4, 4, 4, 4, 4, 4 });

            templates.Add(new int[9] { 5, 5, 5, 5, 5, 5, 5, 5, 5 });
            templates.Add(new int[9] { 5, 9, 5, 5, 5, 5, 5, 5, 5 });
            templates.Add(new int[9] { 5, 5, 5, 5, 5, 5, 5, 9, 5 });
            templates.Add(new int[9] { 5, 5, 5, 5, 5, 5, 5, 5, 5 });
            templates.Add(new int[9] { 5, 5, 5, 5, 5, 5, 5, 5, 5 });

            Shrink();
        }

        public void Refresh(Form1 form, Bitmap screen)
        {
            this.screen = screen;

            rnd = new Random();
            earthimage = Properties.Resources.earth;
            treeimage = Properties.Resources.tree;
            waterimage = Properties.Resources.water;
            lilyimage = Properties.Resources.lily;
            roadimage = Properties.Resources.road;
            trainroadimage = Properties.Resources.trainroad;
            redtrainroadimage = Properties.Resources.redtrainroad;
            carimage = Properties.Resources.car;
            logimage = Properties.Resources.log;
            truckimage = Properties.Resources.truck;
            trainimage = Properties.Resources.train;
            coinlist = new ImageList();
            coinlist.ImageSize = new Size(64, 64);
            coinlist.Images.Add(Properties.Resources.coin);
            coinlist.Images.Add(Properties.Resources.coin1);
            coinlist.Images.Add(Properties.Resources.coin2);
            coinlist.Images.Add(Properties.Resources.coin1);
            r = new Rectangle();
            msg = new Message();

            for (int i = 0; i < startmap.GetLength(0); i++)
            {
                for (int j = 0; j < startmap.GetLength(1); j++)
                {
                    blockmap[i, j].Collision(msg);
                    if (msg.key == "E") blockmap[i, j].Refresh(earthimage, screen);
                    else if (msg.key == "R") blockmap[i, j].Refresh(roadimage, screen);
                    else if (msg.key == "T") blockmap[i, j].Refresh(trainroadimage, screen);
                    else if (msg.key == "DIE") blockmap[i, j].Refresh(waterimage, screen);
                    if (itemmap[i, j] != null)
                    {
                        itemmap[i, j].Collision(msg);
                        if (msg.key == "COIN") itemmap[i, j].Refresh(coinlist.Images[0], screen);
                        else if (msg.key == "STOP") itemmap[i, j].Refresh(treeimage, screen);
                        else if (msg.key == "") itemmap[i, j].Refresh(lilyimage, screen);
                    }
                }
            }
            for (int i = 0; i < moving_items.Count; i++) moving_items[i].Refresh(treeimage, screen);
        }

        public void Shrink()
        {
            moving_items.Clear();
            for (int i = 0; i < startmap.GetLength(0); i++)
            {
                for (int j = 0; j < startmap.GetLength(1); j++)
                {
                    blockmap[i, j] = null;
                    itemmap[i, j] = null;
                    if (startmap[i, j] == 0)
                    {
                        blockmap[i, j] = new Earth(j, i, earthimage, screen);
                    }
                    else if (startmap[i, j] == 1)
                    {
                        itemmap[i, j] = new Tree(j, i, treeimage, screen);
                        blockmap[i, j] = new Earth(j, i, earthimage, screen);
                    }
                }
            }
        }

        public void Collision(Message msg, ref int coins)
        {
            if (msg.roundx > 8 || msg.roundx < 0 || msg.roundy > 11)
            {
                msg.key = "DIE";
            }
            else
            {
                blockmap[msg.roundy, msg.roundx].Collision(msg);
                if (itemmap[msg.roundy, msg.roundx] != null) itemmap[msg.roundy, msg.roundx].Collision(msg);
                if (msg.key == "COIN")
                {
                    coins++;
                    itemmap[msg.roundy, msg.roundx] = null;
                }
                for (int i = 0; i < moving_items.Count; i++) moving_items[i].Collision(msg);
            }
        }

        public void DrawMoving()
        {
            for (int i = 0; i < moving_items.Count; i++) moving_items[i].Draw();
        }

        public void Draw()
        {
            for (int i = 0; i < blockmap.GetLength(0); i++)
            {
                for (int j = 0; j < blockmap.GetLength(1); j++)
                {
                    blockmap[i, j].Draw();
                }
            }
            DrawMoving();
            for (int i = 0; i < blockmap.GetLength(0); i++)
            {
                for (int j = 0; j < blockmap.GetLength(1); j++)
                {
                    if (itemmap[i, j] != null) itemmap[i, j].Draw();
                }
            }
        }

        public void Update()
        {
            for (int i = blockmap.GetLength(0) - 1; i > 0; i--)
            {
                for (int j = 0; j < blockmap.GetLength(1); j++)
                {

                    blockmap[i, j] = blockmap[i - 1, j];
                    blockmap[i, j].Update();
                    itemmap[i, j] = itemmap[i - 1, j];
                    if (itemmap[i, j] != null) itemmap[i, j].Update();
                }
            }
            for (int i = 0; i < moving_items.Count; i++) moving_items[i].Update();

            GetTemplate();
        }

        public void Move()
        {
            for (int i = 0; i < moving_items.Count; i++)
            {
                for (int j = i + 1; j < moving_items.Count; j++)
                {
                    if (moving_items[i].GetY() == moving_items[j].GetY() && moving_items[i].GetSpeed() < moving_items[j].GetSpeed())
                    {
                        r = moving_items[i].GetRect();
                        r.Intersect(moving_items[j].GetRect());
                        if (!r.IsEmpty) moving_items[j].ChangeSpeed(moving_items[i].GetSpeed());
                    }
                }
            }
            for (int i = 0; i < moving_items.Count; i++)
            {
                moving_items[i].Move();
                if (moving_items[i].GetY() > 704 || moving_items[i].GetX() > 576 || moving_items[i].GetX() < -512)
                    moving_items.RemoveAt(i);
            }

            for (int i = 0; i < blockmap.GetLength(0); i++)
            {
                direction = false;
                for (int j = 0; j < blockmap.GetLength(1); j++)
                {
                    if (itemmap[i, j] != null)
                    {
                        itemmap[i, j].Collision(msg);
                        if (msg.key == "" || msg.key == "STOP")
                        {
                            direction = true;
                            break;
                        }
                    }
                }
                if (direction) continue;
                car_type = 0;
                car_type = blockmap[i, 0].PlaceMoving(ref direction);
                if (car_type == 0) continue;
                if (car_type == 1)
                {
                    if (direction) moving_items.Add(new Log(-128, i * 64, logimage, screen, direction, rnd.Next(2) * 4 + rnd.Next(3) + 2, 128));
                    else moving_items.Add(new Log(576, i * 64, logimage, screen, direction, rnd.Next(2) * 4 + rnd.Next(3) + 2, 128));
                }
                else if (car_type == 2)
                {
                    if (direction) moving_items.Add(new Car(-64, i * 64, carimage, screen, direction, rnd.Next(2) * 6 + 2, 64));
                    else moving_items.Add(new Car(576, i * 64, carimage, screen, direction, rnd.Next(2) * 6 + 2, 64));
                }
                else if (car_type == 3)
                {
                    if (direction) moving_items.Add(new Truck(-128, i * 64, truckimage, screen, direction, rnd.Next(2) * 6 + 2, 128));
                    else moving_items.Add(new Truck(576, i * 64, truckimage, screen, direction, rnd.Next(2) * 6 + 2, 128));
                }
                else if (car_type == 4)
                {
                    if (direction) moving_items.Add(new Train(-512, i * 64, trainimage, screen, direction, 32, 512));
                    else moving_items.Add(new Train(576, i * 64, trainimage, screen, direction, 32, 512));
                }
            }
        }

        protected void GetTemplate()
        {
            index = rnd.Next() % templates.Count;
            for (int i = 0; i < 9; i++)
            {
                blockmap[0, i] = null;
                itemmap[0, i] = null;
                if (templates[index][i] == 0)
                {
                    blockmap[0, i] = new Earth(i, 0, earthimage, screen);
                }
                else if (templates[index][i] == 1)
                {
                    itemmap[0, i] = new Tree(i, 0, treeimage, screen);
                    blockmap[0, i] = new Earth(i, 0, earthimage, screen);
                }
                else if (templates[index][i] == 2)
                {
                    blockmap[0, i] = new Water(i, 0, waterimage, screen, new Random().Next(2) == 1 ? true : false);
                }
                else if (templates[index][i] == 3)
                {
                    itemmap[0, i] = new Lily(i, 0, lilyimage, screen);
                    blockmap[0, i] = new Water(i, 0, waterimage, screen, new Random().Next(2) == 1 ? true : false);
                }
                else if (templates[index][i] == 4)
                {
                    blockmap[0, i] = new Road(i, 0, roadimage, screen, new Random().Next(2) == 1 ? true : false);
                }
                else if (templates[index][i] == 5)
                {
                    blockmap[0, i] = new Train_road(i, 0, trainroadimage, screen, new Random().Next(2) == 1 ? true : false, redtrainroadimage);
                }
                else if (templates[index][i] == 6)
                {
                    blockmap[0, i] = new Earth(i, 0, earthimage, screen);
                    itemmap[0, i] = new Coin(i, 0, coinlist.Images[0], screen, coinlist);
                }
                else if (templates[index][i] == 7)
                {
                    blockmap[0, i] = new Water(i, 0, waterimage, screen, new Random().Next(2) == 1 ? true : false);
                    itemmap[0, i] = new Coin(i, 0, coinlist.Images[0], screen, coinlist);
                }
                else if (templates[index][i] == 8)
                {
                    blockmap[0, i] = new Road(i, 0, roadimage, screen, new Random().Next(2) == 1 ? true : false);
                    itemmap[0, i] = new Coin(i, 0, coinlist.Images[0], screen, coinlist);
                }
                else if (templates[index][i] == 9)
                {
                    blockmap[0, i] = new Train_road(i, 0, trainroadimage, screen, new Random().Next(2) == 1 ? true : false, redtrainroadimage);
                    itemmap[0, i] = new Coin(i, 0, coinlist.Images[0], screen, coinlist);
                }
            }
        }

        public void CheckForMove(ref Message msg)
        {
            if (msg.key == "KeyUp") msg.roundy--;
            else if (msg.key == "KeyDown") msg.roundy++;
            else if (msg.key == "KeyLeft") msg.roundx--;
            else if (msg.key == "KeyRight") msg.roundx++;

            if (msg.roundx >= 0 && msg.roundx < blockmap.GetLength(1) && msg.roundy >= 0 && msg.roundy < blockmap.GetLength(0))
            {
                if (itemmap[msg.roundy, msg.roundx] != null) itemmap[msg.roundy, msg.roundx].Collision(msg);
                else msg.key = "";
            }
            else msg.key = "STOP";
        }
    }
}
