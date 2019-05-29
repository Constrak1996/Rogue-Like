using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Rogue_Like
{
    class PlayerBullet : GameObject
    {
        public PlayerBullet(string spriteName, Transform Transform) : base(spriteName, Transform)
        {
        }

        public override Rectangle Hitbox => base.Hitbox;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
