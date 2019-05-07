using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rogue_Like
{
    public class Player : GameObject
    {
        private Texture2D playersprite;
        public int score;
        SpriteFont Font;

        private string name;
        private Vector2 position;

        public string Name { get => name; set => name = value; }
        public Player(Texture2D playersprite, string textureName,ContentManager Content, Vector2 position):base(textureName,Content,position)
        {
            Font = Content.Load<SpriteFont>("Font");
            this.playersprite = playersprite;
            this.name = "Bob";
            this.position = new Vector2(325, 50);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
        }

        public void PostUpdate(GameTime gameTime)
        {
        }

        public void Update(GameTime gameTime)
        {
        }
    }
    
}