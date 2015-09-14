using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OlmoSpil
{
    public struct Circle
    {
        private float radius;
        private Vector2 center;

        public float Radius
        {
            get
            {
                return radius;
            }
        }

        public Vector2 Center
        {
            get
            {
                return center;
            }
        }

        /// <summary>
        /// Collision circle
        /// </summary>
        /// <param name="center">objektets position</param>
        /// <param name="radius">radius af objektet</param>
        public Circle(Vector2 center, float radius)
            : this()
        {
            this.center.X = center.X + radius;
            this.center.Y = center.Y + radius;
            this.radius = radius;
        }

        /// <summary>
        /// Tjek af kollision
        /// </summary>
        /// <param name="other">Det andet objekts cirkel</param>
        /// <returns>Om der er kollision mellem de to cirkler</returns>
        public bool IntersectsWith(Circle other)
        {
            float dx = other.Center.X - this.center.X;
            float dy = other.Center.Y - this.center.Y;
            float sr = this.radius + other.Radius;
            double distance = Math.Sqrt(dx * dx + dy * dy);
            return (distance < sr);
        }
    }
}
