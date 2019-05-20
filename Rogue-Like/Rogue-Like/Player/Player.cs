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
        public Player(string spriteName, Transform Transform) : base(spriteName, Transform)
        {
            
            Player.coin = controller.getItem(4);
            Player.score = controller.getPlayerScore();
            Player.food = controller.getItem(5);
            Player.Name = "Peter";
            Player.health = randomPlayerHealth.Next(50, 75);
            Player.damage = randomPlayerDamage.Next(10, 120);
        }
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
    }
}
