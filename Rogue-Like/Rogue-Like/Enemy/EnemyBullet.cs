using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Rogue_Like
{
    class EnemyBullet : GameObject
    {
        private int damage;
        private int age;
        private int speed;
        /// <summary>
        /// return the damage of the bullet
        /// </summary>
        public int Damage
        {
            get { return damage; }
        }
        
        /// <summary>
        /// shows the stats of the bullet
        /// </summary>
        /// <param name="tex"></param>
        /// <param name="pos"></param>
        /// <param name="damage"></param>
        /// <param name="speed"></param>
        /// <param name="rotation"></param>
        public EnemyBullet(string spriteName, Transform Transform, Vector2 direction, int speed) : base(spriteName, Transform)
        {
            this.speed = speed;
            this.velocity = direction * speed;
            rotation = this.Transform.Rotation;
        }

        public override void Update(GameTime gameTime)
        {
            age++;
            this.Transform.Position += velocity;
            if (age > 100)
            {
                GameWorld.gameObjectsRemove.Add(this);
            }
            base.Update(gameTime);
        }
    }
}
