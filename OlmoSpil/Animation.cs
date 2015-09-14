using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace OlmoSpil
{
    class Animation
    {
        #region Fields

        /// <summary>
        /// The animation's offset
        /// </summary>
        private Vector2 offset;

        /// <summary>
        /// The animation's fps
        /// </summary>
        private float fps;

        /// <summary>
        /// The animation's rectangles
        /// </summary>
        private Rectangle[] rectangles;

        //Properties
        public float Fps
        {
            get { return fps; }
            set { fps = value; }
        }

        public Rectangle[] Rectangles
        {
            get { return rectangles; }
            set { rectangles = value; }
        }

        public Vector2 Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        #endregion

        #region Methodes



        #endregion

        /// <summary>
        /// The animation constructor
        /// </summary>
        /// <param name="frames">Number of frames in the animation</param>
        /// <param name="yPos">Y position on the prite sheet in pixels</param>
        /// <param name="xStartFrame">X position on the sprite sheet in frames</param>
        /// <param name="width">The width of each frame</param>
        /// <param name="height">The height of each frame</param>
        /// <param name="offset">Animation offset(can be used to align animations</param>
        /// <param name="fps">Animation fps</param>
        public Animation(int frames, int yPos, int xStartFrame, int width, int height, Vector2 offset, float fps)
        {
            rectangles = new Rectangle[frames];

            for (int i = 0; i < frames; i++)
            {
                rectangles[i] = new Rectangle((i + xStartFrame) * width, yPos, width, height);
            }

            this.fps = fps;
            this.offset = offset;
        }

    }
}
