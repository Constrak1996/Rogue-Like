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
        public static int health;
        public static int damage;
        public static string score;
        public static string coin;
        public static string food;
        public Random randomPlayerDamage = new Random();
        public Random randomPlayerHealth = new Random();
        private double lastAttack;

        /// <summary>
        /// The players Constructor
        /// </summary>
        /// <param name="spriteName"></param>
        /// <param name="Transform"></param>
        public Player(string spriteName, Transform Transform) : base(spriteName, Transform)
        {

            Player.coin = controller.getItem(4);
            Player.score = controller.getPlayerScore();
            Player.food = controller.getItem(5);
            Player.Name = "Peter";
            Player.health = randomPlayerHealth.Next(50, 75);
            Player.damage = randomPlayerDamage.Next(10, 120);
        }

        public override void Update(GameTime gameTime)
        {
            //Attack cooldown
            lastAttack += gameTime.ElapsedGameTime.TotalSeconds;

            PlayerMovement(3);
            PlayerRanged();
            PlayerMelee();

            base.Update(gameTime);
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
            get { return new Rectangle((int)Transform.Position.X + 1, (int)Transform.Position.Y, Sprite.Width, Sprite.Height); }
        }

        public void PlayerMelee()
        {

        }

        public void PlayerRanged()
        {
            ////Cooldown control to control the rate of which the bullets are fired when pressing the shoot button
            //if (Keyboard.GetState().IsKeyDown(Keys.Space) && lastAttack > 0.7f)
            //{

            //    Vector2 shootDirection = Mouse.GetState().Position.ToVector2() - position;
            //    Bullet gameBullet = new Bullet(content, GameWorld.bulletTexture, position, shootDirection, speed, rotation);
            //    GameWorld.gameObjectsAdd.Add(gameBullet);
            //    lastShot = 0;
            //}
        }
    }
}
