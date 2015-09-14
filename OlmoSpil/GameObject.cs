﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace OlmoSpil
{
    abstract class GameObject
    {

        #region Fields
        /// <summary>
        /// The GameObject's texture
        /// </summary>
        private Texture2D texture;

        /// <summary>
        /// The GameObject's rectangle this is used when drawing the sprite
        /// </summary>
        private Rectangle[] rectangles;

        /// <summary>
        /// The GameObject's position
        /// </summary>
        protected Vector2 position = Vector2.Zero;

        /// <summary>
        /// The GameObject's origin
        /// </summary>
        private Vector2 origin = Vector2.Zero;

        /// <summary>
        /// The GameObject's layer
        /// Insert "spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend)"
        /// This should be placed where the gameObject's are made and where there position is updated
        /// </summary>
        private float layer = 0;

        /// <summary>
        /// The GameObject's scale
        /// </summary>
        private float scale = 1;

        /// <summary>
        /// The GameObject's color
        /// When the Color is set to "Color.White", the sprite is filled with a white background.
        /// </summary>
        private Color color = Color.White;

        /// <summary>
        /// SpriteEffect applied to the GameObject
        /// This could f.x., make the sprite FlipHorizontally.
        /// </summary>
        private SpriteEffects effect = new SpriteEffects();

        /// <summary>
        /// The GameObjects velocity
        /// </summary>
        protected Vector2 velocity;

        /// <summary>
        /// The GameObjects movement speed
        /// </summary>
        protected float speed;

        /// <summary>
        /// Number of frames
        /// </summary>
        private int frames;

        /// <summary>
        /// Index used while running though an animation
        /// </summary>
        private int currentIndex;

        /// <summary>
        /// Time passed since last frame change
        /// </summary>
        private float timeElapsed;

        /// <summary>
        /// The animations fps
        /// </summary>
        protected float fps;

        #endregion

        #region Methodes

        /// <summary>
        /// Loads the SpriteObject's content
        /// Is a virtual void, so it can be overwritten.
        /// </summary>
        /// <param name="content"></Sets the texture if an Object and the rectanle around the texture>
        public virtual void Loadcontent(ContentManager content)
        {
            #region Place THIS in override Loadcontent
            //Loads the object's texture
            texture = content.Load<Texture2D>(@"");  // <-- Place the fileLocation of the sprite f.x. (@"apple");

            //Loads the parameters of the gameObject
            //base.LoadContent(content);
            #endregion

            //Calculates the width of the frame
            int width = texture.Width / frames;

            //Instantiates the rectangle's array
            rectangles = new Rectangle[frames];

            //Creates the rectangles
            for (int i = 0; i < frames; i++)
            {
                rectangles[i] = new Rectangle(i * width, 0, width, texture.Height);
            }
        }

        /// <summary>
        /// Draws the SpriteObject to the screen
        /// </summary>
        /// <param name="spriteBatch"></Holds all the diffferent parameters of the spriteObject>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //Draws the SpriteObject, with all its parameters
            spriteBatch.Draw(texture, position, rectangles[currentIndex], color, 0, origin, scale, effect, layer);
        }

        /// <summary>
        /// Keeps track of the current game time and the current index of the animations
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            //Adds time that have passed since last update
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Calculates the current index
            currentIndex = (int)(timeElapsed * fps);

            //Checks if we need to restart the animation
            if (currentIndex > rectangles.Length -1)
            {
                timeElapsed = 0;
                currentIndex = 0;
            }
        }


        #endregion

        /// <summary>
        /// The SpriteObject's constructor
        /// </summary>
        /// <param name="position"></Initial position of the object>
        public GameObject(Vector2 position, int frames)
        { 
            //The initial position of the object
            this.position = position;
            
            //The number of frames for the GameObject
            this.frames = frames;

        }
        
    }
}
