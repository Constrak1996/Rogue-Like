using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rogue_Like
{
    public class GameObject
    {
        protected Vector2 position;
        protected Vector2 velocity;
        protected Vector2 center;
        protected Vector2 origin;
        protected float rotation;
        public Transform Transform;
        public Texture2D sprite;
        public Vector2 spriteCenter;
        public string spriteName;

        /// <summary>
        /// Makes a rectangle that will be made into the objects hitbox
        /// </summary>
        public virtual Rectangle Hitbox
        {
            get { return new Rectangle((int)Transform.Position.X, (int)Transform.Position.Y, sprite.Width, sprite.Height); }
        }

        /// <summary>
        /// Constructor for our GameObject
        /// </summary>
        /// <param name="Sprite">The sprite for the specific object</param>
        /// <param name="Transform">All positions and such is held in here</param>
        public GameObject(string spriteName, Transform Transform)
        {
            this.sprite = GameWorld.ContentManager.Load<Texture2D>(spriteName);
            this.Transform = Transform;
            spriteCenter = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, new Vector2(Transform.Position.X, Transform.Position.Y), null, Color.White, rotation, spriteCenter, 1f, SpriteEffects.None, 1f);
        }

        /// <summary>
        /// IsColliding checks if there are other object colliding with the current one
        /// </summary>
        /// <param name="otherObject"></param>
        /// <returns></returns>
        public bool IsColliding(GameObject otherObject)
        {
            //A method used to determine when a object collides with another
            return Hitbox.Intersects(otherObject.Hitbox);
        }

        public virtual void DoCollision(GameObject otherObject)
        {

        }
    }
}