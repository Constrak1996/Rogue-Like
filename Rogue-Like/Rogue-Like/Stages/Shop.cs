using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_Like
{
    public class Shop : State
    {
        private SpriteFont textFont;
        public Shop(GameWorld gameWorld, GraphicsDevice graphicsDevice, ContentManager content) : base(gameWorld, graphicsDevice, content)
        {
            textFont = content.Load<SpriteFont>("Font");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(textFont, $"Welcome {Player.Name}: As this moment your Wallet should contain {Player.coin} pieces of gold\nSeach the shelf for the item of your dreams \n Should it happend your wallet is too heavy spend it here for more happiness\n Do you not have the coin, hit the key 1 and seach the dungeons for the needs to aquire the item that you desire", new Vector2(200,200), Color.Black);
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                _gameWorld.ChangeState(new Level1(_gameWorld, _graphichsDevice, _content));
            }

        }
    }
}
