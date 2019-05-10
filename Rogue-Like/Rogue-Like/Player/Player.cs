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
    public class Player : Component
    {
        ContentManager Content;
        Transform transform;
        private static Player instance;
        private Texture2D playersprite;
        public static int health;
        public Random randomPlayerHealth = new Random();
        public int score;
        SpriteFont Font;
        private int speed;
        private Vector2 position;
        private string name;
        public static Transform playerTransform = new Transform(new Vector2(200, 100), new Vector2(), 1f);

        public static Player Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Player();
                    
                }
                return instance;
            }
        }
        public string Name
        {
            get => name;
            set => name = value;
        }
        private Player()
        {
            this.playersprite = Content.Load<Texture2D>("Fisher_Bob");
            this.transform = playerTransform;
            
            this.name = "Bob";
            speed = 5;
            Player.health = randomPlayerHealth.Next(50, 75);
        }
        
        
        public void Movement()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                transform.Position.Y -= 1 * speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                position.X -= 1 * speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                position.Y += 1 * speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                position.X += 1 * speed;
            }
        }

        public override void Update(GameTime gameTime)
        {
            Movement();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBath)
        {
            spriteBath.Draw(playersprite, position, Color.White);
        }
    }
    
}