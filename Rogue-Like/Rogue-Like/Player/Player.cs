﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rogue_Like
{
    public class Player : GameObject
    {
        Controller controller = new Controller();

        private string[] nameList = { "Bore Ragnerock", "Hilbo Maggins", "Pappy Poonter", "Michael the bicicle", "Boris Boatman","Mr.X"};
        public static string name;
        public bool shoot;
        public static int currentHealth;
        public static int maxHealth;
        public static int meleeDamage;
        public static int rangedDamage;
        public static int bulletCount;
        public static string score;
        public static int myScore;
        public string coin;
        public static int myCoin;
        public string food;
        public static int myFood;
        private double lastShot;
       
        /// <summary>
        /// The players Constructor
        /// </summary>
        /// <param name="spriteName"></param>
        /// <param name="Transform"></param>
        public Player(string spriteName, Transform Transform) : base(spriteName, Transform)
        {
            coin = controller.GetItem(4);
            Int32.TryParse(coin, out myCoin); //Converts the string coin to int Coin
            score = controller.GetPlayerScore();
            Int32.TryParse(score, out myScore); //string score to int dataScore
            food = controller.GetItem(5);
            Int32.TryParse(food, out myFood); //Convert the string food to int Food
            name = nameList[GameWorld.r.Next(0,3)]; //randomise a name after the player dies
            maxHealth = 20;
            currentHealth = 20;
            meleeDamage = 10;
            rangedDamage = 5;

            //Thread CheckPoint = new Thread(controller.SaveChar);
            //CheckPoint.IsBackground = true;
            //CheckPoint.Start();
                        
        }

        /// <summary>
        /// Allows the player to move around
        /// </summary>
        /// <param name="speed"></param>
        public void PlayerMovement(int speed)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Transform.Position.Y -= 1 * speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Transform.Position.X -= 1 * speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Transform.Position.Y += 1 * speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Transform.Position.X += 1 * speed;
            }
        }
        /// <summary>
        /// Player hitbox
        /// </summary>
        public override Rectangle Hitbox
        {
            get { return new Rectangle((int)Transform.Position.X + 1, (int)Transform.Position.Y, sprite.Width, sprite.Height); }
        }

        public override void Update(GameTime gameTime)
        {
            lastShot += gameTime.ElapsedGameTime.TotalSeconds;
          
            PlayerRanged();
            PlayerMelee();
            
            base.Update(gameTime);
        }
        /// <summary>
        /// Allows the player to melee attack
        /// </summary>
        public void PlayerMelee()
        {
            var mouseState = Mouse.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.R) && lastShot >= 0.5f || mouseState.LeftButton == ButtonState.Pressed && lastShot >= 0.5)
            {
                Vector2 mousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                Vector2 direction = mousePos - this.Transform.Position;
                direction.Normalize();
                PlayerMeleeAttack playerMelee = new PlayerMeleeAttack("PlayerSwipeTemp", new Transform(new Vector2(this.Transform.Position.X, this.Transform.Position.Y), 0), direction, rotation);
                GameWorld.gameObjectsAdd.Add(playerMelee);
                lastShot = 0;
                GameWorld.playerMeleeSound.Play();
            }
        }

        /// <summary>
        /// Collison between player and objects
        /// </summary>
        /// <param name="otherObject"></param>
        public override void DoCollision(GameObject otherObject)
        {
            if (otherObject is EnemyBullet)
            {
                currentHealth -= RangedEnemy.damage;
                GameWorld.gameObjectsRemove.Add(otherObject);
            }
            if (otherObject is Coin)
            {
                myCoin++;
                GameWorld.gameObjectsRemove.Add(otherObject);
                GameWorld.moneyPickupSound.Play();
            }
            if (otherObject is Food)
            {
                
                myFood++;
                GameWorld.gameObjectsRemove.Add(otherObject);
                GameWorld.gameObjectsAdd.Add(new Bone("Bone", new Transform(Transform.Position, 0)));
                
            }
            if (otherObject is Ammo)
            {
                bulletCount += 10;
                GameWorld.gameObjectsRemove.Add(otherObject);
            }
        }

        /// <summary>
        /// Player ranged method
        /// </summary>
        public void PlayerRanged()
        {
            var mouseState = Mouse.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && lastShot >= 1f && shoot == true || mouseState.RightButton == ButtonState.Pressed && lastShot >= 1f && shoot == true)
            {
                Vector2 mousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                Vector2 direction = mousePos - this.Transform.Position;
                direction.Normalize();
                Bullet bullet = new Bullet("BulletTest", new Transform(new Vector2(this.Transform.Position.X, this.Transform.Position.Y), 0), direction, 5);
                GameWorld.gameObjectsAdd.Add(bullet);
                lastShot = 0;
                bulletCount--;
                GameWorld.playerRangedSound.Play();
            }
            if (bulletCount <= 0)
            {
                shoot = false;
            }
            else
            {
                shoot = true;
            }
        }

        
    }
}
