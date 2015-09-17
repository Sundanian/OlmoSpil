using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OlmoSpil
{
    class InGame : Screen
    {
        private static bool powerUpSpawned = false;
        private static Random rnd = new Random();
        private bool win;
        public static bool PowerUpSpawned
        {
            get { return InGame.powerUpSpawned; }
            set { InGame.powerUpSpawned = value; }
        }
        public InGame()
            : base()
        {

        }
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            Type = "InGame";

            Player player1 = new Player(new Vector2(Game1.Graphics.GraphicsDevice.Viewport.Width / 2 - 30, Game1.Graphics.GraphicsDevice.Viewport.Height / 2 - 16 + 175), 1, "Bob", 1, PlayerId.Player1, 1);
            Player player2 = new Player(new Vector2(140, Game1.Graphics.GraphicsDevice.Viewport.Height / 2 - 50), 1, "Bob", 1, PlayerId.Player2, 4);
            Player player3 = new Player(new Vector2(350, -25), 1, "Bob", 1, PlayerId.Player3, 3);
            Player player4 = new Player(new Vector2(570, Game1.Graphics.GraphicsDevice.Viewport.Height / 2 - 50), 1, "Bob", 1, PlayerId.Player4, 2);
            Game1.AddObjects.Add(player1);
            Game1.AddObjects.Add(player2);
            Game1.AddObjects.Add(player3);
            Game1.AddObjects.Add(player4);
            #region Level
            Turret g = new Turret(new Vector2(Game1.Graphics.GraphicsDevice.Viewport.Width / 2 - 20 - 200, Game1.Graphics.GraphicsDevice.Viewport.Height / 2 - 20 - 200), 1, 1);
            Game1.AddObjects.Add(g);
            g = new Turret(new Vector2(Game1.Graphics.GraphicsDevice.Viewport.Width / 2 - 20 + 200, Game1.Graphics.GraphicsDevice.Viewport.Height / 2 - 20 - 200), 1, 2);
            Game1.AddObjects.Add(g);
            g = new Turret(new Vector2(Game1.Graphics.GraphicsDevice.Viewport.Width / 2 - 20 - 200, Game1.Graphics.GraphicsDevice.Viewport.Height / 2 - 20 + 200), 1, 3);
            Game1.AddObjects.Add(g);
            g = new Turret(new Vector2(Game1.Graphics.GraphicsDevice.Viewport.Width / 2 - 20 + 200, Game1.Graphics.GraphicsDevice.Viewport.Height / 2 - 20 + 200), 1, 4);
            Game1.AddObjects.Add(g);
            #endregion
            base.LoadContent(content);
        }
        public override void Update(GameTime gameTime)
        {
            int tmp = 0;
            foreach (GameObject go in Game1.AllObjects)
            {
                if (go is Player)
                {
                    tmp++;
                }
            }
            if (tmp == 1)
            {
                win = true;
            }
            if (win)
            {
                KeyboardState keystate = Keyboard.GetState();
                if (keystate.IsKeyDown(Keys.Escape) || keystate.IsKeyDown(Keys.Back))
                {
                    Game1.RoomChange = true;
                    Game1.currentScreen.Type = "Lobby";
                }
            }
            SpawnPowerUp();
            base.Update(gameTime);
        }
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            if (win)
            {
                int tmp = 0;
                foreach (GameObject go in Game1.AllObjects)
                {
                    if (go is Player)
                    {
                        tmp = (go as Player).Team;
                    }
                }
                spriteBatch.DrawString(Game1.Sf, "Player " + tmp + " Won the game! Press Esc to return to the lobby", Vector2.Zero, Color.Red);
            }
            base.Draw(spriteBatch);
        }
        public void SpawnPowerUp()
        {
            if (powerUpSpawned == false) //If there is NOT a powerUp on the field
            {
                int x = rnd.Next(0, 1000);
                if (x == 1)
                {
                    int choosePower = rnd.Next(1, 4);
                    int spawn = rnd.Next(1, 5);
                    Vector2 position1 = Vector2.Zero;
                    switch (spawn)
                    {
                        case 1:
                             position1 = new Vector2(Game1.Graphics.GraphicsDevice.Viewport.Width / 2 - 20 - 200+40 , Game1.Graphics.GraphicsDevice.Viewport.Height / 2 - 20 - 200+40);
                            break;
                        case 2:
                             position1 = new Vector2(Game1.Graphics.GraphicsDevice.Viewport.Width / 2 - 20 + 200-128, Game1.Graphics.GraphicsDevice.Viewport.Height / 2 - 20 - 200+40);
                            break;
                        case 3:
                             position1 = new Vector2(Game1.Graphics.GraphicsDevice.Viewport.Width / 2 - 20 - 200+40, Game1.Graphics.GraphicsDevice.Viewport.Height / 2 - 20 + 200-128);
                            break;
                        case 4:
                            position1 = new Vector2(Game1.Graphics.GraphicsDevice.Viewport.Width / 2 - 20 + 200-128, Game1.Graphics.GraphicsDevice.Viewport.Height / 2 - 20 + 200-128);
                            break;
                        default:
                            break;
                    }
                    switch (choosePower)
                    {

                        /*addObjects.add --> Adds a new GameObject
                         * new PowerUp(Path to the file, position where it will spawn, number of frames, PowerType)
                         * powerUpSpawned --> Set the bool to true, so only ONE powerUp can be in the field at a time
                         */
                        case 1:
                            {
                                Game1.AddObjects.Add(new PowerUp(@"PowerUp_Lightning.png", position1, 1, PowerType.Speed));
                                powerUpSpawned = true;
                            }
                            break;
                        case 2:
                            {
                                Game1.AddObjects.Add(new PowerUp(@"PowerUp_Heart.png", position1, 1, PowerType.StickyBall));
                                powerUpSpawned = true;
                            }
                            break;
                        case 3:
                            {
                                Game1.AddObjects.Add(new PowerUp(@"PowerUp_MultiBall.png", position1, 1, PowerType.MultiBall));
                                powerUpSpawned = true;
                            }
                            break;
                        default:
                            break;
                    }
                }

            }
        }
    }
}
