using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Rogue_Like
{
    public class RangedEnemy : GameObject
    {
        double lastAttack;
        float enemyMoveSpeed = 0.5f;

        public RangedEnemy(string spriteName, Transform Transform, int health) : base(spriteName, Transform)
        {
        }

        public override Rectangle Hitbox => base.Hitbox;

        public override void Update(GameTime gameTime)
        {
            //Attack cooldown
            lastAttack += gameTime.ElapsedGameTime.TotalSeconds;

            EnemyRanged();
            ChasePlayer();

            base.Update(gameTime);
        }

        /// <summary>
        /// Logic on how enemies chase the player
        /// </summary>
        public void ChasePlayer()
        {
            Vector2 direction = GameWorld.player.Transform.Position - this.Transform.Position;
            direction.Normalize();
            Vector2 velocity = direction * enemyMoveSpeed;
            this.Transform.Position += velocity;
        }

        /// <summary>
        /// Handles collision 
        /// </summary>
        /// <param name="otherObject"></param>
        public override void DoCollision(GameObject otherObject)
        {
            if (otherObject is Player)
            {
                if (lastAttack > 1.5f)
                {
                    Player.health -= 1;
                    lastAttack = 0;
                }
            }

            //Bullet collision
            if (otherObject is Bullet)
            {
                GameWorld.gameObjectsRemove.Add(this);
                GameWorld.gameObjectsRemove.Add(otherObject);
                int lootpool = GameWorld.r.Next(0, 3);
                switch (lootpool)
                {
                    case 0:
                        GameWorld.gameObjectsAdd.Add(new Bone("Bone", new Transform(Transform.Position, 0)));
                        break;
                    case 1:
                        GameWorld.gameObjectsAdd.Add(new Coin("Coin", new Transform(Transform.Position, 0)));
                        break;
                    case 2:
                        GameWorld.gameObjectsAdd.Add(new Food("Food", new Transform(Transform.Position, 0)));
                        break;
                    case 3:
                        GameWorld.gameObjectsAdd.Add(new Ammo("BulletTest", new Transform(Transform.Position, 0)));
                        break;

                }
            }

            base.DoCollision(otherObject);
        }

        /// <summary>
        /// Enemy ranged attack method
        /// </summary>
        public void EnemyRanged()
        {
            if (lastAttack > 0.5f)
            {
                Vector2 direction = GameWorld.player.Transform.Position - this.Transform.Position;
                direction.Normalize();
                EnemyBullet bullet = new EnemyBullet("BulletTest", new Transform(new Vector2(this.Transform.Position.X, this.Transform.Position.Y), 0), direction, 5);
                GameWorld.gameObjectsAdd.Add(bullet);
                lastAttack = 0;
            }
        }
    }
}
