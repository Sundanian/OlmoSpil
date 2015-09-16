using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OlmoSpil
{
    public enum PlayerId { Player1 = 1, Player2 = 2, Player3 = 3, Player4 = 4, Player5 = 5, Player6 = 6 }

    class Player : GameObject
    {
        private string name;
        private int life;
        private int team;
        private float duration = 50f; //Duration of the power up
        private bool powerOn = false; // If the player has a powerUp, default False;

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
        public int Team
        {
            get
            {
                return team;
            }
        }


        public Player(Vector2 position, int frames, string name, float speed, PlayerId playerId, int team) : base(position, frames)
        {
            this.name = name;
            this.life = 5;
            //this.speed = speed;
            this.playerId = playerId;
            this.team = team;
        }
        public override void Loadcontent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites/Player_Blue.png");
            base.Loadcontent(content);
        }
        public override void OnCollision(GameObject other)
        {
            if (other is Ball)
            {
                lastBallToHit = (Ball)other;
                
                //Virker ikke... Den skal have fat i boldens'state, hvor der er aktiveret Deadball på den
                // og denne if, skal så se om den state er på, for at gøre noget
                if (powerOn == true && powerUp == PowerType.StunBall)
                {
                    life = life - 1;
                    // Awesome Death animation, with explosions, fire, and more explosions
                }
            }
        }
        protected override void CreateAnimations(Texture2D texture)
        {
            CreateAnimation("idle", 1, 0, 0, 32, 32, Vector2.Zero, 1);
            PlayAnimation("idle");
        }

        public override void Update(GameTime gameTime)
        {
            if (lastBallToHit != null)
            {
                lastBallToHit.LastPlayerToHit = this;
            }
            #region Powerup Conditions
            if (powerOn == true) // If a PowerUp is collected
            {
                if (powerUp == PowerType.Speed)
                {
                    if (duration <= 0) // When the durations hits 0
                    {
                        speed = 100; // Resets the player's speed to 100
                        powerOn = false; // resets the player's powerOn to false
                    }
                    else // Effect, while the duration is not 0
                    {
                        if (duration == 50) // Only affects the player ONE time, when the duration is at max
                        {
                            speed = speed + 50; // Increases the Player's speed with 50
                        }
                        duration -= 0.2f; // How fast the duration depletes
                    }
                }
                if (powerUp == PowerType.StunBall)
                {
                    if (duration <= 0)
                    {
                        powerOn = false; 
                    }
                    else
                    {
                        if (duration == 50)
                        {
                            //Indsæt kode for, hvad der sker når bolden første gang bliver ramt af spilleren
                        }
                        duration -= 0.2f;
                    }
                }
                if (powerUp == PowerType.StickyBall)
                {
                    if (duration <= 0)
                    {
                        powerOn = false;
                    }
                    else
                    {
                        duration -= 0.2f;
                    }
                }
            }
            #endregion
			position += velocity;
            HandleInput(Keyboard.GetState());
            base.Update(gameTime);
        }
		private void HandleInput(KeyboardState keystate)
        {
            velocity = Vector2.Zero;
            if (keystate.IsKeyDown(Keys.Left))
            {
                velocity.X = -3f;
            }
            if (keystate.IsKeyDown(Keys.Right))
            {
                velocity.X = 3f;
            }
            if (keystate.IsKeyDown(Keys.Up))
            {
                velocity.Y = -3f;
            }
            if (keystate.IsKeyDown(Keys.Down))
            {
                velocity.Y = 3f;
            }
            if (keystate.IsKeyDown(Keys.Space)) //Shoot
            {

            }
            if (keystate.IsKeyDown(Keys.C)) //Pwup
            {

            }
        }
    }
}
