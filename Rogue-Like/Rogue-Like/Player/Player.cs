using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_Like
{
    public class Player : GameObject
    {
        public static string Name;
        public static int health;
        public static int damage;
        public Random randomPlayerDamage = new Random();
        public Random randomPlayerHealth = new Random();
        public Player(string spriteName, Transform Transform) : base(spriteName, Transform)
        {
            Player.Name = "Bob";
            Player.health = randomPlayerHealth.Next(50, 75);
            Player.damage = randomPlayerDamage.Next(10, 120);
        }

        /// <summary>
        /// Player hitbox
        /// </summary>
        public override Rectangle Hitbox
        {
            get { return new Rectangle((int)Transform.Position.X + 1, (int)Transform.Position.Y, Sprite.Width, Sprite.Height); }
        }
    }
}
