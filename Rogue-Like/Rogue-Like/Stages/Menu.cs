using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_Like
{
    public class Menu : State
    {
        private Controller controller = new Controller();
        Player player;
        private List<Component> _component;
        private Model model;
        public static bool newgame;
        public static bool resume;
        public static bool menu;
        
        /// <summary>
        /// The MenuStates Constructor
        /// </summary>
        /// <param name="gameWorld"></param>
        /// <param name="graphicsDevice"></param>
        /// <param name="content"></param>
        public Menu(GameWorld gameWorld, GraphicsDevice graphicsDevice, ContentManager content) : base(gameWorld, graphicsDevice, content)
        {
            menu = true;
            model = new Model();
            var buttonTexture = _content.Load<Texture2D>("Button");
            var buttonFont = _content.Load<SpriteFont>("Font");

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(700, 200),
                Text = "New Game",
            };
            newGameButton.Click += NewGameButton_Click;
            var resumeButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(700, 400),
                Text = "Resume",
            };
            resumeButton.Click += ResumeButton_Click;

            var highScoreButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(700, 600),
                Text = "HighScore",
            };
            highScoreButton.Click += HighScoreButton_Click;
            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(700, 800),
                Text = "Quit",
            };
            quitGameButton.Click += QuitGameButton_Click;

            _component = new List<Component>()
            {
                newGameButton,
                resumeButton,
                highScoreButton,
                quitGameButton,
            };
        }
        
        /// <summary>
        /// Draws the MenuState
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            foreach (var component in _component)
            {
                component.Draw(gameTime, spriteBatch);
            }
            //IdleIkon();

        }
        //Makes a Newgamebutton
        private void NewGameButton_Click(object sender, EventArgs e)
        {
            if (newgame)
            {
                
                controller.NewPlayer();
                _gameWorld.ChangeState(new Shop_Level1(_gameWorld, _graphichsDevice, _content));
                Shop_Level1.shop = true;
                menu = false;
                Player.score = "0";
                Player.currentHealth = 20;
                resume = true;
                GameWorld.isPlaying = true;
                
            }
                
        }
        
        //Makes a Resumebutton
        private void ResumeButton_Click(object sender, EventArgs e)
        {
            if (resume)
            {
                _gameWorld.ChangeState(new Shop_Level1(_gameWorld, _graphichsDevice, _content));
                Shop_Level1.shop = true;
                menu = false;
            }


        }
        //Makes a HighScorebutton
        private void HighScoreButton_Click(object sender, EventArgs e)
        {

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
            GameWorld.isPlaying = false;
        }
    }
}
