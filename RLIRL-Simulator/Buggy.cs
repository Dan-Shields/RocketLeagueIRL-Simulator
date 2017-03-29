using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RLIRL_Simulator
{
    class Buggy
    {
        public static Texture2D texture;
        public static Vector2 size = new Vector2(50,50);
        public static Vector2 origin = new Vector2(size.X / 2, size.Y / 3);
        public static Rectangle hitbox = new Rectangle(0, 0, (int) size.X, (int) size.Y);

        public Vector2 position;
        public float rotation;

        public Buggy(Vector2 startingPosition, float startingRotation = 0)
        {
            position = startingPosition;
            rotation = startingRotation;
        }

        private void Update(Vector2 moveVector, float rotAngle)
        {
            position = Vector2.Add(position, moveVector);
            rotation += rotAngle;

            Console.WriteLine(moveVector);

        }

        public void Move(int leftWheel, int rightWheel)
        {
            double rightWheelDecimal = rightWheel / 255.0;
            double leftWheelDecimal = leftWheel / 255.0;

            double magnitude = Math.Sqrt(Math.Pow(rightWheelDecimal, 2) + Math.Pow(leftWheelDecimal, 2));

            double angle;

            if (leftWheelDecimal != rightWheelDecimal)
            {
                angle = Math.Tan((rightWheelDecimal + leftWheelDecimal) * 5) * 0.005;
            }
            else
            {
                angle = 0;
            }

            /*int direction = 1;

            if (rightWheelDecimal < leftWheelDecimal)
            {
                direction = -1;
            }

            angle *= direction;*/

            float floatAngle = (float) angle;

            float newAngle = floatAngle + rotation;

            double yDistance = Math.Cos(newAngle) * -1 * magnitude;
            double xDistance = Math.Sin(newAngle) * magnitude;

            Vector2 moveVector = new Vector2((float)xDistance, (float)yDistance);

            Update(moveVector, floatAngle);
        }
    }
}
