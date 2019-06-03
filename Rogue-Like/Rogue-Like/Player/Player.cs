using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_Like
{
    public class Player : GameObject
    {
        Controller controller = new Controller();
        public static string Name;
        public bool shoot;
        public static int health;
        public static int damage;
        public static int bulletCount;
        public static string score;
        public static int DataScore;
        public string coin;
        public static int Coin;
        public string food;
        public static int Food;
        private double lastShot;

        /// <summary>
        /// The players Constructor
        /// </summary>
        /// <param name="spriteName"></param>
        /// <param name="Transform"></param>
        public Player(string spriteName, Transform Transform) : base(spriteName, Transform)
        {
            coin = controller.getItem(4);
            Int32.TryParse(coin, out Coin);
            score = controller.getPlayerScore();
            Int32.TryParse(score, out DataScore);
            food = controller.getItem(5);
            Int32.TryParse(food, out Food);
            Name = "Peter";
            health = 20;
            damage = 10;
            bulletCount = 20;
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
            
            base.Update(gameTime);
        }

        public void PlayerMelee()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.E) && lastShot > 0.5f)
            {
                Vector2 mousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                Vector2 direction = mousePos - this.Transform.Position;
                direction.Normalize();
                PlayerMeleeAttack playerMelee = new PlayerMeleeAttack("PlayerSwipeTemp", new Transform(new Vector2(this.Transform.Position.X, this.Transform.Position.Y), 0), direction, rotation);
                GameWorld.gameObjectsAdd.Add(playerMelee);
                lastShot = 0;
            }
        }

        
        public override void DoCollision(GameObject otherObject)
        {
            if (otherObject is EnemyBullet)
            {
                health -= Enemy.damage;
                GameWorld.gameObjectsRemove.Add(otherObject);
            }
            if (otherObject is Coin)
            {
                Coin++;
                GameWorld.gameObjectsRemove.Add(otherObject);
            }
            if (otherObject is Food)
            {
                Food++;
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
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && lastShot > 1f && shoot == true)
            {
                Vector2 mousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                Vector2 direction = mousePos - this.Transform.Position;
                direction.Normalize();
                Bullet bullet = new Bullet("BulletTest", new Transform(new Vector2(this.Transform.Position.X, this.Transform.Position.Y), 0), direction, 5);
                GameWorld.gameObjectsAdd.Add(bullet);
                lastShot = 0;
                bulletCount--;              
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
