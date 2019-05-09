using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rogue_Like
{
    public abstract class Component
    {
        private Texture2D playersprite;
        public GameObject GameObject
        {
            get;
            set;
        }
        
        public virtual void Attach(GameObject gameObject)
        {
            GameObject = gameObject;
        }
        public virtual void LoadContent(ContentManager content)
        {
            playersprite = content.Load<Texture2D>("Fisher_Bob");
            //An empty virtual function that allow other components to override and implement their own functionailty
        }
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBath);
        public abstract void Update(GameTime gameTime);
    }
}