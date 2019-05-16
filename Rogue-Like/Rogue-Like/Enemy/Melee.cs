using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_Like
{
    public class MeleeEnemy : Enemy
    {
        public MeleeEnemy(string spriteName, Transform Transform, int damage) : base(spriteName, Transform, damage)
        {
        }
    }
}
