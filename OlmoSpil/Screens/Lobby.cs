using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OlmoSpil
{
    class Lobby : Screen
    {
        string ip;
        public Lobby()
            : base()
        {
        }
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            Type = "Lobby";
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            KeyboardState keystate = Keyboard.GetState();
            //Ingen server funktionalitet endnu. Skal tilføjes!
            if (keystate.IsKeyDown(Keys.C))
            {
                Game1.RoomChange = true;
                Game1.currentScreen.Type = "InGame";
            }
            else if (keystate.IsKeyDown(Keys.J))
            {
                Game1.RoomChange = true;
                Game1.currentScreen.Type = "InGame";
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.Sf, "Press 'C' to create a server.\nPress 'J' to join a server.", Vector2.Zero, Color.Red);
        }
    }
}
