using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rogue_Like
{
    public class Bullet : GameObject
    {
        private Vector2 direction;
        public static Vector2 pos;

        public override Rectangle Hitbox
        {
            get { return new Rectangle((int)Transform.Position.X + 1, (int)Transform.Position.Y, Sprite.Width, Sprite.Height); }
        }

        public Bullet(string spriteName, Transform Transform, Vector2 direction) : base(spriteName, Transform)
        {
            this.direction = direction;
            pos = this.Transform.Position;
        }


        public override void Update(GameTime gameTime)
        {
            Movement();
            base.Update(gameTime);
        }

        public void Movement()
        {
            this.Transform.Position -= direction * 10;
        }
    }
}
