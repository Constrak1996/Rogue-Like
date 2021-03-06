﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rogue_Like
{
    public class Enemy : GameObject
    {
        Controller controller = new Controller();
        
        //Spawn Bool
        public bool spawned;
        private int health;
        public static int damage = 1; //static attack of the enemy

        public Vector2 enemyPos;
        public float enemyMoveSpeed = 1;
        private double lastAttack;
        
        /// <summary>
        /// Enemy hitbox
        /// </summary>
        public override Rectangle Hitbox
        {
            get { return new Rectangle((int)Transform.Position.X + 1, (int)Transform.Position.Y, sprite.Width, sprite.Height); }
        }

        public Enemy(string spriteName, Transform Transform) : base(spriteName, Transform)
        {
            health = 100; //Enemys health
            
        }

        public override void Update(GameTime gameTime)
        {
            //Attack cooldown
            lastAttack += gameTime.ElapsedGameTime.TotalSeconds;

            EnemySpawner();
            ChasePlayer();
            base.Update(gameTime);
        }

        /// <summary>
        /// Keeps track of how many enemies spawn in each room, and doesn't respawn if you already
        /// cleared the room
        /// </summary>
        public void EnemySpawner()
        {
            #region Level 1
            if (GameWorld.room1 == true)
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
            if (GameWorld.room2 == true)
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
            if (GameWorld.room3 == true)
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
            if (GameWorld.room4 == true)
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

        /// <summary>
        /// Enemy spawn type and location
        /// </summary>
        public void SpawnEnemy()
        {
            int enemyType = GameWorld.r.Next(0,3); 

            switch (enemyType)
            {
                case 0:
                    GameWorld.gameObjectsAdd.Add(new Enemy("Worker", new Transform(new Vector2(GameWorld.r.Next(192, 1538), GameWorld.r.Next(192, 887)), 0)));
                    break;
                case 1:
                    GameWorld.gameObjectsAdd.Add(new RangedEnemy("RangedWorker", new Transform(new Vector2(GameWorld.r.Next(192, 1538), GameWorld.r.Next(192, 887)), 0)));
                    break;
                case 2:
                    GameWorld.gameObjectsAdd.Add(new Enemy("Worker", new Transform(new Vector2(GameWorld.r.Next(192, 1538), GameWorld.r.Next(192, 887)), 0)));
                    break;
                case 3:
                    GameWorld.gameObjectsAdd.Add(new RangedEnemy("RangedWorker", new Transform(new Vector2(GameWorld.r.Next(192, 1538), GameWorld.r.Next(192, 887)), 0)));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Method that handles how the enemies chase the player
        /// </summary>
        public void ChasePlayer()
        {
            Vector2 direction = GameWorld.player.Transform.Position - this.Transform.Position;
            direction.Normalize();
            Vector2 velocity = direction * enemyMoveSpeed;
            this.Transform.Position += velocity;
        }

        /// <summary>
        /// OnCollision is where the collision logic is located
        /// </summary>
        /// <param name="otherObject"></param>
        public override void DoCollision(GameObject otherObject)
        {
            //Player collision
            if (otherObject is Player)
            {
                if (lastAttack > 1.5f)
                {
                    Player.currentHealth -= Enemy.damage;
                    lastAttack = 0;
                    GameWorld.playerMeleeSound.Play();
                }
            }

            //Bullet collision
            if (otherObject is Bullet)
            {
                

                health -= Player.rangedDamage;
                GameWorld.gameObjectsRemove.Add(otherObject);
                if (health <=0)
                {
                    GameWorld.gameObjectsRemove.Add(this);
                    int lootpool = GameWorld.r.Next(1, 3);
                    switch (lootpool)
                    {
                        //case 0:
                        //    GameWorld.gameObjectsAdd.Add(new Bone("Bone", new Transform(Transform.Position, 0)));
                        //    break;
                        case 1:
                            GameWorld.gameObjectsAdd.Add(new Coin("Coin", new Transform(Transform.Position, 0)));
                            break;
                        case 2:
                            GameWorld.gameObjectsAdd.Add(new Food("Meat", new Transform(Transform.Position, 0)));
                            break;
                        case 3:
                            GameWorld.gameObjectsAdd.Add(new Ammo("BulletTest", new Transform(Transform.Position, 0)));
                            break;

                    }
                }
                GameWorld.gameObjectsRemove.Add(otherObject);
                
            }

            //PlayerMelee collision
            if (otherObject is PlayerMeleeAttack)
            {
                health -= Player.meleeDamage;
                GameWorld.gameObjectsRemove.Add(otherObject);
                if (health <=0)
                {
                    GameWorld.gameObjectsRemove.Add(this);
                    int lootpool = GameWorld.r.Next(1, 3);
                    switch (lootpool)
                    {
                        
                        case 1:
                            GameWorld.gameObjectsAdd.Add(new Coin("Coin", new Transform(Transform.Position, 0)));
                            break;
                        case 2:
                            GameWorld.gameObjectsAdd.Add(new Food("Meat", new Transform(Transform.Position, 0)));
                            break;
                        case 3:
                            GameWorld.gameObjectsAdd.Add(new Ammo("BulletTest", new Transform(Transform.Position, 0)));
                            break;

                    }
                }
                
            }

            base.DoCollision(otherObject);
        }
    }
}
