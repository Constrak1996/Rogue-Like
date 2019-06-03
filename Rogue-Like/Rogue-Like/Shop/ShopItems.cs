using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Rogue_Like
{
    class ShopItems : GameObject
    {
        private string[] shopItems = { "Slingshot", "Armor of The Gods", "Stick", "Sword", "Club", "Rock"};

        public override Rectangle Hitbox => base.Hitbox;

        public ShopItems(string spriteName, Transform Transform) : base(spriteName, Transform)
        {
            spriteName = shopItems[GameWorld.r.Next(0, 4)];
        }
        
        public override void Update(GameTime gameTime)
        {
            SpawnShopItems();

            base.Update(gameTime);
        }

        /// <summary>
        /// Collision with shop items
        /// </summary>
        /// <param name="otherObject"></param>
        public override void DoCollision(GameObject otherObject)
        {
            if (otherObject is Player && this.spriteName == "Slingshot" && Keyboard.GetState().IsKeyDown(Keys.E))
            {

            }

            if (otherObject is Player && this.spriteName == "Armor of The Gods" && Keyboard.GetState().IsKeyDown(Keys.E))
            {

            }

            if (otherObject is Player && this.spriteName == "Stick" && Keyboard.GetState().IsKeyDown(Keys.E))
            {

            }

            if (otherObject is Player && this.spriteName == "Sword" && Keyboard.GetState().IsKeyDown(Keys.E))
            {

            }

            if (otherObject is Player && this.spriteName == "Club" && Keyboard.GetState().IsKeyDown(Keys.E))
            {

            }

            if (otherObject is Player && this.spriteName == "Rock" && Keyboard.GetState().IsKeyDown(Keys.E))
            {

            }
            base.DoCollision(otherObject);
        }

        /// <summary>
        /// Spawn shot items at locations
        /// </summary>
        public void SpawnShopItems()
        {
            //Item positions
            Vector2 pos1 = new Vector2(353, 386);
            GameWorld.gameObjectsAdd.Add(new ShopItems(spriteName, new Transform(pos1, 0)));

            Vector2 pos2 = new Vector2(542, 386);
            GameWorld.gameObjectsAdd.Add(new ShopItems(spriteName, new Transform(pos2, 0)));

            Vector2 pos3 = new Vector2(350, 701);
            GameWorld.gameObjectsAdd.Add(new ShopItems(spriteName, new Transform(pos3, 0)));

            Vector2 pos4 = new Vector2(544, 701);
            GameWorld.gameObjectsAdd.Add(new ShopItems(spriteName, new Transform(pos4, 0)));

            Vector2 pos5 = new Vector2(1183, 388);
            GameWorld.gameObjectsAdd.Add(new ShopItems(spriteName, new Transform(pos5, 0)));

            Vector2 pos6 = new Vector2(1377, 388);
            GameWorld.gameObjectsAdd.Add(new ShopItems(spriteName, new Transform(pos6, 0)));
        }
    }
}
