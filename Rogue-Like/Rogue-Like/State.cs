﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rogue_Like
{
    public abstract class State
    {
        #region Fields
        protected ContentManager _content;
        protected GraphicsDevice _graphichsDevice;
        protected GameWorld _gameWorld;


        #endregion

        #region Methods
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void PostUpdate(GameTime gameTime);
        public State(GameWorld gameWorld, GraphicsDevice graphicsDevice, ContentManager content)
        {
            _gameWorld = gameWorld;
            _graphichsDevice = graphicsDevice;
            _content = content;

        }
        public abstract void Update(GameTime gameTime);
        #endregion
    }
}