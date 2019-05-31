using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
        private string type;



        //Enemy hitbox
        public override Rectangle Hitbox
        {
            get { return new Rectangle((int)Transform.Position.X + 1, (int)Transform.Position.Y, Sprite.Width, Sprite.Height); }
        }

        public Enemy(string spriteName, Transform Transform, int health) : base(spriteName, Transform)
        {
        }

        public override void Update(GameTime gameTime)
        {
            //Attack cooldown
            lastAttack += gameTime.ElapsedGameTime.TotalSeconds;

            EnemySpawner();
            ChasePlayer();
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
            int enemyType = GameWorld.r.Next(0,3); 

            switch (enemyType)
            {
                case 0:
                    GameWorld.gameObjectsAdd.Add(new Enemy("Worker", new Transform(new Vector2(GameWorld.r.Next(192, 1538), GameWorld.r.Next(192, 887)), 0), 50));
                    break;
                case 1:
                    GameWorld.gameObjectsAdd.Add(new RangedEnemy("Worker", new Transform(new Vector2(GameWorld.r.Next(192, 1538), GameWorld.r.Next(192, 887)), 0), 50));
                    break;
                case 2:
                    GameWorld.gameObjectsAdd.Add(new Enemy("Worker", new Transform(new Vector2(GameWorld.r.Next(192, 1538), GameWorld.r.Next(192, 887)), 0), 50));
                    break;
                case 3:
                    GameWorld.gameObjectsAdd.Add(new RangedEnemy("Worker", new Transform(new Vector2(GameWorld.r.Next(192, 1538), GameWorld.r.Next(192, 887)), 0), 50));
                    break;
                default:
                    break;
            }
        }

        public void ChasePlayer()
        {
            Vector2 direction = GameWorld.player.Transform.Position - this.Transform.Position;
            direction.Normalize();
            Vector2 velocity = direction * enemyMoveSpeed;
            this.Transform.Position += velocity;
        }

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
            }

            base.DoCollision(otherObject);
        }

        //public void EnemyRanged()
        //{
        //    if (lastAttack > 0.5f)
        //    {
        //        Vector2 direction =  GameWorld.player.Transform.Position - this.Transform.Position;
        //        direction.Normalize();
        //        EnemyBullet bullet = new EnemyBullet("BulletTest", new Transform(new Vector2(this.Transform.Position.X, this.Transform.Position.Y), 0), direction, 5);
        //        GameWorld.gameObjectsAdd.Add(bullet);
        //        lastAttack = 0;
        //    }
        //}
    }
}
