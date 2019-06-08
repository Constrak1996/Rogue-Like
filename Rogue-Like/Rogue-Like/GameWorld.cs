using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading;

namespace Rogue_Like
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameWorld : Game
    {
        private static GameWorld instance = null;
        public static bool isPlaying;
        Controller controller = new Controller();
        GraphicsDeviceManager graphics;
        private int playerDeathCount;
        SpriteBatch spriteBatch;
        SpriteFont Font;
        private TimeSpan timeSinceStart;
        private TimeSpan enteredRoom;
        private float time;
        private State _currentState;
        private State _nextState;
        private float roomTime;
        //Collision boxes for encasing the player in the map and to interact with doors.
        #region Rectangles
        public Rectangle topLineDoor;
        public Rectangle shopTopLineDoor;
        public Rectangle bottomLineDoor;
        public Rectangle rightLineDoor;
        public Rectangle leftLineDoor;
        public Rectangle topLine1;
        public Rectangle topLine2;
        public Rectangle bottomLine1;
        public Rectangle bottomLine2;
        public Rectangle rightLine1;
        public Rectangle rightLine2;
        public Rectangle leftLine1;
        public Rectangle leftLine2;
        public Rectangle topLine;
        public Rectangle leftLine;
        public Rectangle bottomLine;
        public Rectangle rightLine;
        public Rectangle leftTopCollideable1;
        public Rectangle leftTopCollideable2;
        public Rectangle leftTopCollideable3;
        public Rectangle leftTopCollideable4;
        public Rectangle rightTopCollideable1;
        public Rectangle rightTopCollideable2;
        public Rectangle rightTopCollideable3;
        public Rectangle rightTopCollideable4;
        public Rectangle leftBotCollideable1;
        public Rectangle leftBotCollideable2;
        public Rectangle leftBotCollideable3;
        public Rectangle leftBotCollideable4;
        public Rectangle rightBotCollideable1;
        public Rectangle rightBotCollideable2;
        public Rectangle rightBotCollideable3;
        public Rectangle rightBotCollideable4;
        #endregion

        public static bool isShop;
        public static bool isMap1 = false;
        public static bool isMap2 = false;
        public static bool isMap3 = false;
        public static bool isNextLevelRoom = false;
        public static bool Pitroom = false;

        private static ContentManager _content;
        public static ContentManager ContentManager { get => _content; }

        //The lists used for loading and removing items
        public static List<GameObject> gameObjects = new List<GameObject>();
        public static List<GameObject> gameObjectsAdd = new List<GameObject>();
        public static List<GameObject> gameObjectsRemove = new List<GameObject>();

        //Graphics
        public static int Width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public static int Height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        //Dungeon rooms
        public Texture2D shop;
        public Texture2D level1;
        public Texture2D level2;


        //Player
        public static Player player;

        //Shopitems
        ShopItem shopItems;

        //Collision
        private Texture2D collisionTexture;

        //Enemy
        Enemy enemy;
        private int i;

        //Level bools
        public static bool level_1;
        public static bool level_2;
        public static bool level_3;
        public static bool level_4;

        //Room bools
        public static bool room1;
        public static bool room2;
        public static bool room3;
        public static bool room4;

        //Random static
        public static Random r = new Random();

        //Spawn once checks
        public static bool L1;
        public static bool L2;

        //Soundeffects
        public static SoundEffect playerMeleeSound;
        public static SoundEffect playerRangedSound;
        public static SoundEffect moneyPickupSound;

        //Music
        public static Song backTrack;
        public static GameWorld Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameWorld();
                }
                return instance;
            }

        }

        public static bool NewGame = false;

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
            enemy = new Enemy("Worker", new Transform(new Vector2(0, 0), 0));

            //Player
            player = new Player("SwordBob", new Transform(new Vector2(700, 200), 0));
            gameObjectsAdd.Add(player);

            //Instansiate shop
            shopItems = new ShopItem("worker", new Transform(new Vector2(0, 0), 0));

            //Soundeffects
            playerMeleeSound = Content.Load<SoundEffect>("SwordSlashSound");
            playerRangedSound = Content.Load<SoundEffect>("PlayerBulletThrow");
            moneyPickupSound = Content.Load<SoundEffect>("MoneyPickup");

            //Level bools running once
            L1 = true;
            L2 = true;

            Menu.newgame = true;
            Menu.resume = false;
            EndScreen.endScreen = false;
        }
        public void CALLLOADCONT()
        {
            LoadContent();
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

            enteredRoom += gameTime.ElapsedGameTime;
            roomTime = enteredRoom.Seconds;
            Restart();


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
            player.PlayerMovement(8);

            //Check if gameobject is colliding, if it does run collision code
            foreach (GameObject go in gameObjects)
            {
                go.Update(gameTime);

                foreach (GameObject other in gameObjects)
                {
                    if (go != other && go.IsColliding(other))
                    {
                        go.DoCollision(other);
                    }
                }
            }
            enemy.Update(gameTime);
            base.Update(gameTime);

            if (player.hitBox.Intersects(bottomLineDoor) & _currentState is Shop)
            {
                _nextState = new Room1(this, GraphicsDevice, Content);
                player.Transform = new Transform(new Vector2(865, 150), 1);
                isShop = false;
                isMap1 = true;
            }

            if (player.hitBox.Intersects(bottomLineDoor) & _currentState is Shop)
            {
                _nextState = new Room1(this, GraphicsDevice, Content);
                player.Transform = new Transform(new Vector2(865, 150), 1);
                isShop = false;
                isMap1 = true;
            }

            if (player.hitBox.Intersects(topLineDoor) && _currentState is Shop)
            {
                _nextState = new Menu(this, GraphicsDevice, Content);

            }

            if (player.hitBox.Intersects(topLineDoor) && _currentState is Room1)
            {
                _nextState = new Shop(this, GraphicsDevice, Content);
                player.Transform = new Transform(new Vector2(865, 910), 1);
                isMap1 = false;
                isShop = true;
                ShopItem.spawnItem1 = true;
                ShopItem.spawnItem2 = true;
                ShopItem.spawnItem3 = true;
                ShopItem.spawnItem4 = true;
                ShopItem.spawnItem5 = true;
                ShopItem.spawnitem6 = true;
            }

            if (player.hitBox.Intersects(bottomLineDoor) && _currentState is Room1)
            {
                _nextState = new Room2(this, GraphicsDevice, Content);
                player.Transform = new Transform(new Vector2(865, 150), 1);
                isMap1 = false;
                isMap2 = true;
            }

            if (player.hitBox.Intersects(topLineDoor) && _currentState is Room2)
            {
                _nextState = new Room1(this, GraphicsDevice, Content);
                player.Transform = new Transform(new Vector2(865, 910), 1);
                isMap1 = true;
                isMap2 = false;
            }

            if (player.hitBox.Intersects(rightLineDoor) && _currentState is Room2)
            {
                _nextState = new Room3(this, GraphicsDevice, Content);
                player.Transform = new Transform(new Vector2(147, 545), 1);
                isMap3 = true;
                isMap2 = false;
            }

            if (player.hitBox.Intersects(leftLineDoor) && _currentState is Room3)
            {
                _nextState = new Room2(this, GraphicsDevice, Content);
                player.Transform = new Transform(new Vector2(1585, 545), 1);
                isMap3 = false;
                isMap2 = true;
            }

            if (player.hitBox.Intersects(topLineDoor) & _currentState is Room3)
            {
                _nextState = new NextLevelRoom(this, GraphicsDevice, Content);
                player.Transform = new Transform(new Vector2(865, 910), 1);
                isNextLevelRoom = true;
                isMap3 = false;
            }

            if (player.hitBox.Intersects(bottomLineDoor) & _currentState is NextLevelRoom)
            {
                _nextState = new Room3(this, GraphicsDevice, Content);
                player.Transform = new Transform(new Vector2(865, 150), 1);
                isNextLevelRoom = false;
                isMap3 = true;
            }

            if (player.hitBox.Intersects(topLineDoor) & _currentState is NextLevelRoom)
            {
                _nextState = new Shop_Level1(this, GraphicsDevice, Content);
                player.Transform = new Transform(new Vector2(865, 150), 1);
                isShop = true;
                isNextLevelRoom = false;
            }

            if (isShop == true)
            {
                shopItems.Update(gameTime);
            }
        }

        public void Restart()
        {

            if (NewGame)
            {
                LoadContent();
                NewGame = false;
            }
            else if (Player.currentHealth <= 0 && isPlaying)
            {

                playerDeathCount++;
                if (playerDeathCount >= 2)
                {
                    ChangeState(new EndScreen(this, GraphicsDevice, Content));
                    foreach (GameObject enemy in gameObjects)
                    {
                        gameObjectsRemove.Add(enemy);
                    }
                    isPlaying = false;
                    playerDeathCount = 0;
                }
                else
                {
                    foreach (GameObject enemy in gameObjects)
                    {
                        gameObjectsRemove.Add(enemy);
                    }
                    int tempFood = Player.Food;
                    int tempHealth = Player.maxHealth;

                    LoadContent();

                    Player.Food = tempFood;
                    Player.maxHealth = tempHealth;

                    if (Player.Food <= 3)
                    {
                        Player.maxHealth -= 2;
                        Player.currentHealth = Player.maxHealth;
                    }
                    else
                    {
                        Player.maxHealth += 2;
                        Player.currentHealth = Player.maxHealth;
                    }

                    Menu.newgame = false;
                    Menu.resume = true;

                    _nextState = new Shop(this, GraphicsDevice, Content);

                }


            }
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
            if (Menu.menu == false)
            {
                if (Shop.shop == true)
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

                        //DungeonCollisionBox();
                        if (_currentState is Shop || _currentState is Shop_Level1)
                        {
                            ShopCollisionBox();
                            ShopDoorCollision();
                            CornersCollideableObjects();
                        }

                        //AllDoorCollision();

                        if (_currentState is Room1)
                        {

                            TopDoorCollision();
                            BottomDoorCollision();
                            CornersCollideableObjects();

                        }

                        if (_currentState is Room2)
                        {
                            TopDoorCollision();
                            RightDoorCollision();
                            TopAndRightCollisionBox();

                        }

                        if (_currentState is Room3)
                        {
                            TopDoorCollision();
                            LeftDoorCollision();
                            TopAndLeftCollisionBox();
                            LeftSideCollideableObjects1();
                            LeftSideCollideableObjects2();
                            CornersCollideableObjects();
                        }

                        if (_currentState is NextLevelRoom)
                        {
                            TopDoorCollision();
                            BottomDoorCollision();
                            NextLevelRoomCollisionBox();
                        }
#endif
                    }
                }

                if (player.hitBox.Intersects(topLine) || player.hitBox.Intersects(topLine1) || player.hitBox.Intersects(topLine2))
                {
                    player.Transform.Position.Y = 105;
                }

                if (player.hitBox.Intersects(rightLine) || player.hitBox.Intersects(rightLine1) || player.hitBox.Intersects(rightLine2))
                {
                    if (_currentState is Shop || _currentState is Room1 || _currentState is Room2 || _currentState is Room3)
                    {
                        player.Transform.Position.X = 1567;
                    }

                    if (_currentState is NextLevelRoom)
                    {
                        player.Transform.Position.X = 914;
                    }

                }

                if (player.hitBox.Intersects(bottomLine) || player.hitBox.Intersects(bottomLine1) || player.hitBox.Intersects(bottomLine2))
                {
                    player.Transform.Position.Y = 930;
                }

                if (player.hitBox.Intersects(leftLine) || player.hitBox.Intersects(leftLine1) || player.hitBox.Intersects(leftLine2))
                {
                    if (_currentState is Shop || _currentState is Room1 || _currentState is Room2 || _currentState is Room3)
                    {
                        player.Transform.Position.X = 155;
                    }

                    if (_currentState is NextLevelRoom)
                    {
                        player.Transform.Position.X = 770;
                    }
                }

                spriteBatch.DrawString(Font, $"{Player.name}\nHealth: {Player.currentHealth}\nAmmo: {Player.bulletCount}\nDamage: {Player.meleeDamage}\nRangeDamage: {Player.rangedDamage}\nGold: {Player.Coin}\nFood: {Player.Food}\nScore: {Player.dataScore}", new Vector2(1735, 0), Color.White);
