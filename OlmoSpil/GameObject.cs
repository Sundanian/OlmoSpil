using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace OlmoSpil
{
    public abstract class GameObject
    {

        #region Fields
        /// <summary>
        /// Collision circle that fits the object
        /// </summary>
        public Circle CollisionCircle
        {
            get
            {
                float radius = this.texture.Width / 2;

                return new Circle(position, radius);
            }
        }

        /// <summary>
        /// Collision circle for shoot funktionality
        /// </summary>
        public Circle ShootCollisionCircle
        {
            get
            {
                float radius = this.texture.Width / 2 + 10;

                return new Circle(new Vector2(this.position.X - 10, this.position.Y - 10), radius);
            }
        }

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

		/// <summary>
        /// Dictionary, that contains all animations
        /// </summary>
        private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();

        /// <summary>
        /// For collisionbox
        /// </summary>
        private Texture2D boxtexture;


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

            ////Calculates the width of the frame
            //int width = texture.Width / frames;

            ////Instantiates the rectangle's array
            //rectangles = new Rectangle[frames];

            ////Creates the rectangles
            //for (int i = 0; i < frames; i++)
            //{
            //    rectangles[i] = new Rectangle(i * width, 0, width, texture.Height);
            //}

            boxtexture = content.Load<Texture2D>(@"CollisionTexture");
        }

        /// <summary>
        /// Draws the SpriteObject to the screen
        /// </summary>
        /// <param name="spriteBatch"></Holds all the diffferent parameters of the spriteObject>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //Draws the SpriteObject, with all its parameters
            spriteBatch.Draw(texture, position, rectangles[currentIndex], color, 0, origin, scale, effect, layer);
            
//#if DEBUG
//            System.Drawing.Graphics dc = new System.Drawing.Graphics();
//            dc.DrawEllipse(new System.Drawing.Pen(System.Drawing.Brushes.Red), CollisionCircle.Center.X - CollisionCircle.Radius, CollisionCircle.Center.Y - CollisionCircle.Radius, CollisionCircle.Radius * 2, CollisionCircle.Radius * 2);
//            //spriteBatch.Draw(boxtexture, CollisionCircle, Color.Red);
//            //spriteBatch.Draw(boxtexture, ShootCollisionCircle, Color.Purple);'
//            spriteBatch.
//#endif
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
            CheckCollision();
        }

        /// <summary>
        /// Creates an animation
        /// </summary>
        /// <param name="name">Animation name</param>
        /// <param name="frames">Number of frames in the animation</param>
        /// <param name="yPos">Y position on the sprite sheet in pixels</param>
        /// <param name="xStartFrame">X position on the sprite sheet in frames</param>
        /// <param name="width">The width of each frame</param>
        /// <param name="height">The height of each frame</param>
        /// <param name="offset">Animation offset (can be used to align animations)</param>
        /// <param name="fps">Animation fps</param>
        protected void CreateAnimation(string name, int frames, int yPos, int xStartFrame, int width, int height, Vector2 offset, float fps)
        {
            animations.Add(name, new Animation(frames, yPos, xStartFrame, width, height, offset, fps));
            /* Sample
             * CreateAnimation("Left", 1, 50, 12, 50, 50, Vector2.Zero, 1);
             * Makes an animation with the sprites in "Left", which has only 1 frame, which starts in position 50 on the y-axis
             * X's start position, which is at 12 frames
             * The width of the sprite, which is 50
             * The height if the sprite, which is 50
             * The offset, to which the animation will run from, which is set to the Vector2.zero, which runs on the privious frame
             * Fps, which controls how many frames will be played per second, which is 1
             */
        }
        /// <summary>
        /// Returns true, if the GameObject is colliding with the other GameObject
        /// </summary>
        /// <param name="other">GameObject to check collising with</param>
        /// <returns></returns>
        public bool IsCollidingWith(GameObject other)
        {
            return CollisionCircle.IntersectsWith(other.CollisionCircle);
        }
        /// <summary>
        /// Checks if an object is colliding with another Object
        /// </summary>
        private void CheckCollision()
        {
            foreach (GameObject go in Game1.AllObjects)
            {
                //This prevents an object from colliding with itself.
                if (go != this)
                {
                    if (this.IsCollidingWith(go))
                    {
                        //Collsion
                        OnCollision(go);
                    }
                }
            }
        }
        public abstract void OnCollision(GameObject other);
        #endregion
    }
}
