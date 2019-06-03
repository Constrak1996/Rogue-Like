using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rogue_Like
{
    class PlayerMeleeAttack : GameObject
    {
        private float speed = 13;
        private int age;

        public override Rectangle Hitbox => base.Hitbox;

        public PlayerMeleeAttack(string spriteName, Transform Transform, Vector2 direction, float rotation) : base(spriteName, Transform)
        {
            this.velocity = direction * speed;
            rotation = this.rotation;
        }

        public override void Update(GameTime gameTime)
        {
            age++;
            this.Transform.Position += velocity;
            LookAtMouse();
            if (age > 4)
            {
                GameWorld.gameObjectsRemove.Add(this);
            }
            base.Update(gameTime);
        }
        
        public override void DoCollision(GameObject otherObject)
        {
            base.DoCollision(otherObject);
        }

        private void LookAtMouse()
        {
            //Distance from the mouse to the player position
            Vector2 mousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            Vector2 distance = mousePos - this.Transform.Position;

            //Creating a 90 degree triangle to figure out the angle of from player to mouse
            this.rotation = (float)Math.Atan2(distance.Y, distance.X);
        }
    }
}
