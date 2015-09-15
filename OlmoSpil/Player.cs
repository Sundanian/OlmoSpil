using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OlmoSpil
{
    public enum PlayerId { Player1 = 1, Player2 = 2, Player3 = 3, Player4 = 4, Player5 = 5, Player6 = 6 }

    class Player : GameObject
    {

        #region Fields

        private string name;
        private float speed;
        private int life;
        private int team;

        public int Team
        {
            get { return team; }
            set { team = value; }
        }
        private PlayerId playerId;
        private float duration;
        private bool powerOn = false;
        private PowerType powerUp;

        public PowerType PowerUp
        {
            get { return powerUp; }
            set { powerUp = value; }
        }
        public float Duration
        {
            get { return duration; }
            set { duration = value; }
        }
        public bool PowerOn
        {
            get { return powerOn; }
            set { powerOn = value; }
        }
        public int Life
        {
            get { return life; }
            set { life = value; }
        }

        #endregion

        public Player(Vector2 position, int frames) : base(position, frames)
        {

        }
        public override void OnCollision(GameObject other)
        {
        }

        protected override void CreateAnimations(Microsoft.Xna.Framework.Graphics.Texture2D texture)
        {
        }
    }
}
