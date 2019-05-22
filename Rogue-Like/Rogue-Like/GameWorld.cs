﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        SpriteFont Font;
        private TimeSpan timeSinceStart;
        private State _currentState;
        private State _nextState;
        private float time;

        private static ContentManager _content;
        public static ContentManager ContentManager { get => _content; }

        //The lists used for loading and removing items
        public static List<GameObject> gameObjects = new List<GameObject>();
        public static List<GameObject> gameObjectsAdd = new List<GameObject>();
        public static List<GameObject> gameObjectsRemove = new List<GameObject>();
        public static List<GameObject> gameObjectsIdle = new List<GameObject>();

        //Graphics
        public static int Width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public static int Height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        //Player
        public static Player player;

        //Collision
        private Texture2D collisionTexture;

        //Enemy
        //Enemy enemy;
        private Texture2D arrow;
        private int i;

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        public GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _content = Content;
            IsMouseVisible = true;
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

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _currentState = new Menu(this, GraphicsDevice, Content);
            Font = Content.Load<SpriteFont>("Font");
            //Bullets
            arrow = Content.Load<Texture2D>("Arrow");
            //Collisionbox texture
            collisionTexture = Content.Load<Texture2D>("OnePixel");
            //Enemy

            //enemy = new Enemy("Worker", new Transform(new Vector2(0, 0), 0), 0,20,2);
            //Player
            player = new Player("Fisher_Bob", new Transform(new Vector2(400, 50), 0));
            gameObjectsAdd.Add(player);
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

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
            //Updates gameobjects
            foreach (GameObject go in gameObjects)
            {
                go.Update(gameTime);
            }

            //Adds gameobjects to the gameobjects list
            if (gameObjectsAdd.Count > 0)
            {
                for (int i = 0; i < gameObjectsAdd.Count; i++)
                {
                    gameObjects.Add(gameObjectsAdd[i]);
                }
                gameObjectsAdd.Clear();
            }

            // Remove all game objects in removeList
            foreach (GameObject obj in gameObjectsRemove)
            {
                gameObjects.Remove(obj);
            }
            gameObjectsRemove.Clear();

            //Player movement
            player.PlayerMovement(3);
            //Spawn Enemy at Level
            //SpawnEnemyAtLevel();

            base.Update(gameTime);
        }

        //private static void SpawnEnemyAtLevel()
        //{
        //    if (Enemy.level1 == true)
        //    {

        //        for (int i = 0; i < 2; i++)
        //        {
        //            Enemy.SpawnEnemy();

        //        }

        //    }
        //    if (Enemy.level2 == true)
        //    {

        //        for (int i = 2; i < 4; i++)
        //        {
        //            Enemy.SpawnEnemy();
        //        }
        //    }
        //}

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            _currentState.Draw(gameTime, spriteBatch);
            if (Shop.shop == true || Level1.lvl1 == true || Level2.lvl2 == true)
            {


                //Draws sprites in gameObjects list
                foreach (GameObject go in gameObjects)
                {
                    go.Draw(spriteBatch);

                }

                //Collision texture draw
                foreach (GameObject go in gameObjects)
                {
                    go.Draw(spriteBatch);

#if DEBUG
                    DrawCollisionBox(go);
#endif
                }
                spriteBatch.DrawString(Font, $" {Player.Name}\n Health: {Player.health}\n Damage: {Player.damage}\n Gold: {Player.coin}\n Food: {Player.food}\n Score: {Player.Score}", new Vector2(1735, 0), Color.White);

            }
            
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawCollisionBox(GameObject go)
        {
            //Creating a box around the object
            Rectangle collisionBox = go.Hitbox;

            //Definening each side
            Rectangle topLine = new Rectangle(collisionBox.Center.X - collisionBox.Width, collisionBox.Center.Y - collisionBox.Height, collisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(collisionBox.Center.X - collisionBox.Width, collisionBox.Center.Y + collisionBox.Height / 30, collisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(collisionBox.Center.X + collisionBox.Width / 30, collisionBox.Center.Y - collisionBox.Height, 1, collisionBox.Height);
            Rectangle leftLine = new Rectangle(collisionBox.Center.X - collisionBox.Width, collisionBox.Center.Y - collisionBox.Height, 1, collisionBox.Height);

            //Draw each side
            spriteBatch.Draw(collisionTexture, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        
    }
}
