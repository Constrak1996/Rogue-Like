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
    public class Player
    {
        private static Player instance;
        
        private Texture2D playerTexture;
        public static int health;
        public Random randomPlayerHealth = new Random();
        public int score;
        private SpriteFont Font;
        private int speed;
        Transform transform;
        public Vector2 position;
        private string name;
        public static Transform playerTransform = new Transform(new Vector2(300, 200), new Vector2(), 1f);
        
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
        
        public Player()
        {
            this.name = "Bob";
            position = new Vector2(400, 50);
            speed = 5;
            Player.health = randomPlayerHealth.Next(50, 75);
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public void Update(GameTime gameTime)
        {
            Movement();
        }

        public void Movement()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                position.Y -= 1 * speed;
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
    }
    
}