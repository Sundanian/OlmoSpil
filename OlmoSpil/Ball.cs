using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OlmoSpil
{
    class Ball : GameObject
    {
        public Ball(Vector2 position, int frames)
            : base(position, frames)
        {

        }
        public override void OnCollision(GameObject other)
        {
        }
    }
}
