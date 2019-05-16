using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_Like
{
    public class Enemy : GameObject
    {
        //Level bools
        public static bool level1;
        public static bool level2;
        public static bool level3;
        public static bool level4;

        //Room bools
        public static bool room1;
        public static bool room2;
        public static bool room3;
        public static bool room4;

        public int damage;

        //Enemy hitbox
        public override Rectangle Hitbox
        {
            get { return new Rectangle((int)Transform.Position.X + 1, (int)Transform.Position.Y, Sprite.Width, Sprite.Height); }
        }

        public Enemy(string spriteName, Transform Transform, int damage) : base(spriteName, Transform)
        {
        }

        public void EnemySpawner()
        {
            if (level1 == true)
            {
                if (room1 == true)
                {

                }
                if (room2 == true)
                {

                }
                if (room3 == true)
                {

                }
                if (room4 == true)
                {

                }
            }
            if (level2 == true)
            {
                if (room1 == true)
                {

                }
                if (room2 == true)
                {

                }
                if (room3 == true)
                {

                }
                if (room4 == true)
                {

                }
            }
            if (level3 == true)
            {
                if (room1 == true)
                {

                }
                if (room2 == true)
                {

                }
                if (room3 == true)
                {

                }
                if (room4 == true)
                {

                }
            }
            if (level4 == true)
            {
                if (room1 == true)
                {

                }
                if (room2 == true)
                {

                }
                if (room3 == true)
                {

                }
                if (room4 == true)
                {

                }
            }
        }

        public void OnCollision(GameObject player)
        {
            if (Hitbox.Intersects(player.Hitbox))
            {
                GameWorld.gameObjectsRemove.Add(this);
            }
        }
    }
}
