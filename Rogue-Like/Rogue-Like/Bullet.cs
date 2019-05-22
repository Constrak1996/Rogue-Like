using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rogue_Like
{
    class Bullet : GameObject
    {
        public float velocity;
        public float speed;

        public Bullet(string spriteName, Transform Transform) : base(spriteName, Transform)
        {
            this.velocity = 2.5f;
            this.speed = 20;
        }

        public override Rectangle Hitbox => base.Hitbox;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
