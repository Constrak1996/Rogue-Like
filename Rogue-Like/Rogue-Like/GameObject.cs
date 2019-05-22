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
        public GameObject(Transform transform)
        {
            this.Transform = transform;

        }

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, new Vector2(Transform.Position.X, Transform.Position.Y - 1), null, Color.White, 0f, spriteCenter, 1f, SpriteEffects.None, 1f);
        }
    }
}