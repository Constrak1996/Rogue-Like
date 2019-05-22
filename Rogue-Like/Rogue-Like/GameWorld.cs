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
        public Rectangle topLineDoor;
        public Rectangle bottomLineDoor;
        public Rectangle rightLineDoor;
        public Rectangle leftLineDoor;

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
        Player player;

        //Collision
        private Texture2D collisionTexture;

        //Enemy
        Enemy enemy;

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

            //Player
            player = new Player("Fisher_Bob", new Transform(new Vector2(400, 50), 0));
            gameObjectsAdd.Add(player);

            enemy = new Enemy("Worker", new Transform(new Vector2(0, 0), 0), 0);
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
                go.Update();
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

            //Player movement
            player.PlayerMovement(3);

            enemy.Update();
            base.Update(gameTime);

            if (player.Hitbox.Intersects(topLineDoor) || player.Hitbox.Intersects(bottomLineDoor) || player.Hitbox.Intersects(rightLineDoor) || player.Hitbox.Intersects(leftLineDoor))
            {
                
            }
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
                DungeonCollisionBox();
                //ShopCollisionBox();
                DoorCollision();
#endif
            }
            spriteBatch.DrawString(Font, $"Player Name: {player.Name} Health: {Player.health} Damage: {Player.damage}", new Vector2(0, 20), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawCollisionBox(GameObject go)
        {
            //Creating a box around the object
            Rectangle collisionBox = go.Hitbox;
            
            //Defining each side
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
        
        private void DungeonCollisionBox()
        {
            //Defining each side
            Rectangle topLine1 = new Rectangle(108, 75, 725, 1);
            Rectangle topLine2 = new Rectangle(896, 75, 723, 1);
            Rectangle topLineDoor1 = new Rectangle(832, 30, 63, 1);
            Rectangle topLineDoor2 = new Rectangle(832, 30, 1, 45);
            Rectangle topLineDoor3 = new Rectangle(895, 30, 1, 45);
            Rectangle bottomLine1 = new Rectangle(108, 960, 724, 1);
            Rectangle bottomLine2 = new Rectangle(896, 960, 723, 1);
            Rectangle bottomLineDoor1 = new Rectangle(832, 1005, 63, 1);
            Rectangle bottomLineDoor2 = new Rectangle(832, 960, 1, 45);
            Rectangle bottomLineDoor3 = new Rectangle(895, 960, 1, 45);
            Rectangle rightLine1 = new Rectangle(1618, 75, 1, 437);
            Rectangle rightLine2 = new Rectangle(1618, 576, 1, 385);
            Rectangle rightLineDoor1 = new Rectangle(1670, 512, 1, 63);
            Rectangle rightLineDoor2 = new Rectangle(1618, 512, 53, 1);
            Rectangle rightLineDoor3 = new Rectangle(1618, 575, 53, 1);
            Rectangle leftLine1 = new Rectangle(108, 75, 1, 437);
            Rectangle leftLine2 = new Rectangle(108, 576, 1, 385);
            Rectangle leftLineDoor1 = new Rectangle(50, 512, 1, 63);
            Rectangle leftLineDoor2 = new Rectangle(50, 512, 59, 1);
            Rectangle leftLineDoor3 = new Rectangle(50, 575, 59, 1);

            //Draw each side
            spriteBatch.Draw(collisionTexture, topLine1, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, topLine2, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, topLineDoor1, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, topLineDoor2, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, topLineDoor3, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, bottomLine1, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, bottomLine2, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, bottomLineDoor1, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, bottomLineDoor2, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, bottomLineDoor3, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, rightLine1, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, rightLine2, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, rightLineDoor1, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, rightLineDoor2, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, rightLineDoor3, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftLine1, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftLine2, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftLineDoor1, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftLineDoor2, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftLineDoor3, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        private void ShopCollisionBox()
        {
            //Defining each side
            Rectangle topLine1 = new Rectangle(108, 75, 725, 1);
            Rectangle topLine2 = new Rectangle(896, 75, 723, 1);
            Rectangle topLineDoor1 = new Rectangle(832, 30, 63, 1);
            Rectangle topLineDoor2 = new Rectangle(832, 30, 1, 45);
            Rectangle topLineDoor3 = new Rectangle(895, 30, 1, 45);
            Rectangle bottomLine1 = new Rectangle(108, 960, 724, 1);
            Rectangle bottomLine2 = new Rectangle(896, 960, 723, 1);
            Rectangle bottomLineDoor1 = new Rectangle(832, 1005, 63, 1);
            Rectangle bottomLineDoor2 = new Rectangle(832, 960, 1, 45);
            Rectangle bottomLineDoor3 = new Rectangle(895, 960, 1, 45);
            Rectangle rightLine1 = new Rectangle(1618, 75, 1, 886);
            Rectangle leftLine1 = new Rectangle(108, 75, 1, 886);

            //Draw each side
            spriteBatch.Draw(collisionTexture, topLine1, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, topLine2, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, topLineDoor1, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, topLineDoor2, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, topLineDoor3, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, bottomLine1, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, bottomLine2, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, bottomLineDoor1, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, bottomLineDoor2, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, bottomLineDoor3, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, rightLine1, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftLine1, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        private void DoorCollision()
        {
            //Defining each side
            topLineDoor = new Rectangle(833, 65, 62, 1);
            bottomLineDoor = new Rectangle(833, 974, 62, 1);
            rightLineDoor = new Rectangle(1635, 512, 1, 63);
            leftLineDoor = new Rectangle(92, 512, 1, 63);

            //Draw each side
            spriteBatch.Draw(collisionTexture, topLineDoor, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, bottomLineDoor, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, rightLineDoor, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftLineDoor, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
        }
    }
}
