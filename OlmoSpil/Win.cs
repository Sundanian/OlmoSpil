using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OlmoSpil
{
    class Win : GameObject
    {


        public Win(Vector2 position, int frames)
            : base(position, frames)
        {

        }

        public override void LoadContent(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {
            HandleInput(Keyboard.GetState());
        }
        private void HandleInput(KeyboardState keystate)
        {
            if (keystate.IsKeyDown(Keys.Escape) || keystate.IsKeyDown(Keys.Back))
            {
                Game1.RoomChange = true;
                Game1.currentScreen.Type = "Lobby";
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.Sf, "Player {0} Won the game! Press Esc to return to the lobby", new Vector2(Game1.Graphics.GraphicsDevice.Viewport.Width / 2, Game1.Graphics.GraphicsDevice.Viewport.Height / 2), Color.Red);
        }
    }
}
