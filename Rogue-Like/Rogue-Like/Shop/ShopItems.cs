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
        private string item;

        public override Rectangle Hitbox => base.Hitbox;

        public ShopItems(string spriteName, Transform Transform, string item) : base(spriteName, Transform)
        {
            item = shopItems[GameWorld.r.Next(0, 4)];
        }
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void DoCollision(GameObject otherObject)
        {
            if (otherObject is Player && this.item == "Slingshot" && Keyboard.GetState().IsKeyDown(Keys.E))
            {

            }

            if (otherObject is Player && this.item == "Armor of The Gods" && Keyboard.GetState().IsKeyDown(Keys.E))
            {

            }

            if (otherObject is Player && this.item == "Stick" && Keyboard.GetState().IsKeyDown(Keys.E))
            {

            }

            if (otherObject is Player && this.item == "Sword" && Keyboard.GetState().IsKeyDown(Keys.E))
            {

            }

            if (otherObject is Player && this.item == "Club" && Keyboard.GetState().IsKeyDown(Keys.E))
            {

            }

            if (otherObject is Player && this.item == "Rock" && Keyboard.GetState().IsKeyDown(Keys.E)
                )
            {

            }
            base.DoCollision(otherObject);
        }

        public void SpawnShopItems()
        {

        }
    }
}
