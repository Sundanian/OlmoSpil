using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace OlmoSpil
{
    class Turret : GameObject
    {
        private int id;
        private Stopwatch ballTimer = new Stopwatch();
        private static Random rnd = new Random();
        private Random r = new Random ();
        int rndSpawn;
        int rXPos = rnd.Next();
        int rYPos = rnd.Next();

        bool ballSpawned = false;

        public Turret(Vector2 position, int frames, int id)
            : base(position, frames)
        {
            this.id = id;
        }


        public override void Loadcontent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites/Turret.png");
            base.Loadcontent(content);
        }

        public override void OnCollision(GameObject other)
        {

        }

        protected override void CreateAnimations(Texture2D texture)
        {
            CreateAnimation("idle", 1, 0, 0, 40, 40, Vector2.Zero, 1);
            PlayAnimation("idle");
        }

        public override void Update(GameTime gameTime)
        {
            ballTimer.Start();
            ballSpawn();
            ballSpawned = false;
        }

        private bool spawned;

        private void ballSpawn()
        {

            if (ballTimer.ElapsedMilliseconds > 1000 && ballSpawned == false && spawned == false)
            {
                ballSpawned = true;
                ballTimer.Reset();
                if (ballSpawned == true)
                {
                  rndSpawn = r.Next(1, 5);

                }

                switch (rndSpawn)
                {

                    case 1:
                        if (this.id == 1 && spawned == false)
                        {
                            rXPos = rnd.Next(5, 35);
                            rYPos = rnd.Next(15, 25);
                            Game1.AddObjects.Add(new Ball(new Vector2(this.position.X + rXPos, this.position.Y + rYPos), 1));
                            spawned = true;
                        }
                        break;
                    case 2:
                        if (this.id == 2 && spawned == false)
                        {
                            rXPos = rnd.Next(-13, 2);
                            rYPos = rnd.Next(15, 25);
                            Game1.AddObjects.Add(new Ball(new Vector2(this.position.X + rXPos, this.position.Y + rYPos), 1));
                            spawned = true;
                        }
                        break;
                    case 3:
                        if (this.id == 3 && spawned == false)
                        {
                            rXPos = rnd.Next(5, 35);
                            rYPos = rnd.Next(-15, 5);
                            Game1.AddObjects.Add(new Ball(new Vector2(this.position.X + rXPos, this.position.Y + rYPos), 1));
                            spawned = true;
                        }
                        break;
                    case 4:
                        if (this.id == 4 && spawned == false)
                        {
                            rXPos = rnd.Next(-13, 2);
                            rYPos = rnd.Next(-15, 5);
                            Game1.AddObjects.Add(new Ball(new Vector2(this.position.X + rXPos, this.position.Y + rYPos), 1));
                            spawned = true;
                        }
                        break;
                    default:
                        break;
                }

                
                spawned = false;
            }
        }
    }
}
