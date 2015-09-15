using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OlmoSpil
{
    class Player : GameObject
    {
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
