using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        private int speed;
        

        private string name;
        private Vector2 position;

        public string Name { get => name; set => name = value; }
        public Player(Texture2D playersprite, string textureName,ContentManager Content, Vector2 position):base(textureName,Content,position)
        {
            Font = Content.Load<SpriteFont>("Font");
            this.playersprite = playersprite;
            this.name = "Bob";
            this.position = new Vector2(325, 50);
            speed = 5;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playersprite, position, Color.White);
        }
        
        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                position.X--;
            }
        }
    }
    
}