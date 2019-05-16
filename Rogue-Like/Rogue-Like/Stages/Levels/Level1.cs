using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Rogue_Like
{
    public class Level1 : State
    {
        private SpriteFont Font;
        private Texture2D _playerTexture;


        private List<Component> _component;
        //Tilemap of Lake Map
        private int[,] map = new int[,]
       {
            {0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0},
            {0,2,2,2,2,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2,2,2,2,2,2,0},
            {0,2,1,2,2,2,2,2,2,2,2,1,2,0,2,1,2,2,2,2,2,2,2,2,1,2,0},
            {0,2,2,2,2,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2,2,2,2,2,2,0},
            {0,2,2,2,2,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2,2,2,2,2,2,0},
            {0,2,2,2,2,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2,2,2,2,2,2,0},
            {0,2,1,2,2,2,2,2,2,2,2,1,2,0,2,1,2,2,2,2,2,2,2,2,1,2,0},
            {0,2,2,2,2,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2,2,2,2,2,2,0},
            {0,0,0,0,0,0,2,2,0,0,0,0,0,0,0,0,0,0,0,0,2,2,0,0,0,0,0},
            {0,2,2,2,2,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2,2,2,2,2,2,0},
            {0,2,1,2,2,2,2,2,2,2,2,1,2,0,2,1,2,2,2,2,2,2,2,2,1,2,0},
            {0,2,2,2,2,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2,2,2,2,2,2,0},
            {0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0},
            {0,2,2,2,2,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2,2,2,2,2,2,0},
            {0,2,1,2,2,2,2,2,2,2,2,1,2,0,2,1,2,2,2,2,2,2,2,2,1,2,0},
            {0,2,2,2,2,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2,2,2,2,2,2,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},

       };
        public int GetIndex(int cellX, int cellY)
        {
            if (cellX < 0 || cellX > Width - 1 || cellY < 0 || cellY > Height - 1)
                return 0;

            return map[cellY, cellX];
        }
        private List<Texture2D> tileTextures = new List<Texture2D>();
        //add Textures to the lake map
        public void AddTexture(Texture2D texture)
        {
            tileTextures.Add(texture);
        }
        //The Width of Lake map
        public int Width
        {
            get { return map.GetLength(1); }

        }
        //Height of Lake map
        public int Height
        {
            get { return map.GetLength(0); }
        }

        public int Ground
        {
            get { return map.GetLength(2); }
        }

        public int DoorFront
        {
            get { return map.GetLength(3); }
        }

        public int DoorSide
        {
            get { return map.GetLength(4); }
        }

        public Level1(GameWorld gameWorld, GraphicsDevice graphicsDevice, ContentManager content) : base(gameWorld, graphicsDevice, content)
        {
            var buttonTexture = content.Load<Texture2D>("Button");
            var buttonFont = content.Load<SpriteFont>("Font");
            Font = content.Load<SpriteFont>("Font");
            Texture2D piller = content.Load<Texture2D>("Pillar1");
            Texture2D wall = content.Load<Texture2D>("Wall");
            Texture2D ground = content.Load<Texture2D>("Ground");
            Texture2D DoorFront = content.Load<Texture2D>("DoorFront1");
            Texture2D Shop = content.Load<Texture2D>("Shop");
            _playerTexture = content.Load<Texture2D>("Fisher_Bob");



            var shop = new Button(Shop, buttonFont)
            {
                Position = new Vector2(100, 200),

            };
            shop.Click += Shop_Click;

            AddTexture(wall);
            AddTexture(piller);
            AddTexture(ground);
            AddTexture(DoorFront);


            _component = new List<Component>()
            {
                shop,

            };

        }

        private void Shop_Click(object sender, EventArgs e)
        {
            _gameWorld.ChangeState(new Shop(_gameWorld, _graphichsDevice, _content));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    int textureIndex = map[y, x];
                    if (textureIndex == -1)
                    {
                        continue;
                    }
                    Texture2D texture = tileTextures[textureIndex];
                    spriteBatch.Draw(texture, new Rectangle(x * 64, y * 64, 64, 64), Color.White);
                }

            }
            foreach (var component in _component)
            {
                component.Draw(gameTime, spriteBatch);
            }
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                _gameWorld.ChangeState(new Menu(_gameWorld, _graphichsDevice, _content));
            }
            foreach (var component in _component)
            {
                component.Update(gameTime);
            }
        }
    }
}