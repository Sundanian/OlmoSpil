using Microsoft.Xna.Framework;
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
        private int timer = 10;
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
            //Center
            Post g = new Post(new Vector2(Game1.Graphics.GraphicsDevice.Viewport.Width / 2 - 20, Game1.Graphics.GraphicsDevice.Viewport.Height / 2 - 20), 1);
            Game1.AddObjects.Add(g);

            g = new Post(new Vector2(Game1.Graphics.GraphicsDevice.Viewport.Width / 2 - 20 - 200, Game1.Graphics.GraphicsDevice.Viewport.Height / 2 - 20 - 200), 1);
            Game1.AddObjects.Add(g);
            g = new Post(new Vector2(Game1.Graphics.GraphicsDevice.Viewport.Width / 2 - 20 + 200, Game1.Graphics.GraphicsDevice.Viewport.Height / 2 - 20 - 200), 1);
            Game1.AddObjects.Add(g);
            g = new Post(new Vector2(Game1.Graphics.GraphicsDevice.Viewport.Width / 2 - 20 - 200, Game1.Graphics.GraphicsDevice.Viewport.Height / 2 - 20 + 200), 1);
            Game1.AddObjects.Add(g);
            g = new Post(new Vector2(Game1.Graphics.GraphicsDevice.Viewport.Width / 2 - 20 + 200, Game1.Graphics.GraphicsDevice.Viewport.Height / 2 - 20 + 200), 1);
            Game1.AddObjects.Add(g);
            #endregion
            base.LoadContent(content);
        }
        public override void Update(GameTime gameTime)
        {
            SpawnPowerUp();
            SpawnBalls();
            base.Update(gameTime);
        }
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
        public void SpawnPowerUp()
        {
            if (powerUpSpawned == false) //If there is NOT a powerUp on the field
            {
                int x = rnd.Next(0, 1000);
                if (x <= 10)
                {
                    int choosePower = rnd.Next(1, 4);
                    float xCoord = rnd.Next(100, 250); // Should be set to be within the screen/Level
                    float yCoord = rnd.Next(100, 250); // Should be set to be within the screen/Level
                    Vector2 position1 = new Vector2(xCoord, yCoord);
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
                        case 4:
                            {
                                Game1.AddObjects.Add(new PowerUp(@"PowerUp_DeadBall_Skull.png", position1, 1, PowerType.StunBall));
                                powerUpSpawned = true;
                            }
                            break;
                        default:
                            break;
                    }
                }

            }
        }
        private void SpawnBalls()
        {
            if (timer <= 0)
            {
                int x = +rnd.Next(-1, 2);
                int y = +rnd.Next(-1, 2);
                do
                {
                    x = +rnd.Next(-1, 2);
                    y = +rnd.Next(-1, 2);
                } while (x == 0 || y == 0);
                Ball ball = new Ball(new Vector2(Game1.Graphics.GraphicsDevice.Viewport.Width / 2 - 16 + x, Game1.Graphics.GraphicsDevice.Viewport.Height / 2 - 16 + y), 1);
                Game1.AddObjects.Add(ball);
                timer = 1000;
            }
            timer--;
        }
    }
}
