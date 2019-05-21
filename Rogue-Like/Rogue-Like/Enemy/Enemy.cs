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
        Controller controller = new Controller();
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
        public static Random random = new Random();
        //Enemy Stats
        public int damage;
        public int health;

        public Vector2 enemyPos;
        public float enemyMoveSpeed = 1;

        //Enemy hitbox
        public override Rectangle Hitbox
        {
            get { return new Rectangle((int)Transform.Position.X + 1, (int)Transform.Position.Y, Sprite.Width, Sprite.Height); }
        }

        public Enemy(string spriteName, Transform Transform, int damage, int health, float range) : base(spriteName, Transform)
        {
        }

        public override void Update(GameTime gameTime)
        {
            EnemySpawner();
            ChasePlayer();
            OnCollision();
            base.Update(gameTime);
        }

        public void EnemySpawner()
        {

            #region Level 1

            if (level1 == true)
            {
                for (int i = 0; i <= 4; i++)
                {
                    SpawnEnemy();
                    level1 = false;
                }
                

                if (room1 == true)
                {


                    room1 = false;
                    room2 = true;
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

        public static void SpawnEnemy()
        {
            
            GameWorld.gameObjectsAdd.Add(new Enemy("Worker", new Transform(new Vector2(random.Next(50, 500), random.Next(50, 500)), 0), 5,20,2));
        }
        
        public void OnCollision()
        {
            if (this.Hitbox.Intersects(GameWorld.player.Hitbox))
            {
                GameWorld.gameObjectsRemove.Add(this);
                Player.Score++;
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
