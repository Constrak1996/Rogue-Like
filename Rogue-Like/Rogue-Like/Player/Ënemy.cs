﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Rogue_Like
{
    public class Ënemy : GameObject
    {
        public Ënemy(string textureName, ContentManager Content, Transform Transform) : base(textureName, Content, Transform)
        {
        }
    }
}