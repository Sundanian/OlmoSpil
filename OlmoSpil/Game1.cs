﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace OlmoSpil
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
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


        private static bool powerUpSpawned = false;

        public static bool PowerUpSpawned
        {
            get { return Game1.powerUpSpawned; }
            set { Game1.powerUpSpawned = value; }
        }


        private static bool powerUpSpawned = false;

        public static bool PowerUpSpawned
        {
            get { return Game1.powerUpSpawned; }
            set { Game1.powerUpSpawned = value; }
        }


        private static bool powerUpSpawned = false;

        public static bool PowerUpSpawned
        {
            get { return Game1.powerUpSpawned; }
            set { Game1.powerUpSpawned = value; }
        }


        private static bool powerUpSpawned = false;

        public static bool PowerUpSpawned
        {
            get { return Game1.powerUpSpawned; }
            set { Game1.powerUpSpawned = value; }
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
            foreach (GameObject go in AllObjects)
            {
                go.Loadcontent(Content);
            }
            for (int i = 0; i < 10; i++)
            {
                Ball ball = new Ball(new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), 1);
                AddObjects.Add(ball);
            }
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
        //Din metode mangler Skinke
    }
}
