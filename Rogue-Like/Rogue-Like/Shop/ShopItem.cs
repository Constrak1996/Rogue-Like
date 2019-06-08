using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Rogue_Like
{
    class ShopItem : GameObject
    {
        private string[] shopItems = { "Slingshot", "ArmorOfTheGods", "Stick", "Sword", "Club", "Rock"};
        public static bool spawnItem1 = true;
        public static bool spawnItem2 = true;
        public static bool spawnItem3 = true;
        public static bool spawnItem4 = true;
        public static bool spawnItem5 = true;
        public static bool spawnitem6 = true;

        public override Rectangle hitBox => base.hitBox;

        public ShopItem(string spriteName, Transform Transform) : base(spriteName, Transform)
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
            if (otherObject is Player && this.spriteName == "Slingshot" && Keyboard.GetState().IsKeyDown(Keys.E) && Player.Coin == 3)
            {
                GameWorld.gameObjectsRemove.Add(this);
                Player.meleeDamage++;
            }

            if (otherObject is Player && this.spriteName == "ArmorOfTheGods" && Keyboard.GetState().IsKeyDown(Keys.E) && Player.Coin == 3)
            {
                GameWorld.gameObjectsRemove.Add(this);
                Player.currentHealth++;
            }

            if (otherObject is Player && this.spriteName == "Stick" && Keyboard.GetState().IsKeyDown(Keys.E) && Player.Coin == 3)
            {
                GameWorld.gameObjectsRemove.Add(this);
                Player.meleeDamage++;
            }

            if (otherObject is Player && this.spriteName == "Sword" && Keyboard.GetState().IsKeyDown(Keys.E) && Player.Coin == 3)
            {
                GameWorld.gameObjectsRemove.Add(this);
                Player.meleeDamage += 3;
            }

            if (otherObject is Player && this.spriteName == "Club" && Keyboard.GetState().IsKeyDown(Keys.E) && Player.Coin == 4)
            {
                GameWorld.gameObjectsRemove.Add(this);
                Player.meleeDamage += 2;
            }

            if (otherObject is Player && this.spriteName == "Rock" && Keyboard.GetState().IsKeyDown(Keys.E) && Player.Coin == 3)
            {
                GameWorld.gameObjectsRemove.Add(this);
                Player.bulletCount += 5;
            }
            base.DoCollision(otherObject);
        }

        /// <summary>
        /// Spawn shot items at locations
        /// </summary>
        public void SpawnShopItems()
        {         
            if (spawnItem1 && GameWorld.isShop == true)
            {
                spriteName = shopItems[GameWorld.r.Next(0, 5)];
                Vector2 pos1 = new Vector2(353, 386);
                ShopItem item1 = new ShopItem(spriteName, new Transform(pos1, 0));
                GameWorld.gameObjectsAdd.Add(item1);
                spawnItem1 = false;
            }

            if (spawnItem2 && GameWorld.isShop == true)
            {
                spriteName = shopItems[GameWorld.r.Next(0, 5)];
                Vector2 pos2 = new Vector2(542, 386);
                ShopItem item2 = new ShopItem(spriteName, new Transform(pos2, 0));
                GameWorld.gameObjectsAdd.Add(item2);
                spawnItem2 = false;
            }

            if (spawnItem3 && GameWorld.isShop == true)
            {
                spriteName = shopItems[GameWorld.r.Next(0, 5)];
                Vector2 pos3 = new Vector2(350, 701);
                ShopItem item3 = new ShopItem(spriteName, new Transform(pos3, 0));
                GameWorld.gameObjectsAdd.Add(item3);
                spawnItem3 = false;
            }

            if (spawnItem4 && GameWorld.isShop == true)
            {
                spriteName = shopItems[GameWorld.r.Next(0, 5)];
                Vector2 pos4 = new Vector2(544, 701);
                ShopItem item4 = new ShopItem(spriteName, new Transform(pos4, 0));
                GameWorld.gameObjectsAdd.Add(item4);
                spawnItem4 = false;
            }

            if (spawnItem5 && GameWorld.isShop == true)
            {
                spriteName = shopItems[GameWorld.r.Next(0, 5)];
                Vector2 pos5 = new Vector2(1183, 388);
                ShopItem item5 = new ShopItem(spriteName, new Transform(pos5, 0));
                GameWorld.gameObjectsAdd.Add(item5);
                spawnItem5 = false;
            }

            if (spawnitem6 && GameWorld.isShop == true)
            {
                spriteName = shopItems[GameWorld.r.Next(0, 5)];
                Vector2 pos6 = new Vector2(1377, 388);
                ShopItem item6 = new ShopItem(spriteName, new Transform(pos6, 0));
                GameWorld.gameObjectsAdd.Add(item6);
                spawnitem6 = false;
            }

            if (GameWorld.isShop != true)
            {
                GameWorld.gameObjectsRemove.Add(this);
            }
        }
    }
}
