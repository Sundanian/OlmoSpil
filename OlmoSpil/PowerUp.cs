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
    public enum PowerType { Speed = 1, PowerShot = 2, StickyBall = 3, MultiBall = 4, DeadBall = 5 } // The different powerUps, and their variables
    class PowerUp : GameObject
    {
        #region Fields
        private PowerType powerType; // This variable contains the different Enum-Types, of the powerUps
        private static Random rnd = new Random();
        #endregion

        // PowerUp's constructor, with GameObject as its superclass
        public PowerUp(string imagePath, PointF startPos, Vector2 position, int frames, PowerType type) : base(position, frames)
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
                    case PowerType.PowerShot:
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

                        }
                        break;
                    case PowerType.DeadBall:
                        {

                        }
                        break;
                    default:
                        break;
                }
            }
        }


        #endregion

        protected override void CreateAnimations(Texture2D texture)
        {
        }
    }
}
