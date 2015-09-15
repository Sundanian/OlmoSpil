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
            if (other is Ball || other is Player) // If the obj is Ball or Player
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
                float sizeFactor = 5 / hyp;

                //ajusts the x and y speed to the ball
                this.xSpeed = xDiff * sizeFactor;
                this.ySpeed = yDiff * sizeFactor;
            }
        }

        protected override void CreateAnimations(Texture2D texture)
        {
            CreateAnimation("idle", 1, 0, 0, 32, 32, Vector2.Zero, 1);
            PlayAnimation("idle");
        }
        public override void Update(GameTime gameTime)
        {
                if (xSpeed != 0)
                {
                    this.position.X += xSpeed;
                    if (xSpeed > 0)
                    {
                        //for at farten ikke ender med aldrig at blive 0
                        if (xSpeed < 0.09)
                        {
                            xSpeed -= xSpeed;
                        }
                        else
                        {
                            xSpeed -= 0.1f;
                        }
                    }
                    else
                    {
                        //for at farten ikke ender med aldrig at blive 0
                        if (xSpeed > -0.09)
                        {
                            xSpeed += xSpeed;
                        }
                        else
                        {
                            xSpeed += 0.1f;
                        }
                    }
                }
                if (ySpeed != 0)
                {
                    this.position.Y += ySpeed;
                    if (ySpeed > 0)
                    {
                        //for at farten ikke ender med aldrig at blive 0
                        if (ySpeed < 0.09)
                        {
                            ySpeed -= ySpeed;
                        }
                        else
                        {
                            ySpeed -= 0.1f;
                        }
                    }
                    else
                    {
                        //for at farten ikke ender med aldrig at blive 0
                        if (ySpeed > -0.09)
                        {
                            ySpeed += ySpeed;
                        }
                        else
                        {
                            ySpeed += 0.1f;
                        }
                    }
                }
            base.Update(gameTime);
        }
    }
}
