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
        protected ContentManager Content;
        protected Transform Transform;
        protected Texture2D sprite;
        public Vector2 Center
        {
            get { return center; }
        }
        public Vector2 Position
        {
            get { return position; }
        }
        public virtual Rectangle Hitbox
        {
            get { return new Rectangle((int)Transform.Position.X, (int)Transform.Position.Y, sprite.Width, sprite.Height); }
        }
        public GameObject(string textureName, ContentManager Content, Transform Transform)
        {
            this.Content = Content;
            sprite = Content.Load<Texture2D>(textureName);
            this.position = Transform.Position;
            velocity = Vector2.Zero;
            center = new Vector2(Transform.Position.X + sprite.Width / 2, Transform.Position.Y + sprite.Height / 2);
            origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
        }
        public virtual void Update(GameTime gameTime)
        {
            this.center = new Vector2(position.X + sprite.Width / 2, position.Y + sprite.Height / 2);
        }
        /// <summary>
        /// Draws the GameObject
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, Color.White);
        }

    }
}