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
        /// sees if the bullet is dead or not
        /// </summary>
        /// <returns></returns>
        public bool IsDead()
        {
            return age > 100;
        }
        /// <summary>
        /// kills the bullet
        /// </summary>
        public void Kill()
        {
            this.age = 200;
        }
        /// <summary>
        /// allows the bullet to bend, so it dosen´t fly in a straight line so that we will be allowed to hit the target
        /// </summary>
        /// <param name="value"></param>
        //public void SetRotation(float value)
        //{
        //    rotation = value;
        //    velocity = Vector2.Transform(new Vector2(0, -speed), Matrix.CreateRotationZ(rotation));
        //}
        /// <summary>
        /// shows the stats of the bullet
        /// </summary>
        /// <param name="tex"></param>
        /// <param name="pos"></param>
        /// <param name="damage"></param>
        /// <param name="speed"></param>
        /// <param name="rotation"></param>
        public Bullet(string spriteName, Transform Transform, Vector2 direction, int speed) : base(spriteName,Transform)
        {
            this.damage = damage;
            this.speed = speed;
            this.rotation = rotation;
            this.velocity = direction * speed;
            rotation = this.Transform.Rotation;
        }

        public override void Update(GameTime gameTime)
        {
            age++;
            this.Transform.Position += velocity;
            base.Update(gameTime);
        }
    }
}
