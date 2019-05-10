using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Rogue_Like
{
    public class Enemy : GameObject
    {
        public string enemyName;
        public int health;
        public int damage;

        public Enemy(string textureName, ContentManager Content, Transform Transform) : base(textureName, Content, Transform)
        {

        }
    }
}