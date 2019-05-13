using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rogue_Like
{
    public class Shop : State
    {
        private SpriteFont Font;
        private Texture2D _playerTexture;
        private int playerHealth = Player.health;
        
        public Shop(GameWorld gameWorld, GraphicsDevice graphicsDevice, ContentManager content) : base(gameWorld, graphicsDevice, content)
        {
            _playerTexture = content.Load<Texture2D>("Fisher_Bob");
            
            Font = content.Load<SpriteFont>("Font");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            spriteBatch.DrawString(Font, $"Welcome {Player.Instance.Name} to the shop, here you can upgrade if you got the coins, if not Hit the 1 key to return", Vector2.Zero, Color.White);
            
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                _gameWorld.ChangeState(new Map1(_gameWorld, _graphichsDevice, _content));
            }
        }
    }
}