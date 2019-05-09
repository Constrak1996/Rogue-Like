﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Rogue_Like
{
    public class Equipment : Player
    {
        private List<Component> weapon;

        public Equipment(Texture2D playersprite, string textureName, ContentManager Content, Transform Transform, int health) : base(playersprite, textureName, Content, Transform, health)
        {

        }
    }
}