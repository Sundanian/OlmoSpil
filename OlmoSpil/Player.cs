﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace OlmoSpil
{
    public enum PlayerId { Player1 = 1, Player2 = 2, Player3 = 3, Player4 = 4, Player5 = 5, Player6 = 6 }

    class Player : GameObject
    {
        private Vector2 lastPosition;
        private string name;
        private int life;
        private int team;
        private float duration; //Duration of the power up
        private bool powerOn = false; // If the player has a powerUp, default False;
        private bool usedPower = true; // If the player has used his powerUp
        private bool powerStunned = false; // Checks if

        private PowerType powerUp; // Which powerUp it is
        private PlayerId playerId; // Which player
        private Ball lastBallToHit; // Is used in Ball-class

        internal Ball LastBallToHit
        {
            get { return lastBallToHit; }
            set { lastBallToHit = value; }
        }
        private static Random rnd = new Random();

        public PowerType PowerUp
        {
            get { return powerUp; }
            set { powerUp = value; }
        }
        public float Duration
        {
            get
            {
                return duration;
            }
            set
            {
                duration = value;
            }
        }
        public bool PowerOn
        {
            get { return PowerOn; }
            set { powerOn = value; }
        }
        public bool UsedPower
        {
            get { return usedPower; }
            set { usedPower = value; }
        }
        public int Team
        {
            get
            {
                return team;
            }
        }
        public int Life
        {
            get { return life; }
            set { life = value; }
        }
        public Player(Vector2 position, int frames, string name, float speed, PlayerId playerId, int team)
            : base(position, frames)
        {
            //Fag
            this.name = name;
            this.life = 20;
            this.playerId = playerId;
            this.team = team;
            this.position = position;
            this.speed = 1;
        }
        public override void Loadcontent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            if (playerId == PlayerId.Player1)
            {
                texture = content.Load<Texture2D>(@"Sprites/Slime_Player_Red.png");
            }
            else if (playerId == PlayerId.Player2)
            {
                texture = content.Load<Texture2D>(@"Sprites/Slime_Player_Blue.png");
            }
            else if (playerId == PlayerId.Player3)
            {
                texture = content.Load<Texture2D>(@"Sprites/Slime_Player_Green.png");
            }
            else if (playerId == PlayerId.Player4)
            {
                texture = content.Load<Texture2D>(@"Sprites/Slime_Player_Yellow.png");
            }

            base.Loadcontent(content);
        }
        public override void OnCollision(GameObject other)
        {
            if (other is PowerUp)
            {
                powerOn = true;
            }
            if (other is Turret)
            {
                this.position = lastPosition;
            }
            if (other is Ball)
            {
                lastBallToHit = (Ball)other; // Finds which ball was last hit
            }
        }
        protected override void CreateAnimations(Texture2D texture)
        {
            if (playerId == PlayerId.Player1)
            {
                CreateAnimation("idle", 1, 0, 0, 100, 50, Vector2.Zero, 1);
                PlayAnimation("idle");
            }
            else if (playerId == PlayerId.Player2)
            {
                CreateAnimation("idle", 1, 0, 0, 100, 100, Vector2.Zero, 1);
                PlayAnimation("idle");
            }
            else if (playerId == PlayerId.Player3)
            {
                CreateAnimation("idle", 1, 0, 0, 100, 100, Vector2.Zero, 1);
                PlayAnimation("idle");
            }
            else if (playerId == PlayerId.Player4)
            {
                CreateAnimation("idle", 1, 0, 0, 100, 100, Vector2.Zero, 1);
                PlayAnimation("idle");
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (playerId == PlayerId.Player1)
            {
                spriteBatch.DrawString(Game1.Sf, "Lives: " + life, new Vector2(this.position.X, this.position.Y - 32), Color.Red);
            }

            else if (playerId == PlayerId.Player2)
            {
                spriteBatch.DrawString(Game1.Sf, "Lives: " + life, new Vector2(this.position.X, this.position.Y - 32), Color.Red);
            }
            else if (playerId == PlayerId.Player3)
            {
                spriteBatch.DrawString(Game1.Sf, "Lives: " + life, new Vector2(this.position.X, this.position.Y + 110), Color.Red);
            }
            else if (playerId == PlayerId.Player4)
            {
                spriteBatch.DrawString(Game1.Sf, "Lives: " + life, new Vector2(this.position.X, this.position.Y - 32), Color.Red);
            }
            spriteBatch.DrawString(Game1.Sf, "Lives: " + life, new Vector2(this.position.X, this.position.Y - 32), Color.Red);
            base.Draw(spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
            CheckDeath();
            if (lastBallToHit != null)
            {
                lastBallToHit.LastPlayerToHit = this;
            }
            #region Powerup Conditions
            if (powerOn == true) // If a PowerUp is collected
            {
                if (usedPower == true)
                {
                    if (powerUp == PowerType.Speed)
                    {
                        speed = 2; // Increases the Player's speed
                    }
                    if (powerUp == PowerType.StickyBall)
                    {
                        //Release
                        powerUp = PowerType.None;
                    }
                    if (powerUp == PowerType.MultiBall)
                    {
                        //Spawn flere bolde
                        for (int i = 0; i < 3; i++)
                        {
                            Game1.AddObjects.Add(new Ball(new Vector2(Game1.Graphics.GraphicsDevice.Viewport.Width / 2 - 16, Game1.Graphics.GraphicsDevice.Viewport.Height / 2 - 16), 1));
                        }
                        powerUp = PowerType.None;
                    }
                }
                switch (powerUp)
                {
                    case PowerType.Speed:
                        if (duration <= 0) // When the durations hits 0
                        {
                            speed = 1; // Resets the player's speed to 100
                            powerOn = false; // resets the player's powerOn to false
                        }
                        else if (duration > 0)
                        {
                            duration -= 0.2f; // How fast the duration depletes
                        }
                        break;
                    case PowerType.StickyBall:
                        if (duration <= 0) // When the durations hits 0
                        {
                            powerOn = false; // resets the player's powerOn to false
                        }
                        else if (duration > 0)
                        {
                            duration -= 0.2f; // How fast the duration depletes
                        }
                        break;
                    case PowerType.MultiBall:
                        break;
                    default:
                        break;
                }
            }
            #endregion
            position += velocity;
            HandleInput(Keyboard.GetState());
            base.Update(gameTime);
            lastPosition = position;
        }
        private void HandleInput(KeyboardState keystate)
        {
            velocity = Vector2.Zero;
            if (!powerStunned)
            {
                if (playerId == PlayerId.Player1)
                {
                    if (keystate.IsKeyDown(Keys.Left))
                    {
                        velocity.X = -5f * speed;
                    }
                    if (keystate.IsKeyDown(Keys.Right))
                    {
                        velocity.X = 5f * speed;
                    }
                }
                if (playerId == PlayerId.Player2)
                {
                    if (keystate.IsKeyDown(Keys.Left))
                    {
                        velocity.Y = -5f * speed;
                    }
                    if (keystate.IsKeyDown(Keys.Right))
                    {
                        velocity.Y = 5f * speed;
                    }
                }
                if (playerId == PlayerId.Player3)
                {
                    if (keystate.IsKeyDown(Keys.Left))
                    {
                        velocity.X = -5f * speed;
                    }
                    if (keystate.IsKeyDown(Keys.Right))
                    {
                        velocity.X = 5f * speed;
                    }
                }
                if (playerId == PlayerId.Player4)
                {
                    if (keystate.IsKeyDown(Keys.Left))
                    {
                        velocity.Y = -5f * speed;
                    }
                    if (keystate.IsKeyDown(Keys.Right))
                    {
                        velocity.Y = 5f * speed;
                    }
                }

                if (keystate.IsKeyDown(Keys.Space)) //Shoot
                {

                }
                if (keystate.IsKeyDown(Keys.C)) //Pwup
                {
                    UsedPower = true;
                }
                else
                {
                    UsedPower = false;
                }
            }
            else
            {
                if (duration <= 0)
                {
                    powerStunned = false;
                    duration = 50;
                }
                else
                {
                    duration -= 0.3f;
                }
            }
        }
        private void CheckDeath()
        {
            if (life <= 0)
            {
                //Block my side
                switch (team)
                {
                    case 1:
                        for (int i = 0; i < 9; i++)
                        {
                            Turret g = new Turret(new Vector2(Game1.Graphics.GraphicsDevice.Viewport.Width / 2 - 180 + (40 * i), Game1.Graphics.GraphicsDevice.Viewport.Height / 2 - 20 + 200), 1, 5);
                            Game1.AddObjects.Add(g);
                        }
                        break;
                    case 2:
                        for (int i = 0; i < 9; i++)
                        {
                            Turret g = new Turret(new Vector2(Game1.Graphics.GraphicsDevice.Viewport.Width / 2 - 20 + 200, Game1.Graphics.GraphicsDevice.Viewport.Height / 2 - 180 + (40 * i)), 1, 5);
                            Game1.AddObjects.Add(g);
                        }

                        break;
                    case 3:
                        for (int i = 0; i < 9; i++)
                        {
                            Turret g = new Turret(new Vector2(Game1.Graphics.GraphicsDevice.Viewport.Width / 2 - 180 + (40 * i), Game1.Graphics.GraphicsDevice.Viewport.Height / 2 - 20 - 200), 1, 5);
                            Game1.AddObjects.Add(g);
                        }
                        break;
                    case 4:
                        for (int i = 0; i < 9; i++)
                        {
                            Turret g = new Turret(new Vector2(Game1.Graphics.GraphicsDevice.Viewport.Width / 2 - 20 - 200, Game1.Graphics.GraphicsDevice.Viewport.Height / 2 - 180 + (40 * i)), 1, 5);
                            Game1.AddObjects.Add(g);
                        }
                        break;
                    default:
                        break;
                }
                //Sent "I am dead" to server

                //Removes this
                Game1.RemoveObjects.Add(this);
            }
        }
    }
}
