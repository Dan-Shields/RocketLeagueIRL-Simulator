using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RLIRL_Simulator
{
    class Ball
    {
        public static Texture2D texture;
        public static Vector2 size = new Vector2(25,25), origin = new Vector2(size.X/2,size.Y/2);
        public static Rectangle hitbox = new Rectangle(0, 0, 25, 25);

        public Vector2 position;

        public Ball(Vector2 startPosition)
        {
            position = startPosition;
        }
    }
}
