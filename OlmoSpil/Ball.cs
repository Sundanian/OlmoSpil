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
            if (other is Ball || other is Player || other is Post) // If the obj is Ball or Player
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
            CheckPositionForDelete();
            this.position.X += xSpeed;
            this.position.Y += ySpeed;
            base.Update(gameTime);
        }
       
        /// <summary>
        /// Removes a ball if its out of the screen
        /// </summary>
        private void CheckPositionForDelete()
        {
            if (this.position.X > Game1.Graphics.GraphicsDevice.Viewport.Width || this.position.X < -32 ||this.position.Y > Game1.Graphics.GraphicsDevice.Viewport.Height || this.position.Y < -32)
            {
                Game1.RemoveObjects.Add(this); 
            }
        }
    }
}
