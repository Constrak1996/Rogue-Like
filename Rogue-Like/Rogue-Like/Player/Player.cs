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
        private MouseState mouse;
        Vector2 distance;
        private Bullet bullet;

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

            bullet = new Bullet("BulletTest", new Transform(new Vector2(0, 0), 0), Vector2.Zero);
        }

        public override void Update(GameTime gameTime)
        {
            //Attack cooldown
            lastAttack += gameTime.ElapsedGameTime.TotalSeconds;

            PlayerMovement(3);

            LookAtMouse();
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

        private void LookAtMouse()
        {
            //Distance from the mouse to the player position
            mouse = Mouse.GetState();
            distance.X = mouse.X - this.Transform.Position.X;
            distance.Y = mouse.Y - this.Transform.Position.Y;

            //Creating a 90 degree triangle to figure out the angle of from player to mouse
            this.Transform.Rotation = (float)Math.Atan2(distance.Y, distance.X);
        }

        public void PlayerMelee()
        {

        }

        public void PlayerRanged()
        {
            Bullet.pos = this.Transform.Position;
            Vector2 direction = Vector2.Subtract(Bullet.pos, new Vector2(mouse.X, mouse.Y));
            direction.Normalize();

            //Cooldown control to control the rate of which the bullets are fired when pressing the shoot button
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && lastAttack > 0.7f)
            {
                GameWorld.gameObjectsAdd.Add(new Bullet("BulletTest", new Transform(Bullet.pos, 0), direction));                
                lastAttack = 0;
            }
        }
    }
}
