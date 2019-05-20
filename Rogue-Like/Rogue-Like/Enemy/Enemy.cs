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

        //Spawn Bool
        public bool spawned;

        public int damage;

        public Vector2 enemyPos;
        public float enemyMoveSpeed = 1;

        //Enemy hitbox
        public override Rectangle Hitbox
        {
            get { return new Rectangle((int)Transform.Position.X + 1, (int)Transform.Position.Y, Sprite.Width, Sprite.Height); }
        }

        public Enemy(string spriteName, Transform Transform, int damage) : base(spriteName, Transform)
        {
        }

        public override void Update(GameTime gameTime)
        {
            ChasePlayer();
            EnemySpawner();
            OnCollision();
            base.Update(gameTime);
        }

        public void EnemySpawner()
        {
            #region Level 1
            if (level1 == true)
            {
                if (room1 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    room1 = false;
                }
                if (room2 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    room2 = false;
                }
                if (room3 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    room3 = false;
                }
                if (room4 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    room4 = false;
                }
            }
            #endregion
            #region Level 2
            if (level2 == true)
            {
                if (room1 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    room1 = false;
                }
                if (room2 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    room2 = false;
                }
                if (room3 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    room3 = false;
                }
                if (room4 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    room4 = false;
                }
            }
            #endregion
            #region Level 3
            if (level3 == true)
            {
                if (room1 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    room1 = false;
                }
                if (room2 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    room2 = false;
                }
                if (room3 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    room3 = false;
                }
                if (room4 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    room4 = false;
                }
            }
            #endregion
            #region Level 4
            if (level4 == true)
            {
                if (room1 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    room1 = false;
                }
                if (room2 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    room2 = false;
                }
                if (room3 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    room3 = false;
                }
                if (room4 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    room4 = false;
                }
            }
            #endregion
        }

        public void SpawnEnemy()
        {
            Random r = new Random();
            GameWorld.gameObjectsAdd.Add(new Enemy("Worker", new Transform(new Vector2(r.Next(50, 500), r.Next(50, 500)), 0), 5));
        }

        public void OnCollision()
        {
            if (this.Hitbox.Intersects(GameWorld.player.Hitbox))
            {
                GameWorld.gameObjectsRemove.Add(this);
            }
        }

        public void ChasePlayer()
        {
            Vector2 direction = GameWorld.player.Transform.Position - this.Transform.Position;
            direction.Normalize();
            Vector2 velocity = direction * enemyMoveSpeed;
            this.Transform.Position += velocity;
        }
    }
}
