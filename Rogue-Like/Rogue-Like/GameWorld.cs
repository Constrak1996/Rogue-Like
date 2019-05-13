using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Rogue_Like
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameWorld : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player;
        
        private TimeSpan timeSinceStart;
        private State _currentState;
        private State _nextState;
        private float time;
        public static int Width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public static int Height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        public void ChangeState(State state)
        {
            _nextState = state;
        }

        //Object pool
        private List<GameObject> gameObjects = new List<GameObject>();
        private static List<GameObject> gameObjectsAdd = new List<GameObject>();
        private static List<GameObject> gameObjectRemove = new List<GameObject>();
        private Texture2D collisionTexture;

        public GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = Width;
            graphics.PreferredBackBufferHeight = Height;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            IsMouseVisible = true;
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _currentState = new Menu(this, GraphicsDevice, Content);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();
            if (_nextState != null)
            {
                _currentState = _nextState;
                _nextState = null;
            }
            _currentState.Update(gameTime);
            _currentState.PostUpdate(gameTime);

            timeSinceStart += gameTime.ElapsedGameTime;
            time = (int)timeSinceStart.Seconds;
            // TODO: Add your update logic here

            // Remove all game objects in removeList
            foreach (GameObject obj in gameObjectRemove)
            {
                gameObjects.Remove(obj);
            }
            gameObjectRemove.Clear();


            // Add all game obejcts in addList
            foreach (GameObject obj in gameObjectsAdd)
            {
                gameObjects.Add(obj);
            }
            gameObjectsAdd.Clear();

            Player.Instance.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            _currentState.Draw(gameTime, spriteBatch);
            Player.Instance.Draw(spriteBatch);
            // Iterate and draw each object
            foreach (GameObject obj in gameObjects)
            {
                obj.Draw(spriteBatch);
            }

            //Collision texture draw
            foreach (GameObject go in gameObjects)
            {
                go.Draw(spriteBatch);
#if DEBUG
                DrawCollisionBox(go);
#endif
            }
            // TODO: Add your drawing code here

            base.Draw(gameTime);
            spriteBatch.End();
        }
        private void DrawCollisionBox(GameObject go)
        {
            //Creating a box around the object
            Rectangle collisionBox = go.Hitbox;

            Rectangle topLine = new Rectangle(collisionBox.Center.X - collisionBox.Width / 2, collisionBox.Center.Y - collisionBox.Height / 2, collisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(collisionBox.Center.X - collisionBox.Width / 2, collisionBox.Center.Y + collisionBox.Height / 2, collisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(collisionBox.Center.X + collisionBox.Width / 2, collisionBox.Center.Y - collisionBox.Height / 2, 1, collisionBox.Height);
            Rectangle leftLine = new Rectangle(collisionBox.Center.X - collisionBox.Width / 2, collisionBox.Center.Y - collisionBox.Height / 2, 1, collisionBox.Height);

            spriteBatch.Draw(collisionTexture, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
        }
    }
}
