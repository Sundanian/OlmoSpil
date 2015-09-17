using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OlmoSpil
{
    class Ball : GameObject
    {
        private float xSpeed = 0;
        private float ySpeed = 0;
        private Player lastPlayerToHit;

        internal Player LastPlayerToHit
        {
            get { return lastPlayerToHit; }
            set { lastPlayerToHit = value; }
        }

        public Ball(Vector2 position, int frames)
            : base(position, frames)
        {
        }
        public override void Loadcontent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites/ball.png");
            base.Loadcontent(content);
        }
        public override void OnCollision(GameObject other)
        {
            if (other is Ball || other is Player || other is Turret) // If the obj is Ball or Player
            {
                if (this.position == other.Position)
                {
                    Random r = new Random();
                    this.position = this.position + new Vector2(r.Next(-9, 10), r.Next(-9, 10));
                }
                //x og y difference between ball and object
                float xDiff = (this.Position.X + this.CollisionCircle.Radius) - (other.Position.X + other.CollisionCircle.Radius);
                float yDiff = (this.Position.Y + this.CollisionCircle.Radius) - (other.Position.Y + other.CollisionCircle.Radius);

                float tempXDiff = xDiff;
                float tempYDiff = yDiff;

                //to work with positive numbers
                if (xDiff < 0)
                {
                    tempXDiff = -xDiff;
                }
                if (yDiff < 0)
                {
                    tempYDiff = -yDiff;
                }

                //a^2 + b^2
                double tempTotalDiff = tempXDiff * tempXDiff + tempYDiff * tempYDiff;

                //lenght of the hyp
                float hyp = (float)Math.Sqrt(tempTotalDiff);

                //Scale
                float sizeFactor = 6 / hyp;

                //ajusts the x and y speed to the ball
                this.xSpeed = xDiff * sizeFactor;
                this.ySpeed = yDiff * sizeFactor;
                this.xSpeed = xSpeed + 0.15f;
                this.ySpeed = ySpeed + 0.15f;
            }
        }

        protected override void CreateAnimations(Texture2D texture)
        {
            CreateAnimation("idle", 1, 0, 0, 32, 32, Vector2.Zero, 1);
            PlayAnimation("idle");
        }

        public override void Update(GameTime gameTime)
        {
            CheckForGoal();
            CheckPositionForDelete();
            // This makes the stickyBall-PowerUp Work
            if (lastPlayerToHit != null && lastPlayerToHit.PowerUp == PowerType.StickyBall && lastPlayerToHit.Duration > 0) //Finds the ball with number 1, should find the ball, which it collides with
            {
                switch (lastPlayerToHit.Team)
                {
                    case 1:
                        this.position.X = lastPlayerToHit.Position.X + 50-16;
                        this.position.Y = lastPlayerToHit.Position.Y - 32;
                        break;
                    case 2:
                        this.position.X = lastPlayerToHit.Position.X - 32;
                        this.position.Y = lastPlayerToHit.Position.Y + 50-16;
                        break;
                    case 3:
                        this.position.X = lastPlayerToHit.Position.X + 50-16;
                        this.position.Y = lastPlayerToHit.Position.Y + 100;
                        break;
                    case 4:
                        this.position.X = lastPlayerToHit.Position.X + 100;
                        this.position.Y = lastPlayerToHit.Position.Y + 50-16;
                        break;
                    default:
                        break;
                }
            }
            this.position.X += xSpeed;
            this.position.Y += ySpeed;
            base.Update(gameTime);
        }

        /// <summary>
        /// Removes a ball if its out of the screen
        /// </summary>
        private void CheckPositionForDelete()
        {
            if (this.position.X > Game1.Graphics.GraphicsDevice.Viewport.Width || this.position.X < -32 || this.position.Y > Game1.Graphics.GraphicsDevice.Viewport.Height || this.position.Y < -32)
            {
                Game1.RemoveObjects.Add(this);
            }
        }
        private void CheckForGoal()
        {
            //If this ball is outside the playing field
            if (this.position.X < Game1.Graphics.GraphicsDevice.Viewport.Width / 2 - 20 - 200) //West
            {
                foreach (GameObject go in Game1.AllObjects)
                {
                    if (go is Player && (go as Player).Team == 4)
                    {
                        (go as Player).Life -= 1;
                        Game1.RemoveObjects.Add(this);
                    }
                }
            }
            else if (this.position.Y < Game1.Graphics.GraphicsDevice.Viewport.Height / 2 - 20 - 200) //North
            {
                foreach (GameObject go in Game1.AllObjects)
                {
                    if (go is Player && (go as Player).Team == 3)
                    {
                        (go as Player).Life -= 1;
                        Game1.RemoveObjects.Add(this);
                    }
                }
            }
            else if (this.position.X > Game1.Graphics.GraphicsDevice.Viewport.Width / 2 + 260) //East
            {
                foreach (GameObject go in Game1.AllObjects)
                {
                    if (go is Player && (go as Player).Team == 2)
                    {
                        (go as Player).Life -= 1;
                        Game1.RemoveObjects.Add(this);
                    }
                }
            }
            else if (this.position.Y > Game1.Graphics.GraphicsDevice.Viewport.Height / 2 - 20 + 240) //South
            {
                foreach (GameObject go in Game1.AllObjects)
                {
                    if (go is Player && (go as Player).Team == 1)
                    {
                        (go as Player).Life -= 1;
                        Game1.RemoveObjects.Add(this);
                    }
                }
            }
        }
    }
}
