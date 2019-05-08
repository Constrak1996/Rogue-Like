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
        public static Transform playerTransform = new Transform(new Vector2(200, 100), new Vector2(), 1f);

        public string Name { get => name; set => name = value; }
        public Player(Texture2D playersprite, string textureName,ContentManager Content, Transform Transform):base(textureName,Content,Transform)
        {
            Font = Content.Load<SpriteFont>("Font");
            this.playersprite = playersprite;
            this.Transform = playerTransform;
            this.name = "Bob";
            speed = 5;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playersprite, position, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                position.Y -= 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                position.X -= 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                position.Y += 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                position.X += 2;
            }
        }
    }
    
}