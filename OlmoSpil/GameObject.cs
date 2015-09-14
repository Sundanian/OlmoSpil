using System;
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

        #region Variables
        /// <summary>
        /// The GameObject's texture
        /// </summary>
        private Texture2D texture;

        /// <summary>
        /// The GameObject's rectangle this is used when drawing the sprite
        /// </summary>
        private Rectangle[] rectangle;

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

        #endregion

        #region Methodes

        /// <summary>
        /// Loads the SpriteObject's content
        /// Is a virtual void, so it can be overwritten.
        /// </summary>
        /// <param name="content"></Sets the texture if an Object and the rectanle around the texture>
        public virtual void Loadcontent(ContentManager content)
        {
            #region Place in override Loadcontent
            //Loads the object's texture
            texture = content.Load<Texture2D>(@"");  // <-- Place the fileLocation of the sprite f.x. (@"apple");

            //Loads the parameters of the gameObject
            //base.LoadContent(content);
            #endregion
            //Sets the size and position of the object
            rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
        }

        /// <summary>
        /// Draws the SpriteObject to the screen
        /// </summary>
        /// <param name="spriteBatch"></Holds all the diffferent parameters of the spriteObject>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //Draws the SpriteObject, with all its parameters
            spriteBatch.Draw(texture, position, rectangle, color, 0, origin, scale, effect, layer);
        }



        #endregion

        /// <summary>
        /// The SpriteObject's constructor
        /// </summary>
        /// <param name="position"></Initial position of the object>
        public GameObject(Vector2 position)
        { 
            //The initial position of the object
            this.position = position;
        }
        
    }
}
