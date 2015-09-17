using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace OlmoSpil
{
    //Fag!!!!
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private static SpriteFont sf;
        private static GraphicsDeviceManager graphics;

        public static GraphicsDeviceManager Graphics
        {
            get { return Game1.graphics; }
            set { Game1.graphics = value; }
        }
        SpriteBatch spriteBatch;
        private static List<GameObject> allObjects;
        private static List<GameObject> addObjects;
        private static List<GameObject> removeObjects;

        public static List<GameObject> RemoveObjects
        {
            get { return Game1.removeObjects; }
            set { Game1.removeObjects = value; }
        }
        public static List<GameObject> AddObjects
        {
            get { return Game1.addObjects; }
            set { Game1.addObjects = value; }
        }
        public static List<GameObject> AllObjects
        {
            get { return Game1.allObjects; }
            set { Game1.allObjects = value; }
        }

        private static bool powerUpSpawned = false;

        public static bool PowerUpSpawned
        {
            get { return Game1.powerUpSpawned; }
            set { Game1.powerUpSpawned = value; }
        }

        private static Random rnd = new Random();

        private int timer = 10;
        public static SpriteFont Sf
        {
            get { return sf; }
            set { sf = value; }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            allObjects = new List<GameObject>();
            addObjects = new List<GameObject>();
            removeObjects = new List<GameObject>();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Window.Title = "Pong for 6";

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            sf = Content.Load<SpriteFont>("SpriteFont");
            foreach (GameObject go in AllObjects)
            {
                go.Loadcontent(Content);
            }

            Player player1 = new Player(new Vector2(GraphicsDevice.Viewport.Width / 2 - 30, GraphicsDevice.Viewport.Height / 2 - 16 + 175), 1, "Bob", 1, PlayerId.Player1, 1);
            Player player2 = new Player(new Vector2(140, GraphicsDevice.Viewport.Height / 2 - 50), 1, "Bob", 1, PlayerId.Player2, 4);
            Player player3 = new Player(new Vector2(350, -25), 1, "Bob", 1, PlayerId.Player3, 3);
            Player player4 = new Player(new Vector2(570, GraphicsDevice.Viewport.Height / 2 - 50), 1, "Bob", 1, PlayerId.Player4, 2);
            AddObjects.Add(player1);
            AddObjects.Add(player2);
            AddObjects.Add(player3);
            AddObjects.Add(player4);
            #region Level
            //Center
            Post g = new Post(new Vector2(GraphicsDevice.Viewport.Width / 2 - 20, GraphicsDevice.Viewport.Height / 2 - 20), 1);
            AddObjects.Add(g);

            g = new Post(new Vector2(GraphicsDevice.Viewport.Width / 2 - 20 - 200, GraphicsDevice.Viewport.Height / 2 - 20 - 200), 1);
            AddObjects.Add(g);
            g = new Post(new Vector2(GraphicsDevice.Viewport.Width / 2 - 20 + 200, GraphicsDevice.Viewport.Height / 2 - 20 - 200), 1);
            AddObjects.Add(g);
            g = new Post(new Vector2(GraphicsDevice.Viewport.Width / 2 - 20 - 200, GraphicsDevice.Viewport.Height / 2 - 20 + 200), 1);
            AddObjects.Add(g);
            g = new Post(new Vector2(GraphicsDevice.Viewport.Width / 2 - 20 + 200, GraphicsDevice.Viewport.Height / 2 - 20 + 200), 1);
            AddObjects.Add(g);
            #endregion
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            // TODO: Add your update logic here
            SpawnPowerUp();
            SpawnBalls();
            foreach (GameObject go in AddObjects)
            {
                AllObjects.Add(go);
                go.Loadcontent(Content);
            }
            foreach (GameObject go in RemoveObjects)
            {
                AllObjects.Remove(go);
            }
            AddObjects.Clear();
            RemoveObjects.Clear();
            foreach (GameObject go in AllObjects)
            {
                go.Update(gameTime);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            foreach (GameObject go in AllObjects)
            {
                go.Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
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
                                addObjects.Add(new PowerUp(@"PowerUp_Lightning.png", position1, 1, PowerType.Speed));
                                powerUpSpawned = true;
                            }
                            break;
                        case 2:
                            {
                                addObjects.Add(new PowerUp(@"PowerUp_Heart.png", position1, 1, PowerType.StickyBall));
                                powerUpSpawned = true;
                            }
                            break;
                        case 3:
                            {
                                addObjects.Add(new PowerUp(@"PowerUp_MultiBall.png", position1, 1, PowerType.MultiBall));
                                powerUpSpawned = true;
                            }
                            break;
                        case 4:
                            {
                                addObjects.Add(new PowerUp(@"PowerUp_DeadBall_Skull.png", position1, 1, PowerType.StunBall));
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
                Ball ball = new Ball(new Vector2(GraphicsDevice.Viewport.Width / 2 - 16 + x, GraphicsDevice.Viewport.Height / 2 - 16 + y), 1);
                AddObjects.Add(ball);
                timer = 1000;
            }
            timer--;
        }
    }
}
