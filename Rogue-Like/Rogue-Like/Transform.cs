using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_Like
{
    /// <summary>
    /// A transform containing the position, rotation and velocity of each GameObject
    /// </summary>
    public struct Transform
    {
        /// <summary>
        /// The position of this object in the GameWorld
        /// </summary>
        public Vector2 Position;
        /// <summary>
        /// Velocity of this object
        /// </summary>
        public Vector2 Velocity;
        /// <summary>
        /// Rotation of this object
        /// </summary>
        public float Rotation;

        /// <summary>
        /// Creates a new instance of the Transform struct
        /// </summary>
        /// <param name="pos">Position in the GameWorld</param>
        /// <param name="v">Velocity</param>
        /// <param name="r">Rotation</param>
        public Transform(Vector2 pos, Vector2 v, float r)
        {
            this.Position = pos;
            this.Rotation = r;
            this.Velocity = v;

        }
        /// <summary>
        /// Creates a new instance of the Transform struct
        /// </summary>
        /// <param name="pos">Position in the GameWorld</param>
        /// <param name="r">Rotation</param>
        public Transform(Vector2 pos, float r)
        {
            this.Position = pos;
            this.Rotation = r;
            this.Velocity = Vector2.Zero;
        }
    }
}
