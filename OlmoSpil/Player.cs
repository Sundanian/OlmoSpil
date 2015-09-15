using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        public override void Loadcontent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites/Player_Blue.png");
            base.Loadcontent(content);
        }
        public override void OnCollision(GameObject other)
        {
        }
        protected override void CreateAnimations(Texture2D texture)
        {
            CreateAnimation("idle", 1, 0, 0, 32, 32, Vector2.Zero, 1);
            PlayAnimation("idle");
        }
        public override void Update(GameTime gameTime)
        {
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
