using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rogue_Like
{
    class HighScore : State
    {
        private Controller controller;
        
        public SpriteFont textFont;

        public HighScore(GameWorld gameWorld, GraphicsDevice graphicsDevice, ContentManager content) : base(gameWorld, graphicsDevice, content)
        {
            controller = new Controller();
            textFont = _content.Load<SpriteFont>("Font");



        }
        public override void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            spritebatch.DrawString(textFont, controller.GetHighScore(), new Vector2(750, 300), Color.White);
        }
        public override void PostUpdate(GameTime gameTime)
        {
            //remove sprite if they are not needen no more
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                _gameWorld.ChangeState(new Menu(_gameWorld, _graphichsDevice, _content));
            }
        }
        
    }
}
