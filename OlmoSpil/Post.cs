using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OlmoSpil
{
    class Post : GameObject
    {
        int type;
        public Post(Vector2 position, int frames)
            : base(position, frames)
        {
        }
        public override void Loadcontent(Microsoft.Xna.Framework.Content.ContentManager content)
        {

            texture = content.Load<Texture2D>(@"Sprites/Post.png");


            base.Loadcontent(content);
        }

        public override void OnCollision(GameObject other)
        {

        }

        protected override void CreateAnimations(Texture2D texture)
        {

            CreateAnimation("idle", 1, 0, 0, 40, 40, Vector2.Zero, 1);
            PlayAnimation("idle");


        }
    }
}
