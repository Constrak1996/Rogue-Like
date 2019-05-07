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
        protected Texture2D texture;
        protected Vector2 position;
        protected Vector2 velocity;
        protected Vector2 center;
        protected Vector2 origin;
        protected ContentManager Content;
        public Vector2 Center
        {
            get { return center; }
        }
        public Vector2 Position
        {
            get { return position; }
        }
        public GameObject(string textureName, ContentManager Content, Vector2 position)
        {
            this.Content = Content;
            texture = Content.Load<Texture2D>(textureName);
            this.position = position;
            velocity = Vector2.Zero;
            center = new Vector2(position.X + texture.Width / 2, position.Y + texture.Height / 2);
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }
        public virtual void Update(GameTime gameTime)
        {
            this.center = new Vector2(position.X + texture.Width / 2, position.Y + texture.Height / 2);
        }
        /// <summary>
        /// Draws the GameObject
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

    }
}