#if DEBUG
                spriteBatch.DrawString(Font, $"Mouse X: {Mouse.GetState().X.ToString()}\nMouse Y: {Mouse.GetState().Y.ToString()}", new Vector2(1735, 500), Color.White);
#endif
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }
        #region CollisionDrawings
        private void DrawCollisionBox(GameObject go)
        {
            //Creating a box around the object
            Rectangle collisionBox = go.hitBox;

            //Defining each side
            Rectangle topLine = new Rectangle(collisionBox.Center.X - collisionBox.Width, collisionBox.Center.Y - collisionBox.Height, collisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(collisionBox.Center.X - collisionBox.Width, collisionBox.Center.Y, collisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(collisionBox.Center.X, collisionBox.Center.Y - collisionBox.Height, 1, collisionBox.Height);
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
            topLine1 = new Rectangle(108, 75, 725, 1);
            topLine2 = new Rectangle(896, 75, 723, 1);
            Rectangle topLineDoor1 = new Rectangle(832, 30, 63, 1);
            Rectangle topLineDoor2 = new Rectangle(832, 30, 1, 45);
            Rectangle topLineDoor3 = new Rectangle(895, 30, 1, 45);
            bottomLine1 = new Rectangle(108, 980, 724, 1);
            bottomLine2 = new Rectangle(896, 980, 723, 1);
            Rectangle bottomLineDoor1 = new Rectangle(832, 1005, 63, 1);
            Rectangle bottomLineDoor2 = new Rectangle(832, 960, 1, 45);
            Rectangle bottomLineDoor3 = new Rectangle(895, 960, 1, 45);
            rightLine1 = new Rectangle(1618, 75, 1, 437);
            rightLine2 = new Rectangle(1618, 576, 1, 405);
            Rectangle rightLineDoor1 = new Rectangle(1670, 512, 1, 63);
            Rectangle rightLineDoor2 = new Rectangle(1618, 512, 53, 1);
            Rectangle rightLineDoor3 = new Rectangle(1618, 575, 53, 1);
            leftLine1 = new Rectangle(108, 75, 1, 437);
            leftLine2 = new Rectangle(108, 576, 1, 405);
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
            topLine1 = new Rectangle(108, 105, 724, 1);
            topLine2 = new Rectangle(925, 105, 723, 1);
            Rectangle topLineDoor1 = new Rectangle(832, 30, 63, 1);
            Rectangle topLineDoor2 = new Rectangle(832, 30, 1, 45);
            Rectangle topLineDoor3 = new Rectangle(895, 30, 1, 45);
            bottomLine1 = new Rectangle(108, 980, 724, 1);
            bottomLine2 = new Rectangle(896, 980, 723, 1);
            Rectangle bottomLineDoor1 = new Rectangle(832, 1005, 63, 1);
            Rectangle bottomLineDoor2 = new Rectangle(832, 960, 1, 45);
            Rectangle bottomLineDoor3 = new Rectangle(895, 960, 1, 45);
            rightLine = new Rectangle(1618, 105, 1, 876);
            leftLine = new Rectangle(158, 105, 1, 876);

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
            spriteBatch.Draw(collisionTexture, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);

            if (isShop == true)
            {
                bottomLine.Y = -1000;
                topLine.Y = -1000;
                leftLine.Y = 105;
                rightLine.Y = 105;
            }

        }

        private void TopAndRightCollisionBox()
        {
            //Defining each side
            topLine1 = new Rectangle(108, 105, 720, 1);
            topLine2 = new Rectangle(890, 105, 723, 1);
            Rectangle topLineDoor1 = new Rectangle(832, 30, 63, 1);
            Rectangle topLineDoor2 = new Rectangle(832, 30, 1, 45);
            Rectangle topLineDoor3 = new Rectangle(895, 30, 1, 45);
            bottomLine = new Rectangle(108, 980, 1620, 1);
            rightLine1 = new Rectangle(1618, 75, 1, 410);
            rightLine2 = new Rectangle(1618, 610, 1, 405);
            Rectangle rightLineDoor1 = new Rectangle(1670, 512, 1, 63);
            Rectangle rightLineDoor2 = new Rectangle(1618, 512, 53, 1);
            Rectangle rightLineDoor3 = new Rectangle(1618, 575, 53, 1);
            leftLine = new Rectangle(158, 105, 1, 906);


            //Draw each side
            spriteBatch.Draw(collisionTexture, topLine1, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, topLine2, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, topLineDoor1, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, topLineDoor2, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, topLineDoor3, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, rightLine1, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, rightLine2, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, rightLineDoor1, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, rightLineDoor2, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, rightLineDoor3, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);

            if (isMap2 == true)
            {
                rightLine.Y = -1000;
                topLine.Y = -1000;
                leftLine.Y = 75;
                bottomLine.Y = 980;
            }
        }

        private void TopAndLeftCollisionBox()
        {
            //Defining each side
            topLine1 = new Rectangle(108, 75, 725, 1);
            topLine2 = new Rectangle(896, 75, 723, 1);
            Rectangle topLineDoor1 = new Rectangle(832, 30, 63, 1);
            Rectangle topLineDoor2 = new Rectangle(832, 30, 1, 45);
            Rectangle topLineDoor3 = new Rectangle(895, 30, 1, 45);
            bottomLine = new Rectangle(108, 980, 1620, 1);
            rightLine = new Rectangle(1618, 75, 1, 906);
            leftLine1 = new Rectangle(108, 75, 1, 437);
            leftLine2 = new Rectangle(108, 576, 1, 405);
            Rectangle leftLineDoor1 = new Rectangle(50, 512, 1, 63);
            Rectangle leftLineDoor2 = new Rectangle(50, 512, 59, 1);
            Rectangle leftLineDoor3 = new Rectangle(50, 575, 59, 1);


            //Draw each side
            spriteBatch.Draw(collisionTexture, topLine1, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, topLine2, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, topLineDoor1, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, topLineDoor2, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, topLineDoor3, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftLine1, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftLine2, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftLineDoor1, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftLineDoor2, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftLineDoor3, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);

            if (isMap3 == true)
            {
                bottomLine.Y = 980;
                rightLine.Y = 75;
                leftLine.Y = -1000;
                topLine.Y = -1000;
            }
        }

        private void ShopDoorCollision()
        {
            //Defining each side
            topLineDoor = new Rectangle(833, 95, 62, 1);
            bottomLineDoor = new Rectangle(833, 974, 62, 1);

            //Draw each side
            spriteBatch.Draw(collisionTexture, topLineDoor, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, bottomLineDoor, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        private void AllDoorCollision()
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

        private void TopDoorCollision()
        {
            //Defining each side
            topLineDoor = new Rectangle(833, 95, 62, 1);

            //Draw each side
            spriteBatch.Draw(collisionTexture, topLineDoor, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        private void LeftDoorCollision()
        {
            //Defining each side
            leftLineDoor = new Rectangle(92, 512, 1, 63);

            //Draw each side
            spriteBatch.Draw(collisionTexture, leftLineDoor, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        private void BottomDoorCollision()
        {
            //Defining each side
            bottomLineDoor = new Rectangle(833, 974, 62, 1);

            //Draw each side
            spriteBatch.Draw(collisionTexture, bottomLineDoor, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        private void RightDoorCollision()
        {
            //Defining each side
            rightLineDoor = new Rectangle(1635, 512, 1, 63);

            //Draw each side
            spriteBatch.Draw(collisionTexture, rightLineDoor, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        private void NextLevelRoomCollisionBox()
        {

            //Defining each side
            topLine1 = new Rectangle(108, 105, 724, 1);
            topLine2 = new Rectangle(925, 105, 723, 1);
            Rectangle topLineDoor1 = new Rectangle(832, 30, 63, 1);
            Rectangle topLineDoor2 = new Rectangle(832, 30, 1, 45);
            Rectangle topLineDoor3 = new Rectangle(895, 30, 1, 45);
            bottomLine1 = new Rectangle(108, 980, 724, 1);
            bottomLine2 = new Rectangle(896, 980, 723, 1);
            Rectangle bottomLineDoor1 = new Rectangle(832, 1005, 63, 1);
            Rectangle bottomLineDoor2 = new Rectangle(832, 960, 1, 45);
            Rectangle bottomLineDoor3 = new Rectangle(895, 960, 1, 45);
            rightLine = new Rectangle(959, 105, 1, 876);
            leftLine = new Rectangle(767, 105, 1, 876);

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
            spriteBatch.Draw(collisionTexture, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(collisionTexture, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 0);

            if (isNextLevelRoom == true)
            {
                bottomLine.Y = -1000;
                topLine.Y = -1000;
                rightLine.Y = 105;
                leftLine.Y = 105;
            }
        }

        private void LeftSideCollideableObjects1()
        {
            Rectangle leftCollideable1 = new Rectangle(128, 450, 1, 64);
            Rectangle leftCollideable2 = new Rectangle(128, 450, 64, 1);
            Rectangle leftCollideable3 = new Rectangle(128, 513, 64, 1);
            Rectangle leftCollideable4 = new Rectangle(192, 450, 1, 64);

            spriteBatch.Draw(collisionTexture, leftCollideable1, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftCollideable2, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftCollideable3, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftCollideable4, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);


        }

        private void LeftSideCollideableObjects2()
        {
            Rectangle leftCollideable1 = new Rectangle(128, 578, 1, 64);
            Rectangle leftCollideable2 = new Rectangle(128, 578, 64, 1);
            Rectangle leftCollideable3 = new Rectangle(128, 641, 64, 1);
            Rectangle leftCollideable4 = new Rectangle(192, 578, 1, 64);

            spriteBatch.Draw(collisionTexture, leftCollideable1, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftCollideable2, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftCollideable3, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftCollideable4, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);


        }

        private void CornersCollideableObjects()
        {
            leftTopCollideable1 = new Rectangle(128, 128, 1, 64);
            leftTopCollideable2 = new Rectangle(128, 128, 64, 1);
            leftTopCollideable3 = new Rectangle(128, 192, 64, 1);
            leftTopCollideable4 = new Rectangle(192, 128, 1, 64);

            rightTopCollideable1 = new Rectangle(1536, 128, 1, 64);
            rightTopCollideable2 = new Rectangle(1536, 128, 64, 1);
            rightTopCollideable3 = new Rectangle(1536, 192, 64, 1);
            rightTopCollideable4 = new Rectangle(1600, 128, 1, 64);

            leftBotCollideable1 = new Rectangle(128, 896, 1, 64);
            leftBotCollideable2 = new Rectangle(128, 896, 64, 1);
            leftBotCollideable3 = new Rectangle(128, 960, 64, 1);
            leftBotCollideable4 = new Rectangle(192, 896, 1, 64);

            rightBotCollideable1 = new Rectangle(1536, 896, 1, 64);
            rightBotCollideable2 = new Rectangle(1536, 896, 64, 1);
            rightBotCollideable3 = new Rectangle(1536, 960, 64, 1);
            rightBotCollideable4 = new Rectangle(1600, 896, 1, 64);

            spriteBatch.Draw(collisionTexture, leftTopCollideable1, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftTopCollideable2, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftTopCollideable3, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftTopCollideable4, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);

            spriteBatch.Draw(collisionTexture, rightTopCollideable1, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, rightTopCollideable2, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, rightTopCollideable3, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, rightTopCollideable4, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);

            spriteBatch.Draw(collisionTexture, leftBotCollideable1, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftBotCollideable2, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftBotCollideable3, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftBotCollideable4, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);

            spriteBatch.Draw(collisionTexture, rightBotCollideable1, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, rightBotCollideable2, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, rightBotCollideable3, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, rightBotCollideable4, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);



        }
    }
    #endregion
}
