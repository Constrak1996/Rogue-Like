using Microsoft.Xna.Framework;
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

        //Graphics
        public static int Width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public static int Height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        //Player
        public static Player player;

        //Bullet
        public static Bullet bullet;

        //Collision
        private Texture2D collisionTexture;

        //Enemy
        Enemy enemy;
        private int i;

        //Level bools
        public static bool level1;
        public static bool level2;
        public static bool level3;
        public static bool level4;

        //Room bools
        public static bool room1;
        public static bool room2;
        public static bool room3;
        public static bool room4;

        //Spawn once checks
        public static bool L1;
        public static bool L2;

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
            //Collisionbox texture
            collisionTexture = Content.Load<Texture2D>("OnePixel");
            
            //Enemy
            enemy = new Enemy("Worker", new Transform(Vector2.Zero, 0), 0, 0, null);
            
            //Player
            player = new Player("Fisher_Bob", new Transform(new Vector2(400, 50), 0));
            gameObjectsAdd.Add(player);

            //Bullet Texture
            bullet = new Bullet("BulletTest", new Transform(new Vector2(100,100), 0), new Vector2(1,1));
            
            
            gameObjectsRemove.Add(enemy);
            gameObjectsRemove.Add(bullet);

            //Level bools running once
            L1 = true;
            L2 = true;
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

            ////Player movement
            //player.PlayerMovement(3);

            
            //if (i <= 2)
            //{
            //    enemy.SpawnEnemy();
            //    i++;
            //}
            enemy.Update(gameTime);
            base.Update(gameTime);
        }

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
                spriteBatch.DrawString(Font, $"{Player.Name}\nHealth: {Player.health}\nDamage: {Player.damage}\nGold: {Player.coin}\nFood: {Player.food}\nScore: {Player.score}", new Vector2(1735, 0), Color.White);

                //Test mouse position
                spriteBatch.DrawString(Font, $"MouseX: {Mouse.GetState().X}\nMouseY: {Mouse.GetState().Y}", new Vector2(1735, 800), Color.White);

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
