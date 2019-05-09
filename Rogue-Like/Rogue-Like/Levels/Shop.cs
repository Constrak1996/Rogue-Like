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
        Player player;
        public Shop(GameWorld gameWorld, GraphicsDevice graphicsDevice, ContentManager content) : base(gameWorld, graphicsDevice, content)
        {
            _playerTexture = content.Load<Texture2D>("Fisher_Bob");
            player = new Player(_playerTexture, "Fisher_Bob", content, Player.playerTransform);
            Font = content.Load<SpriteFont>("Font");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(Font, $"Welcome {player.Name} to the shop, here you can upgrade if you got the coins, if not Hit the ESC key to return", Vector2.Zero, Color.White);
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                _gameWorld.ChangeState(new Map1(_gameWorld, _graphichsDevice, _content));
            }
        }
    }
}