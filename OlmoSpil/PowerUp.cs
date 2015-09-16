using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace OlmoSpil
{
    public enum PowerType { Speed = 1, DeadBall = 2, StickyBall = 3, MultiBall = 4 } // The different powerUps, and their variables
    class PowerUp : GameObject
    {
        #region Fields
        private PowerType powerType; // This variable contains the different Enum-Types, of the powerUps
        private static Random rnd = new Random();
        #endregion

        // PowerUp's constructor, with GameObject as its superclass
        public PowerUp(string imagePath/*, PointF startPos*/, Vector2 position, int frames, PowerType type) : base(position, frames)
        {
            this.powerType = type;
        }

        #region Methoeds
        public override void OnCollision(GameObject other)
        {
            if (other is Player) // If the colliding GameObject is of the type, Player
            {

                switch (powerType) //Switch-case, for the different powerTypes
                {
                    case PowerType.Speed:
                        {
                            Player tempPlayer = (Player)other;
                            tempPlayer.PowerOn = true; // Sets the player's bool (powerOn) to true.
                            tempPlayer.PowerUp = this.powerType; // Sets this powerType to the Player
                            tempPlayer.Duration = 50f; // Sets the duration of the powerUp, to the player
                        }
                        break;
                    case PowerType.DeadBall:
                        {
                            Player tempPlayer = (Player)other;
                            tempPlayer.PowerOn = true; // Sets the player's bool (powerOn) to true.
                            tempPlayer.PowerUp = this.powerType; // Sets this powerType to the Player
                            tempPlayer.Duration = 50f; // Sets the duration of the powerUp, to the player
                        }
                        break;
                    case PowerType.StickyBall:
                        {
                            Player tempPlayer = (Player)other;
                            tempPlayer.PowerOn = true; // Sets the player's bool (powerOn) to true.
                            tempPlayer.PowerUp = this.powerType; // Sets this powerType to the Player
                            tempPlayer.Duration = 50f; // Sets the duration of the powerUp, to the player
                        }
                        break;
                    case PowerType.MultiBall:
                        {
                            Player tempPlayer = (Player)other;
                            tempPlayer.PowerOn = true; // Sets the player's bool (powerOn) to true.
                            tempPlayer.PowerUp = this.powerType; // Sets this powerType to the Player
                            tempPlayer.Duration = 50f; // Sets the duration of the powerUp, to the player
                        }
                        break;
                    default:
                        break;
                }
            }
            Game1.RemoveObjects.Add(this); // Removes the PowerUp, which is collected from the field
            Game1.PowerUpSpawned = false; // Resets the PowerUpSpawned, so it can spawn a new PowerUp

        }

        protected override void CreateAnimations(Texture2D texture)
        {
            CreateAnimation("idle", 1, 0, 0, 40, 40, Vector2.Zero, 1);
            PlayAnimation("idle");
        }

        #endregion
    }
}
