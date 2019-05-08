using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Rogue_Like
{
    public class Menu : State
    {
        private Controller controller = new Controller();
        private List<Component> _component;
        //private Thread idleIkon;
        /// <summary>
        /// The MenuStates Constructor
        /// </summary>
        /// <param name="gameWorld"></param>
        /// <param name="graphicsDevice"></param>
        /// <param name="content"></param>
        public Menu(GameWorld gameWorld, GraphicsDevice graphicsDevice, ContentManager content) : base(gameWorld, graphicsDevice, content)
        {
            
            var buttonTexture = _content.Load<Texture2D>("Button");
            var buttonFont = _content.Load<SpriteFont>("Font");

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(700, 200),
                Text = "New Game",
            };
            newGameButton.Click += NewGameButton_Click;


            var highScoreButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(700, 400),
                Text = "HighScore",
            };
            highScoreButton.Click += HighScoreButton_Click;
            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(700, 600),
                Text = "Quit",
            };
            quitGameButton.Click += QuitGameButton_Click;

            _component = new List<Component>()
            {
                newGameButton,
                
                highScoreButton,
                quitGameButton,
            };
        }
        /// <summary>
        /// Make a idleikon spinning around in the buttom right side of the screen attached to a thread
        /// </summary>
        //public void IdleIkon()
        //{
            
        //    //idleIkon = new Thread(new ThreadStart(IdleIkon));
        //    idleIkon.Start();
        //    idleIkon.IsBackground = true;
        //}
        /// <summary>
        /// Draws the MenuState
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (var component in _component)
            {
                component.Draw(gameTime, spriteBatch);
            }
            //IdleIkon();
            spriteBatch.End();
        }
        //Makes a Newgamebutton
        private void NewGameButton_Click(object sender, EventArgs e)
        {
            controller.newPlayer();
            _gameWorld.ChangeState(new Map1(_gameWorld, _graphichsDevice, _content));
        }
        //Makes a HighScorebutton
        private void HighScoreButton_Click(object sender, EventArgs e)
        {
            controller.newPlayer();
            _gameWorld.ChangeState(new HighScore(_gameWorld, _graphichsDevice, _content));
        }

        public override void PostUpdate(GameTime gameTime)
        {
            //remove sprite if they are not needen no more
        }
        /// <summary>
        /// Updates the MenuState
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            foreach (var component in _component)
            {
                component.Update(gameTime);
            }
            //IdleIkon();
        }
        //Makes a QuitGamebutton
        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _gameWorld.Exit();
        }
    }
}