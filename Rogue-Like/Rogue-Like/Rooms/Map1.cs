using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rogue_Like
{
    public class Map1 : State
    {
        private Player player;
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

        public Map1(GameWorld gameWorld, GraphicsDevice graphicsDevice, ContentManager content) : base(gameWorld, graphicsDevice, content)
        {
            Font = content.Load<SpriteFont>("Font");
            Texture2D piller = content.Load<Texture2D>("Pillar1");
            Texture2D wall = content.Load<Texture2D>("Wall");
            Texture2D ground = content.Load<Texture2D>("Ground");
            Texture2D DoorFront = content.Load<Texture2D>("DoorFront1");
            _playerTexture = content.Load<Texture2D>("Fisher_Bob");
            player = new Player(_playerTexture, "Fisher_Bob", content, Player.playerTransform);
            AddTexture(wall);
            AddTexture(piller);
            AddTexture(ground);
            AddTexture(DoorFront);

            
            _component = new List<Component>()
            {
                

            };

        }
        
        /// <summary>
        /// Draws the Lake
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spritebatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            spritebatch.Begin();
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
                    spritebatch.Draw(texture, new Rectangle(x * 64, y * 64, 64, 64), Color.White);
                }

            }
            foreach (var component in _component)
            {
                component.Draw(gameTime, spritebatch);
            }
            //Draws the player
            {
                //spritebatch.Draw(_playerTexture, new Vector2(450, 80), Color.White); //draws the player and his position
                player.Draw(spritebatch);
            }

            spritebatch.End();
        }
        //Allows a NextStage Event to happen
        

        public override void PostUpdate(GameTime gameTime)
        {

            //remove sprite if they are not needen no more
        }
        /// <summary>
        /// Update the lake map
        /// </summary>
        /// <param name="gameTime"></param>
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

            player.Update(gameTime);
        }
        
    }
}