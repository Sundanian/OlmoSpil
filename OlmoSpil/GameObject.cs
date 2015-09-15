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
        private Vector2 offset;
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
        protected Texture2D texture;

        /// <summary>
        /// The GameObject's rectangle this is used when drawing the sprite
        /// </summary>
        private Rectangle[] rectangles;

        /// <summary>
        /// The GameObject's position
        /// </summary>
        private Vector2 position = Vector2.Zero;

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

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
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

        #region Methods
        /// <summary>
        /// Loads the SpriteObject's content
        /// Is a virtual void, so it can be overwritten.
        /// </summary>
        /// <param name="content"></Sets the texture if an Object and the rectanle around the texture>
        public virtual void Loadcontent(ContentManager content)
        {
            #region Place THIS in override Loadcontent
            //Loads the object's texture
            //texture = content.Load<Texture2D>(@"");  // <-- Place the fileLocation of the sprite f.x. (@"apple");

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

            CreateAnimations(texture);
        }

        /// <summary>
        /// Draws the SpriteObject to the screen
        /// </summary>
        /// <param name="spriteBatch"></Holds all the diffferent parameters of the spriteObject>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //Draws the SpriteObject, with all its parameters
            spriteBatch.Draw(texture, position, rectangles[currentIndex], color, 0, origin, scale, effect, layer);
            
#if DEBUG
            Texture2D circle = CreateCircle((int)CollisionCircle.Radius);
            spriteBatch.Draw(circle, Position, Color.Red);
#endif
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
        /// <summary>
        /// Creates a circle from the radius specified.
        /// </summary>
        /// <param name="radius"></param>
        /// <returns></returns>
        public Texture2D CreateCircle(int radius)
        {
            int outerRadius = radius * 2 + 2; // So circle doesn't go out of bounds
            Texture2D texture = new Texture2D(Game1.Graphics.GraphicsDevice, outerRadius, outerRadius);

            Color[] data = new Color[outerRadius * outerRadius];

            // Colour the entire texture transparent first.
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.Transparent;

            // Work out the minimum step necessary using trigonometry + sine approximation.
            double angleStep = 1f / radius;

            for (double angle = 0; angle < Math.PI * 2; angle += angleStep)
            {
                // Use the parametric definition of a circle: http://en.wikipedia.org/wiki/Circle#Cartesian_coordinates
                int x = (int)Math.Round(radius + radius * Math.Cos(angle));
                int y = (int)Math.Round(radius + radius * Math.Sin(angle));

                data[y * outerRadius + x + 1] = Color.White;
            }

            texture.SetData(data);
            return texture;
        }
        /// <summary>
        /// Not to be confused with CreateAnimation. No -s.
        /// Use the CreateAnimation method in here to create animations.
        /// </summary>
        /// <param name="texture"></param>
        protected abstract void CreateAnimations(Texture2D texture);
        protected void PlayAnimation(string name)
        {
            rectangles = animations[name].Rectangles;
            offset = animations[name].Offset;
            fps = animations[name].Fps;
        }
        #endregion
    }
}
