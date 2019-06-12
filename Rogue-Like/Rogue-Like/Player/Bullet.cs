using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_Like
{
    class Bullet : GameObject
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
        public Bullet(string spriteName, Transform Transform, Vector2 direction, int speed) : base(spriteName, Transform)
        {
            this.speed = speed;
            this.velocity = direction * speed;
            rotation = this.Transform.Rotation;
        }

        public override void Update(GameTime gameTime)
        {
            age++;
            if (age > 100)
            {
                GameWorld.gameObjectsRemove.Add(this);
            }
            this.Transform.Position += velocity;
            base.Update(gameTime);
        }
    }
}
