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

        //Spawn Bool
        public bool spawned;

        public int damage;

        public Vector2 enemyPos;
        public float enemyMoveSpeed = 1;
        private double lastAttack;

        string[] enemies = { "melee", "ranged" };
        Random r = new Random();
        //int index = r.Next(enemies.Length);
        private string enemyType;
        private double deleteBullet;

        //Enemy hitbox
        public override Rectangle Hitbox
        {
            get { return new Rectangle((int)Transform.Position.X + 1, (int)Transform.Position.Y, Sprite.Width, Sprite.Height); }
        }

        public Enemy(string spriteName, Transform Transform, int damage, int health, string enemyType) : base(spriteName, Transform)
        {
            this.enemyType = enemyType;
        }

        public override void Update(GameTime gameTime)
        {
            //Attack cooldown
            lastAttack += gameTime.ElapsedGameTime.TotalSeconds;
            deleteBullet +=gameTime.ElapsedGameTime.TotalSeconds;


            //Spawns enemies in the given rooms
            EnemySpawner();

            //Determines what enemy type it is, melee or ranged
            Type();

            //Determines what happens on collision
            //OnCollision();

            base.Update(gameTime);
        }

        public void EnemySpawner()
        {
            #region Level 1
            if (GameWorld.level1 == true)
            {
                if (GameWorld.room1 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    GameWorld.room1 = false;
                }
                if (GameWorld.room2 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    GameWorld.room2 = false;
                }
                if (GameWorld.room3 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    GameWorld.room3 = false;
                }
                if (GameWorld.room4 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    GameWorld.room4 = false;
                }
            }
            #endregion
            #region Level 2
            if (GameWorld.level2 == true)
            {
                if (GameWorld.room1 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    GameWorld.room1 = false;
                }
                if (GameWorld.room2 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    GameWorld.room2 = false;
                }
                if (GameWorld.room3 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    GameWorld.room3 = false;
                }
                if (GameWorld.room4 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    GameWorld.room4 = false;
                }
            }
            #endregion
            #region Level 3
            if (GameWorld.level3 == true)
            {
                if (GameWorld.room1 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    GameWorld.room1 = false;
                }
                if (GameWorld.room2 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    GameWorld.room2 = false;
                }
                if (GameWorld.room3 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    GameWorld.room3 = false;
                }
                if (GameWorld.room4 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    GameWorld.room4 = false;
                }
            }
            #endregion
            #region Level 4
            if (GameWorld.level4 == true)
            {
                if (GameWorld.room1 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    GameWorld.room1 = false;
                }
                if (GameWorld.room2 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    GameWorld.room2 = false;
                }
                if (GameWorld.room3 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    GameWorld.room3 = false;
                }
                if (GameWorld.room4 == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        SpawnEnemy();
                    }
                    GameWorld.room4 = false;
                }
            }
            #endregion
        }

        public void SpawnEnemy()
        {
            Random r = new Random();
            GameWorld.gameObjectsAdd.Add(new Enemy("Worker", new Transform(new Vector2(r.Next(50, 500), r.Next(50, 500)), 0), 5, 20, "ranged"));
        }
        
        //public void OnCollision()
        //{
        //    if (this.Hitbox.Intersects(GameWorld.player.Hitbox))
        //    {
        //        if (lastAttack > 1f)
        //        {
        //            Player.health -= 1;
        //            lastAttack = 0;
        //        }
        //    }

        //    if (this.Hitbox.Intersects(GameWorld.bullet.Hitbox))
        //    {
        //        GameWorld.gameObjectsRemove.Add(this);
        //    }
        //}

        public void Type()
        {
            if (enemyType == "melee")
            {
                ChasePlayer();
            }

            if (enemyType == "ranged")
            {
                ChasePlayer();

                
                Vector2 direction = Vector2.Subtract(this.Transform.Position, new Vector2(GameWorld.player.Transform.Position.X, GameWorld.player.Transform.Position.Y));
                direction.Normalize();

                if (lastAttack >= 1)
                {
                    EnemyBullet bullet = new EnemyBullet("BulletTest", new Transform(this.Transform.Position, 0), direction);
                    GameWorld.gameObjectsAdd.Add(bullet);
                    lastAttack = 0;

                    if (deleteBullet >= 3)
                    {
                        GameWorld.gameObjectsRemove.Add(bullet);
                        deleteBullet = 0;
                    }
                }                  
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
