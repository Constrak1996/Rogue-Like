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
        private string[] shopItems = { "Slingshot", "ArmorOfTheGods", "Stick", "Sword", "Club", "Rock"};
        private static bool spawnItem1 = true;
        private static bool spawnItem2 = true;
        private static bool spawnItem3 = true;
        private static bool spawnItem4 = true;
        private static bool spawnItem5 = true;
        private static bool spawnitem6 = true;

        public override Rectangle Hitbox => base.Hitbox;

        public ShopItems(string spriteName, Transform Transform) : base(spriteName, Transform)
        {
            this.spriteName = spriteName;
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
                GameWorld.gameObjectsRemove.Add(this);
            }

            if (otherObject is Player && this.spriteName == "ArmorOfTheGods" && Keyboard.GetState().IsKeyDown(Keys.E))
            {
                GameWorld.gameObjectsRemove.Add(this);
            }

            if (otherObject is Player && this.spriteName == "Stick" && Keyboard.GetState().IsKeyDown(Keys.E))
            {
                GameWorld.gameObjectsRemove.Add(this);
            }

            if (otherObject is Player && this.spriteName == "Sword" && Keyboard.GetState().IsKeyDown(Keys.E))
            {
                GameWorld.gameObjectsRemove.Add(this);
            }

            if (otherObject is Player && this.spriteName == "Club" && Keyboard.GetState().IsKeyDown(Keys.E))
            {
                GameWorld.gameObjectsRemove.Add(this);
            }

            if (otherObject is Player && this.spriteName == "Rock" && Keyboard.GetState().IsKeyDown(Keys.E))
            {
                GameWorld.gameObjectsRemove.Add(this);
            }
            base.DoCollision(otherObject);
        }

        /// <summary>
        /// Spawn shot items at locations
        /// </summary>
        public void SpawnShopItems()
        {
            if (spawnItem1)
            {
                spriteName = shopItems[GameWorld.r.Next(0, 5)];
                Vector2 pos1 = new Vector2(353, 386);
                GameWorld.gameObjectsAdd.Add(new ShopItems(spriteName, new Transform(pos1, 0)));
                spawnItem1 = false;
            }

            if (spawnItem2)
            {
                spriteName = shopItems[GameWorld.r.Next(0, 5)];
                Vector2 pos2 = new Vector2(542, 386);
                GameWorld.gameObjectsAdd.Add(new ShopItems(spriteName, new Transform(pos2, 0)));
                spawnItem2 = false;
            }

            if (spawnItem3)
            {
                spriteName = shopItems[GameWorld.r.Next(0, 5)];
                Vector2 pos3 = new Vector2(350, 701);
                GameWorld.gameObjectsAdd.Add(new ShopItems(spriteName, new Transform(pos3, 0)));
                spawnItem3 = false;
            }

            if (spawnItem4)
            {
                spriteName = shopItems[GameWorld.r.Next(0, 5)];
                Vector2 pos4 = new Vector2(544, 701);
                GameWorld.gameObjectsAdd.Add(new ShopItems(spriteName, new Transform(pos4, 0)));
                spawnItem4 = false;
            }

            if (spawnItem5)
            {
                spriteName = shopItems[GameWorld.r.Next(0, 5)];
                Vector2 pos5 = new Vector2(1183, 388);
                GameWorld.gameObjectsAdd.Add(new ShopItems(spriteName, new Transform(pos5, 0)));
                spawnItem5 = false;
            }

            if (spawnitem6)
            {
                spriteName = shopItems[GameWorld.r.Next(0, 5)];
                Vector2 pos6 = new Vector2(1377, 388);
                GameWorld.gameObjectsAdd.Add(new ShopItems(spriteName, new Transform(pos6, 0)));
                spawnitem6 = false;
            }
        }
    }
}
