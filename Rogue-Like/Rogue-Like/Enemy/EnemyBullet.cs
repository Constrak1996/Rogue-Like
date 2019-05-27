using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Rogue_Like
{
    public class EnemyBullet : GameObject
    {
        private string v;
        private Transform transform;
        private Vector2 direction;

        public override Rectangle Hitbox
        {
            get { return new Rectangle((int)Transform.Position.X + 1, (int)Transform.Position.Y, Sprite.Width, Sprite.Height); }
        }

        public EnemyBullet(string spriteName, Transform Transform, Vector2 direction) : base(spriteName, Transform)
        {
            this.direction = direction;
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
