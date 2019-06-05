using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Rogue_Like
{
    class Bone : GameObject
    {
        private float time;
        Thread deleteItem;

        public Bone(string spriteName, Transform Transform) : base(spriteName, Transform)
        {
            
        }
        public override void Update(GameTime gameTime)
        {
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (time > 2)
            {
                RemoveItem();
            }
            base.Update(gameTime);
        }
        public void RemoveItem()
        {
            GameWorld.gameObjectsRemove.Add(this);
        }
    }
}
